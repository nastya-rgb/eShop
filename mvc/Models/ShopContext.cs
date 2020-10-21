using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using mvc.Models.ConpInfo;
using mvc.Models.Product;
using mvc.Models.script;

namespace mvc.Models
{
    public class ShopContext : DbContext
    {
        private readonly ILoggerFactory loggerFactory = LoggerFactory.Create(config => config.AddConsole());
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory);
        }
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        { }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<OS> OS { get; set; }
        //для скриптов
        public DbSet<PhoneChart> PhoneChart { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PhoneChart>().HasNoKey().ToView(null);

            builder.Entity<Users>().HasIndex(u => u.IIN).IsUnique();
            builder.Entity<Users>().HasIndex(u => u.Login).IsUnique();
             builder.Entity<Company>().HasIndex(u => u.XIN).IsUnique();
              builder.Entity<Users>().HasIndex(u =>new{ u.Name, u.LastName}).IsUnique();



        }
    }
}