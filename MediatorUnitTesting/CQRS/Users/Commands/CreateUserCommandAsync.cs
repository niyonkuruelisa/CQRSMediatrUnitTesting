using MediatrUnitTesting.Abstractions;
using MediatrUnitTesting.Models;
using MediatR;

namespace MediatrUnitTesting.CQRS.Users.Commands
{
	public class CreateUserCommandAsync
	{
		public record Command(User user) : IRequest<Response>;
		public class Handler : IRequestHandler<Command, Response>
		{
			private readonly IUserRepository userRepository;
			private readonly IUnitOfWork unityOfWork;

			public Handler(IUserRepository userRepository,IUnitOfWork unityOfWork)
            {
				this.userRepository = userRepository;
				this.unityOfWork = unityOfWork;
			}
            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
			{
				userRepository.Create(request.user);
				await unityOfWork.SaveChangesAsync(cancellationToken);
				
				return new Response(request.user);
			}
		}
		public record Response(User user);
	}
}
