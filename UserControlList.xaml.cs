﻿using System;
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

    public partial class UserControlList : UserControl
    {
        public UserControlList()
        {
            InitializeComponent();
            List<Movies> movie_posters = new List<Movies>();
            ConnectDB.LoadPoster_List(ref movie_posters);
            foreach (Movies movies in movie_posters)
            {
                list_movies(movies.poster, movies.movie_id);
            }
        }
        private void list_movies(byte[] zdjecie, string nazwa)
        {
            var bitmap = (BitmapSource)new ImageSourceConverter().ConvertFrom(zdjecie);
            ListViewItem item = new ListViewItem();
            Image image = new Image();
            image.Source = bitmap;
            image.Stretch = Stretch.UniformToFill;
            item.Content = image;
            item.DataContext = nazwa;
            ListMovies.Items.Add(item);
        }

        private void ListMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var see = (ListView)sender;
            ListViewItem see_item = (ListViewItem)see.SelectedItem;
            UserControlFilm film = new UserControlFilm(see_item.DataContext.ToString());
            MainWindow.grid_main.Children.Add(film);
        }

    }
}
