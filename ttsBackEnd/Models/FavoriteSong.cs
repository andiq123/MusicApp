namespace ttsBackEnd.Models
{
    public class FavoriteSong : Song
    {
        public int SongID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}