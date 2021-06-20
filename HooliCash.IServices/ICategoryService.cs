using HooliCash.DTOs.Category;
using System;
using System.Collections.Generic;

namespace HooliCash.IServices
{
    public interface ICategoryService
    {
        CategoryDto CreateCategory(CreateCategoryDto createCategoryDto);
        IEnumerable<CategoryDto> GetCategories();
        CategoryDto GetCategory(Guid categoryId);
        CategoryDto UpdateCategory(UpdateCategoryDto dto);
        bool DeleteCategory(Guid id);
    }
}
