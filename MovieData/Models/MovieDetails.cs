using System;
using System.Collections.Generic;

namespace MovieData.Models
{

    public partial class MovieDetails
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }
    }
}
