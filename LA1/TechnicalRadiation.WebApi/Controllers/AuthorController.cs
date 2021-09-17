using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services.Interfaces;

namespace TechnicalRadiation.WebApi.Controllers
{
	[ApiController]
	[Route("api/authors/")]
	public class AuthorController : ControllerBase
	{
		private readonly IAuthorService _authorService;

		public AuthorController(IAuthorService authorService)
		{
			_authorService = authorService;
		}

		// /api?pageSize=25&pageNumber=0
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

        [HttpGet]
        [Route("{id:int}/newsItems", Name = "GetAuthorNewsItems")]

        public IActionResult GetAuthorNewsItems(int id) {
            return Ok(_authorService.GetAuthorNewsItems(id));
        }


	}
}
