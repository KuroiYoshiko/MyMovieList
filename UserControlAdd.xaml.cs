using Microsoft.Win32;
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
using System.Data.SQLite;
using System.IO;

namespace MyMovieList
{
    public partial class UserControlAdd : UserControl
    {
        public UserControlAdd()
        {           
            InitializeComponent();
        }

        public OpenFileDialog op = new OpenFileDialog();


        private void AddPosterButton_Click(object sender, RoutedEventArgs e)
        {
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                PastePosterHere.Source = new BitmapImage(new Uri(op.FileName));
            }           
        }

        private void SumbitButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(new BitmapImage(new Uri(op.FileName))));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            Movies movies = new Movies();
            movies.movie_name = TitleBox.Text;
            movies.year_ofProd = YearBox.Text;
            movies.genre = GenreBox.Text;
            movies.description = DescriptionBox.Text;
            movies.poster = data;
            movies.load_into = LoadIntoBox.Text;
            movies.rate = RateBox.Text;
            movies.review = ReviewBox.Text;
            ConnectDB.AddMovie(movies);
        }


        private void LoadIntoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LoadIntoBox.SelectedItem == ToWatchItem) SumbitButton.IsEnabled = true;
            if (LoadIntoBox.SelectedItem == WatchedItem) SumbitButton.IsEnabled = false;

            if (LoadIntoBox.SelectedItem == WatchedItem)
            {
                ReviewGrid.Visibility = Visibility.Visible;
            }
            else if (LoadIntoBox.SelectedItem == ToWatchItem)
            {
                ReviewGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void YearBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LoadIntoBox.SelectedItem == ToWatchItem && GenreBox.SelectedIndex >= 0 && YearBox.SelectedIndex > 0) SumbitButton.IsEnabled = true;
            else SumbitButton.IsEnabled = false;
        }

        private void GenreBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LoadIntoBox.SelectedItem == ToWatchItem && GenreBox.SelectedIndex >= 0 && YearBox.SelectedIndex > 0) SumbitButton.IsEnabled = true;
            else SumbitButton.IsEnabled = false;
        }

        private void RateBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RateBox.SelectedIndex>=0) SumbitButton.IsEnabled = true;
        }
    }
}
