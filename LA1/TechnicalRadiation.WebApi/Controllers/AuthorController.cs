using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services.Interfaces;
using TechnicalRadiation.Models.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace TechnicalRadiation.WebApi.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("api/authors/")]
	public class AuthorController : ControllerBase
	{
		private readonly IAuthorService _authorService;

		public AuthorController(IAuthorService authorService)
		{
			_authorService = authorService;
		}

		[HttpGet]
		[Route("")]
		public IActionResult GetAllAuthors()
		{
			return Ok(_authorService.GetAllAuthors());
		}

		[HttpGet]
		[Route("{id:int}", Name = "GetAuthorById")]
		public IActionResult GetAuthorById(int id) {
			return Ok(_authorService.GetAuthorById(id));
		}

		[Authorize]
		[HttpPost]
		[Route("")]
		public IActionResult CreateAuthor(AuthorInputModel newAuthor) {
			if (!_authorService.IsValidToken(Request.Headers["Authorization"])) {return Unauthorized();}
			int newid = _authorService.CreateAuthor(newAuthor);
			return CreatedAtRoute("GetAuthorById", new { id = newid }, null);
		}
		[Authorize]
		[HttpPut]
		[Route("{id:int}", Name = "UpdateAuthorById")]
		public IActionResult UpdateAuthorById(int id, [FromBody] AuthorInputModel newAuthor)
		{
			if (!_authorService.IsValidToken(Request.Headers["Authorization"])) {return Unauthorized();}
			if (!ModelState.IsValid)
            {

                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                throw new ModelFormatException(message);
            }
			_authorService.UpdateAuthorById(id, newAuthor);
			return NoContent();
		}

		[HttpDelete]
		[Authorize]
		[Route("{id:int}", Name = "DeleteAuthorById")]
		public IActionResult DeleteAuthorById(int id)
		{
			if (!_authorService.IsValidToken(Request.Headers["Authorization"])) {return Unauthorized();}
			_authorService.DeleteAuthorById(id);
			return NoContent();
		}

		[HttpPut]
		[Authorize]
		[Route("{authorid:int}/newsItems/{newsId:int}")]
		public IActionResult LinkAuthorNewsItem(int authorid, int newsId)
		{
			_authorService.LinkAuthorNewsItem(authorid, newsId);
			return NoContent();
		}
	}
}
