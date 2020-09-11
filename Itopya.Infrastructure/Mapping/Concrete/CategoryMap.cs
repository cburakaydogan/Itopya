using Itopya.Domain.Entities.Concrete;
using Itopya.Infrastructure.Mapping.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itopya.Infrastructure.Mapping.Concrete
{
    public class CategoryMap : BaseMap<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.ParentCategory).WithMany(x => x.SubCategories).HasForeignKey(x => x.ParentCategoryId);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

            base.Configure(builder);
        }
    }
}
