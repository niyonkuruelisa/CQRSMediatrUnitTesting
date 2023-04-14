using MediatrUnitTesting.CQRS.Users.Queries;

namespace MediatrUnitTesting.Test.Users.CQRS.Queries
{

	public sealed class GetUserByIdQueryHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        public GetUserByIdQueryHandlerTests()
        {
            _userRepositoryMock = new();
            _unitOfWorkMock = new();

        }

        [Fact]
        public async void Handler_Should_ReturnNull_WhenInvalidUserIdIsProvided()
        {
            // Arrange
            var query = new GetUserByIdCommandAsync.Query(Guid.NewGuid());
            var handler = new GetUserByIdCommandAsync.Handler(_userRepositoryMock.Object,_unitOfWorkMock.Object);
            //Act
            var response = await handler.Handle(query, default);
			//Assert
			Assert.Null(response.user);
		}
    }
}
