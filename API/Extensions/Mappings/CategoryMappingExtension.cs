using API.Dtos.Category;
using Core.Entities;

namespace API.Extensions.Mappings
{
    public static class CategoryMappingExtension
    {
        public static CategoryDto ToDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                PCategoryName = category.PCategory?.Name,
                CCategorieNames = category.CCategories?.Select(c => c.Name).ToList(),
                Books = category.Books?.Select(b => b.ToDto()).ToList()
            };
        }

        public static Category ToEntity(this CategoryUpsertDto categoryDto)
        {
            return new Category
            {
                Name = categoryDto.Name,
                PId = categoryDto.PId,
            };
        }
    }
}
