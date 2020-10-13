using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Media.Imaging;

namespace MyMovieList
{
    public class Movies : IEquatable<Movies>
    {
        public string movie_id { get; set; }
        public string movie_name { get; set; }
        public string year_ofProd { get; set; }
        public string genre { get; set; }
        public string description { get; set; }
        public byte[] poster { get; set; }
        public string load_into { get; set; }
        public string rate { get; set; }
        public string review { get; set; }

        public bool Equals([AllowNull] Movies other)
        {
            throw new NotImplementedException();
        }
    }
}
