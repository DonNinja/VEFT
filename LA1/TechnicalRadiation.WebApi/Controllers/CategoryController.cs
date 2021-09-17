using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services.Interfaces;
using TechnicalRadiation.Models.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace TechnicalRadiation.WebApi.Controllers
{
    [AllowAnonymous]
	[ApiController]
	[Route("api/categories/")]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

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

		[HttpPost]
		[Route("")]
		[Authorize]
		public IActionResult CreateCategory([FromBody] CategoryInputModel newCat)
		{
			if (!_categoryService.IsValidToken(Request.Headers["Authorization"])) {return Unauthorized();}
			if (!ModelState.IsValid)
			{
				var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                throw new ModelFormatException(message);
			}
			int newid = _categoryService.CreateCategory(newCat);
			return CreatedAtRoute("GetNewsItemById", new { id = newid }, null);
		}

		[Authorize]
		[HttpPut]
		[Route("{id:int}", Name = "UpdateCategoryById")]
		public IActionResult UpdateCategoryById(int id, [FromBody] CategoryInputModel newCat)
		{
			if (!_categoryService.IsValidToken(Request.Headers["Authorization"])) {return Unauthorized();}
			if (!ModelState.IsValid)
            {

                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                throw new ModelFormatException(message);
            }
			_categoryService.UpdateCategoryById(id, newCat);
			return NoContent();
		}

		[Authorize]
		[HttpDelete]
		[Route("{id:int}", Name = "DeleteCategoryById")]
		public IActionResult DeleteCategoryById(int id)
		{
			if (!_categoryService.IsValidToken(Request.Headers["Authorization"])) {return Unauthorized();}
			_categoryService.DeleteCategoryById(id);
			return NoContent();
		}

		[Authorize]
		[HttpPut]
		[Route("{catid:int}/newsItems/{newsid:int}")]
		public IActionResult LinkNewsToCategory(int catId, int newsId)
		{
			if (!_categoryService.IsValidToken(Request.Headers["Authorization"])) {return Unauthorized();}
			_categoryService.LinkNewsToCategory(catId, newsId);
			return NoContent();
		}
	}
}
