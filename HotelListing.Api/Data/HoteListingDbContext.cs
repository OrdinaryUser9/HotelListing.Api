using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Data;

public class HoteListingDbContext : DbContext
{
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Country> Countries { get; set; }

    public HoteListingDbContext(DbContextOptions<HoteListingDbContext> options) : base(options)
    {
        
    }
}
