using MyMovieList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public partial class MainWindow : Window
    {
        public static Grid grid_main;
        public MainWindow()
        {
            InitializeComponent();
            grid_main = GridMain;
            GridMain.Children.Add(new UserControlHome());
        }
        private void ButtonPopUpExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void klik_Click(object sender, RoutedEventArgs e)
        {
            Page eeno = new Page();
            this.Content = eeno;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            switch (index)
            {
                case 0:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlHome());
                    break;
                case 1:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlRanking());
                    break;
                case 2:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlWatched());
                    break;
                case 3:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlList());
                    break;
                case 4:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlAdd());
                    break;
            }
        }

        private void ListViewCloseApp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewCloseApp.SelectedIndex;
            switch (index)
            {
                case 0:
                    WindowState = WindowState.Minimized;
                    break;
                case 1:
                    Application.Current.Shutdown();
                    break;
            }
        }
    }
}
