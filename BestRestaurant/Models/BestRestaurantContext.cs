using Microsoft.EntityFrameworkCore;

namespace BestRestaurant.Models
{
public class BestRestaurantContext : DbContext
  {
    public DbSet<Cuisine> Cuisines { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }

    public BestRestaurantContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  } 
}