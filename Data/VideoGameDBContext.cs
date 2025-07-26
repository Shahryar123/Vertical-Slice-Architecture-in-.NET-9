using Microsoft.EntityFrameworkCore;
using Vertical_Slice_Architecture.Entities;

namespace Vertical_Slice_Architecture.Data
{
    public class VideoGameDBContext : DbContext
    {
        public VideoGameDBContext(DbContextOptions<VideoGameDBContext> options)
            : base(options)
        {
        }
        public DbSet<VideoGame> VideoGames { get; set; }
        
    }
}
