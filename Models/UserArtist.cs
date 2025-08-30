using System;
using System.Collections.Generic;

namespace Artist_api1.Models
{
    public partial class UserArtist
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
    }
}
