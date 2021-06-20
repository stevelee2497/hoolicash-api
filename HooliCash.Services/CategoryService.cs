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
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Transaction> _transactionRepository;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = unitOfWork.Repository<Category>();
            _transactionRepository = unitOfWork.Repository<Transaction>();
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
            _categoryRepository.Add(model);
            _unitOfWork.Complete();
            return _mapper.Map<CategoryDto>(model);
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            return _categoryRepository.All().Select(_mapper.Map<CategoryDto>);
        }

        public CategoryDto GetCategory(Guid categoryId)
        {
            var model = _categoryRepository.Find(categoryId);
            return _mapper.Map<CategoryDto>(model);
        }

        public CategoryDto UpdateCategory(UpdateCategoryDto dto)
        {
            var model = _categoryRepository.Find(dto.Id);
            model.Name = dto.Name;
            model.IconUrl = dto.IconUrl;
            model.TransactionType = Enum.Parse<TransactionType>(dto.TransactionType);
            _categoryRepository.Update(model);
            return _mapper.Map<CategoryDto>(model);
        }

        public bool DeleteCategory(Guid id)
        {
            var model = _categoryRepository.Find(id);
            _categoryRepository.Remove(model);
            var transactions = _transactionRepository.Where(x => x.Category.Id == id);
            _transactionRepository.RemoveRange(transactions);
            _unitOfWork.Complete();
            return true;
        }

        public void SeedDataCategories()
        {
            _categoryRepository.AddRange(new [] 
            {
                new Category { Name = "Food", TransactionType = TransactionType.Expense },
                new Category { Name = "Cafe", TransactionType = TransactionType.Expense },
                new Category { Name = "Salary", TransactionType = TransactionType.Income },
            });
            _unitOfWork.Complete();
        }
    }
}
