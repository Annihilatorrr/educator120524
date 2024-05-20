using Microsoft.EntityFrameworkCore;

namespace Educator.Dal
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BinaryIntegerElementaryExercise>().HasData(
                new BinaryIntegerElementaryExercise
                {
                    Id = 1,
                    FirstOperand = 1,
                    SecondOperand = 1,
                    Answer = 23
                }
            );

            modelBuilder.Entity<BinaryIntegerElementaryExercise>().HasData(
                new BinaryIntegerElementaryExercise
                {
                    Id = 2,
                    FirstOperand = 1,
                    SecondOperand = 1,
                    Answer = 223
                }
            );
        }
    }
}
