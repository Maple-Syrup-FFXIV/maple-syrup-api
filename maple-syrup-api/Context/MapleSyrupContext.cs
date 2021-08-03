using maple_syrup_api.Models;
using Microsoft.EntityFrameworkCore;

namespace maple_syrup_api.Context
{
    public class MapleSyrupContext: DbContext
    {
        public MapleSyrupContext (DbContextOptions<MapleSyrupContext> options) : base(options)
        {
        }

        public DbSet<GuildConfig> GuildConfigs { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventRequirement> EventRequirements { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=maplesyrup;user=maplesyrup;password=Passw0rd!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GuildConfig>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.GuildId).IsRequired();
                entity.Property(e => e.Prefix).IsRequired();
            });
        }
    }
}
