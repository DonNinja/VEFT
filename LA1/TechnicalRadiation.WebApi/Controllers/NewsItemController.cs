using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Extensions;
using TechnicalRadiation.Services.Interfaces;
using TechnicalRadiation.Models.Exceptions;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace TechnicalRadiation.WebApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/")]
    public class NewsItemController : ControllerBase
    {
        private readonly INewsItemService _newsItemService;

        public NewsItemController(INewsItemService newsItemService)
        {
            _newsItemService = newsItemService;
        }

        // /api?pageSize=25&pageNumber=0
        [HttpGet]
        [Route("")]
        public IActionResult GetAllNewsItems([FromQuery] int pageSize = 25, int pageNumber = 0)
        {
            return Ok(_newsItemService.GetNewsPages(pageSize, pageNumber));
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetNewsItemById")]
        public IActionResult GetNewsItemById(int id)
        {
            return Ok(_newsItemService.GetNewsItemById(id));
        }

        [HttpGet]
        [Route("authors/{id:int}/newsItems", Name = "GetAuthorNewsItems")]

        public IActionResult GetAuthorNewsItems(int id) {
            return Ok(_newsItemService.GetAuthorNewsItems(id));
        }

        [HttpPost]
        [Route("")]
		[Authorize]
        public IActionResult CreateNewsItem([FromBody] NewsItemInputModel nItem)
        {
			if (!_newsItemService.IsValidToken(Request.Headers["Authorization"])) {return Unauthorized();}
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                throw new ModelFormatException(message);
            }
			int newid = _newsItemService.CreateNewsItem(nItem);
            return CreatedAtRoute("GetNewsItemById", new { id = newid }, null);
            // return Created("GetNewsItemById", GetNewsItemById(newid));

        }
        [HttpPut]
        [Route("{id:int}", Name = "UpdateNewsItemById")]
		[Authorize]
        public IActionResult UpdateNewsItemById(int id, [FromBody] NewsItemInputModel nItem)
        {
			if (!_newsItemService.IsValidToken(Request.Headers["Authorization"])) {return Unauthorized();}
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                throw new ModelFormatException(message);
            }

            _newsItemService.UpdateNewsItemById(id, nItem);
            return NoContent();
        }
    
		[HttpDelete]
		[Route("{id:int}", Name = "DeleteNewsItemById")]
		[Authorize]
		public IActionResult DeleteNewsItemById(int id)
		{
            _newsItemService.DeleteNewsItemById(id);
			return NoContent();
		}
	}
}
