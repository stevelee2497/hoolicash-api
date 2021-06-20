using HooliCash.DTOs.Category;
using HooliCash.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HooliCash.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        public ActionResult CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            var response = _categoryService.CreateCategory(createCategoryDto);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        public ActionResult GetCategories()
        {
            var response = _categoryService.GetCategories();
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        public ActionResult GetCategory(Guid id)
        {
            var response = _categoryService.GetCategory(id);
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        public ActionResult UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var response = _categoryService.UpdateCategory(updateCategoryDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        public ActionResult DeleteCategory(Guid id)
        {
            var response = _categoryService.DeleteCategory(id);
            return Ok(response);
        }
    }
}
