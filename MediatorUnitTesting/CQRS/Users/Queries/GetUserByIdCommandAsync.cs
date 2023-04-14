using MediatrUnitTesting.Abstractions;
using MediatrUnitTesting.Models;
using MediatR;

namespace MediatrUnitTesting.CQRS.Users.Queries
{
	public class GetUserByIdCommandAsync
	{
		public record Query(Guid userId) : IRequest<Response>;
		public class Handler : IRequestHandler<Query, Response>
		{
			private readonly IUserRepository userRepository;
			private readonly IUnitOfWork unityOfWork;

			public Handler(IUserRepository userRepository, IUnitOfWork unityOfWork)
			{
				this.userRepository = userRepository;
				this.unityOfWork = unityOfWork;
			}

			public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
			{
				var user = userRepository.GetUserById(request.userId);
				await unityOfWork.SaveChangesAsync(cancellationToken);

				return new Response(user);
			}

		}
		public record Response(User user);
	}
}
