using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DataTransferObjects;
using WebApi.Filters;
using WebApi.Repositories;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly CommentRepository _commentRepo;

		public CommentController(CommentRepository commentRepo)
		{
			_commentRepo = commentRepo;
		}

        [UseApiKey]
        [HttpPost]
		public async Task<IActionResult> Create(CommentHttpRequest request)
		{
			if (ModelState.IsValid)
			{
				var comment = await _commentRepo.CreateAsync(request);

				if (comment != null)
				{
					return Created("", comment);
				}
			}

			return BadRequest();
		}
	}
}
