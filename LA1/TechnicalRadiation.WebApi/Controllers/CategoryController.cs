using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services.Interfaces;

namespace TechnicalRadiation.WebApi.Controllers
{
	[ApiController]
	[Route("api/categories/")]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		// /api?pageSize=25&pageNumber=0
		[HttpGet]
		[Route("")]
		public IActionResult GetAllCategories()
		{
			return Ok(_categoryService.GetAllCategories());
		}

		[HttpGet]
		[Route("{id:int}", Name = "GetCategoryById")]
		public IActionResult GetCategoryById(int id) {
			return Ok(_categoryService.GetCategoryById(id));
		}

		// [HttpPost]
		// [Route("")]
		// public IActionResult CreateCategory([FromBody] CategoryInputModel newCat)
		// {
		// 	if (!ModelState.IsValid)
		// 	{
		// 		var message = string.Join(" | ", ModelState.Values
        //             .SelectMany(v => v.Errors)
        //             .Select(e => e.ErrorMessage));
        //         throw new ModelFormatException(message);
		// 	}
		// 	int newid = _categoryService.CreateCategory(newCat);
		// 	return Created("GetCategoryById", GetCategoryById(newid));
		// }
	}
}
