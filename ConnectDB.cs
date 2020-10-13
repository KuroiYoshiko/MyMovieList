using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Windows.Media.Imaging;
using System.Data.SQLite;
using Dapper;
using System.IO;
using System.Windows.Media;

namespace MyMovieList
{
    public class ConnectDB
    {

        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_connect = new SQLiteConnection(@"Data Source=mymovielistDB.db;Version=3");
            try
            {
                sqlite_connect.Open();
            }
            catch (SystemException)
            {

            }
            return sqlite_connect;
        }

        public static void MainPage_load(ref List<MainPagePicture> picture)
        {
            var dbConnection = CreateConnection();
            string zapytanie = "Select picture from Mainpage"; 
            using var cmd = new SQLiteCommand(zapytanie, dbConnection);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                picture.Add(new MainPagePicture() { picture = GetBytes(rdr, 0)});
            }
        }

        public static void AddMovie(Movies movies)
        {
            System.Data.IDbConnection dbConnection = CreateConnection();
            string zapytanie;
            if (movies.rate == "")
                zapytanie = "INSERT INTO Movies(movie_name, year_ofProd, genre, description, poster, load_into) VALUES " +
                            "(@movie_name, @year_ofProd, @genre, @description, @poster, @load_into);";
            else
                zapytanie = "INSERT INTO Movies(movie_name, year_ofProd, genre, description, poster, load_into, rate, review) VALUES " +
                            "(@movie_name, @year_ofProd, @genre, @description, @poster, @load_into, @rate, @review);";
            dbConnection.Execute(zapytanie, movies);

        }
        
        public static void Load_Movie_Details(ref Movies movies, string movie_id)
        {
            var dbConnection = CreateConnection();
            string zapytanie = "Select poster, movie_name, year_ofProd, genre, description, rate, review, load_into from Movies where movie_id='" + movie_id + "';";
            using var cmd = new SQLiteCommand(zapytanie, dbConnection);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                movies.poster = GetBytes(rdr, 0);
                movies.movie_name = rdr.GetString(1);
                movies.year_ofProd = rdr.GetValue(2).ToString();
                movies.genre = rdr.GetString(3);
                movies.description = rdr.GetString(4);
                movies.rate = rdr.GetValue(5).ToString();
                movies.review = rdr.GetValue(6).ToString();
                movies.load_into = rdr.GetString(7);
            }

        }
        public static void DeleteMovie(string  movie_id)
        {
            var dbConnection = CreateConnection();
            string zapytanie = "DELETE from Movies where movie_id=\"" + movie_id + "\"";
            using var cmd = new SQLiteCommand(zapytanie, dbConnection);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
        }
        public static void UpdateMovie_review(ref Movies movies, string movie_id)
        {
            var dbConnection = CreateConnection();
            string zapytanie = "UPDATE Movies set rate=\"" + movies.rate + "\", review=\"" + movies.review + "\", load_into=\"Watched\" where movie_id='" + movie_id + "';";
            using var cmd = new SQLiteCommand(zapytanie, dbConnection);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
        }
        public static void LoadPoster_List(ref List<Movies> movies)
        {
            var dbConnection = CreateConnection();
            string zapytanie = "Select poster, movie_id  from Movies where load_into=\"To-watch list\"";
            using var cmd = new SQLiteCommand(zapytanie, dbConnection);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                movies.Add(new Movies() { poster = GetBytes(rdr, 0), movie_id = rdr.GetValue(1).ToString() });
            }
        }
        public static void LoadPoster_Ranking(ref List<Movies> movies)
        {
            var dbConnection = CreateConnection();
            string zapytanie = "Select poster, movie_id from Movies where rate>0 order by rate desc";
            using var cmd = new SQLiteCommand(zapytanie, dbConnection);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                movies.Add(new Movies() { poster = GetBytes(rdr, 0), movie_id = rdr.GetValue(1).ToString() });
            }
        }
        public static void LoadPoster_Watched(ref List<Movies> movies)
        {
            var dbConnection = CreateConnection();
            string zapytanie = "Select poster, movie_id  from Movies where load_into=\"Watched\"";
            using var cmd = new SQLiteCommand(zapytanie, dbConnection);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                movies.Add(new Movies() { poster = GetBytes(rdr, 0), movie_id = rdr.GetValue(1).ToString() });
            }
        }


        static byte[] GetBytes(SQLiteDataReader reader, int i)
        {
            const int CHUNK_SIZE = 2 * 1024;
            byte[] buffer = new byte[CHUNK_SIZE];
            long bytesRead;
            long fieldOffset = 0;
            using (MemoryStream stream = new MemoryStream())
            {
                while ((bytesRead = reader.GetBytes(i, fieldOffset, buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, (int)bytesRead);
                    fieldOffset += bytesRead;
                }
                return stream.ToArray();
            }
        }

        static public void change_poster(System.Windows.Controls.Image image, byte[] zdjecie)
        {
            var bitmap = (BitmapSource)new ImageSourceConverter().ConvertFrom(zdjecie);
            image.Source = bitmap;
        }



    }

}
