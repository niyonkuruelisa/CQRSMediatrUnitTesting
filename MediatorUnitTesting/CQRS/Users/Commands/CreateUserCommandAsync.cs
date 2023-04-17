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

				if (userRepository.IsEmailUnique(request.user.Email))
				{
					userRepository.Create(request.user);
					await unityOfWork.SaveChangesAsync(cancellationToken);

					return new Response(request.user);
				}
				else
				{

					return new Response(null);
				}
				
			}
		}
		public record Response(User user);
	}
}
