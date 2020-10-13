using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MyMovieList
{
    public class MainPagePicture : IEquatable<MainPagePicture>
    {
        public byte[] picture { get; set; }

        public bool Equals([AllowNull] MainPagePicture other)
        {
            throw new NotImplementedException();
        }
    }
}
