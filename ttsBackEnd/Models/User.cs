using System;
using System.Collections.Generic;

namespace ttsBackEnd.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ProfilePicUrl { get; set; }
        public DateTime LastOnline { get; set; }
        public DateTime DateJoined { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

    }
}