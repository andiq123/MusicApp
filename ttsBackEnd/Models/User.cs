using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ttsBackEnd.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required]
        public string Username { get; set; }
        public string ProfilePicUrl { get; set; }
        public DateTime LastOnline { get; set; }
        public DateTime DateJoined { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public ICollection<FavoriteSong> FavSongs { get; set; }

    }
}