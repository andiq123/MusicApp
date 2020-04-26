namespace ttsBackEnd.Models
{
    public class FavoriteSong : Song
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}