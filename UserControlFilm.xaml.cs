using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyMovieList
{
    /// <summary>
    /// Logika interakcji dla klasy UserControlFilm.xaml
    /// </summary>
    public partial class UserControlFilm : UserControl
    {
        private string id;
        public UserControlFilm(string movie_id)
        {
            id = movie_id;
            InitializeComponent();
            Movies movie_loadDetails = new Movies();
            ConnectDB.Load_Movie_Details(ref movie_loadDetails, movie_id);
            LoadDetails(ref movie_loadDetails);
        }
        private void LoadDetails(ref Movies movies)
        {
            MoviePoster.Source= (BitmapSource)new ImageSourceConverter().ConvertFrom(movies.poster);
            MovieTitleLabel.Content = movies.movie_name;
            YearLabel.Content = movies.year_ofProd;
            GenreLabel.Content = movies.genre;
            DescriptionTextBox.Text = movies.description;
            YourRate.Content = movies.rate;
            ReviewTextBox.Text = movies.review;
            if(movies.load_into== "Watched")
            {
                ReviewGrid.Visibility = Visibility.Visible;
            }           
        }

        private void LoadToWatched_Click(object sender, RoutedEventArgs e)
        {
            AddReview_Grid.Visibility = Visibility.Visible;
        }

        private void DeleteMovie_Click(object sender, RoutedEventArgs e)
        {
            ConnectDB.DeleteMovie(id);
        }

        private void ListViewClose_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void RateBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RateBox.SelectedIndex >= 0) SubmitNewReview.IsEnabled = true;
        }

        private void SubmitNewReview_Click(object sender, RoutedEventArgs e)
        {
            Movies movies = new Movies();
            movies.rate = RateBox.Text;
            movies.review = ReviewBox.Text;
            ConnectDB.UpdateMovie_review(ref movies, id);
        }
    }
}
