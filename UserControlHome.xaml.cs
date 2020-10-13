using MyMovieList;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MyMovieList
{


    public partial class UserControlHome : UserControl
    {
        DispatcherTimer timer = new DispatcherTimer();
        List<MainPagePicture> pictureMain = new List<MainPagePicture>();
        public UserControlHome()
        {
            InitializeComponent();

            ConnectDB.MainPage_load(ref pictureMain);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 2);
            timer.IsEnabled = true;
            timer.Start();
        }
        int i = 0;
        void timer_Tick(object sender, EventArgs e)
        {
            mainpage.Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(pictureMain[i].picture);
            i++;
            if (i == pictureMain.Count) i = 0;
        }



    }
}
