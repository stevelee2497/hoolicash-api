using AutoMapper;
using HooliCash.Core.Models;
using HooliCash.DTOs.Category;
using HooliCash.IRepositories;
using HooliCash.IServices;
using HooliCash.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HooliCash.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public CategoryDto CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var model = new Category
            {
                Name = createCategoryDto.Name,
                IconUrl = createCategoryDto.IconUrl,
                TransactionType = Enum.Parse<TransactionType>(createCategoryDto.TransactionType),
            };
            _unitOfWork.Categories.Add(model);
            _unitOfWork.Complete();
            return _mapper.Map<CategoryDto>(model);
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            return _unitOfWork.Categories.All().Select(_mapper.Map<CategoryDto>);
        }

        public CategoryDto GetCategory(Guid categoryId)
        {
            var model = _unitOfWork.Categories.Find(categoryId);
            return _mapper.Map<CategoryDto>(model);
        }

        public CategoryDto UpdateCategory(UpdateCategoryDto dto)
        {
            var model = _unitOfWork.Categories.Find(dto.Id);
            model.Name = dto.Name;
            model.IconUrl = dto.IconUrl;
            model.TransactionType = Enum.Parse<TransactionType>(dto.TransactionType);
            _unitOfWork.Categories.Update(model);
            return _mapper.Map<CategoryDto>(model);
        }

        public bool DeleteCategory(Guid id)
        {
            var model = _unitOfWork.Categories.Find(id);
            _unitOfWork.Categories.Remove(model);
            var transactions = _unitOfWork.Transactions.Where(x => x.Category.Id == id);
            _unitOfWork.Transactions.RemoveRange(transactions);
            _unitOfWork.Complete();
            return true;
        }

        public void SeedDataCategories()
        {
            _unitOfWork.Categories.AddRange(new [] 
            {
                new Category { Name = "Food", TransactionType = TransactionType.Expense },
                new Category { Name = "Cafe", TransactionType = TransactionType.Expense },
                new Category { Name = "Salary", TransactionType = TransactionType.Income },
            });
            _unitOfWork.Complete();
        }
    }
}
