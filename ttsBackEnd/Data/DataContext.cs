using Microsoft.EntityFrameworkCore;
using ttsBackEnd.Models;

namespace ttsBackEnd.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<FavoriteSong> FavSongs { get; set; }
        public DbSet<LogActivity> Activities { get; set; }
    }
}