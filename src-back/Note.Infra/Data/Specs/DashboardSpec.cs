using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Note.Core.Entities;

namespace Note.Infra.Data.Specs
{
    public class DashboardSpec
    {
        public DashboardSpec(EntityTypeBuilder<Dashboard> entityBuilder)
        {
            EntitySpec<Dashboard>.SetEntitySpecs(entityBuilder);

            entityBuilder
                .Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(100);

            entityBuilder
                .HasMany(o => o.Categories)
                .WithOne(o => o.Dashboard);

            entityBuilder
                .HasMany(o => o.Columns)
                .WithOne(o => o.Dashboard);

            entityBuilder
                .HasOne(o => o.Owner)
                .WithMany(o => o.Dashboards)
                .IsRequired();
        }
    }
}
