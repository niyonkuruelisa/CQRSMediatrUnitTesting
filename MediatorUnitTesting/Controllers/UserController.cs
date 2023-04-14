using MediatrUnitTesting.CQRS.Users.Commands;
using MediatrUnitTesting.CQRS.Users.Queries;
using MediatrUnitTesting.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediatrUnitTesting.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		
		private readonly ILogger<UserController> _logger;
		private readonly IMediator mediator;

		public UserController(ILogger<UserController> logger,IMediator mediator)
		{
			_logger = logger;
			this.mediator = mediator;
		}

		[HttpPost()]
		public async ValueTask<IActionResult> CreateUserAsync(User user)
		{
			var results = await mediator.Send(new CreateUserCommandAsync.Command(user));
			return Ok(results);
		}

		[HttpGet()]
		public async ValueTask<IActionResult> GetUserById(Guid id)
		{
			var results = await mediator.Send(new GetUserByIdCommandAsync.Query(id));
			return Ok(results);
		}
	}
}