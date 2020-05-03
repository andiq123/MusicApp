namespace ttsBackEnd.Models
{
    public class FavoriteSong
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Url { get; set; }
        public string Cover_art_url { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}