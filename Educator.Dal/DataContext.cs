using Microsoft.EntityFrameworkCore;

namespace Educator.Dal
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)    
        {
        }

        public DbSet<BinaryIntegerElementaryExercise> BinaryIntegerElementaryExercise { get; set; }
        public DbSet<GeometryProblemElementaryExercise> GeometryProblemElementaryExercise { get; set; }
        public DbSet<TextualProblemElementaryExercise> TextualProblemElementaryExercise { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
