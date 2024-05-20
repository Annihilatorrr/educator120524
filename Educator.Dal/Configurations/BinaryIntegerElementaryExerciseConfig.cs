using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Educator.Dal.Configurations
{
    internal class BinaryIntegerElementaryExerciseConfig : IEntityTypeConfiguration<BinaryIntegerElementaryExercise>
    {
        public void Configure(EntityTypeBuilder<BinaryIntegerElementaryExercise> builder)
        {
            builder.Property(ex => ex.Id).ValueGeneratedOnAdd();
        }
    }
}
