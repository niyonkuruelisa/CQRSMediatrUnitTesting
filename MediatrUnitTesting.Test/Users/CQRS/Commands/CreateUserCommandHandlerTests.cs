
using MediatrUnitTesting.CQRS.Users.Commands;
using MediatrUnitTesting.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit.Abstractions;

namespace MediatrUnitTesting.Test.Users.CQRS.Commands
{
	public sealed class CreateUserCommandHandlerTests
	{
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock; 
        private readonly ILogger<CreateUserCommandAsync> _logger;
		private readonly ITestOutputHelper Console;
		public CreateUserCommandHandlerTests(ITestOutputHelper output)
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
			_logger = new NullLogger<CreateUserCommandAsync>();
			Console = output;
		}

        [Fact]
        public async void Handler_Should_ReturnUser_WhenUniqueEmailProvided()
        {
            // Arrange
            var uniqueEmail = "testing@email.com";
			var command = new CreateUserCommandAsync.Command(new User() { Name = "Testing User", Email = uniqueEmail });
            var hander = new CreateUserCommandAsync.Handler(_userRepositoryMock.Object, _unitOfWorkMock.Object);
			_userRepositoryMock.Setup(ur => ur.IsEmailUnique(uniqueEmail)).Returns(true);
			// Act

			var response =  await hander.Handle(command,default);
            // Assert
            Assert.NotNull(response.user);
        }

        [Fact]
        public async void Handler_Should_ReturnNull_WhenNonUniqueEmailProvided()
        {
			// Arrange
			var command = new CreateUserCommandAsync.Command(new User() { Name = "Testing User", Email = "testing@email.com" });
            _userRepositoryMock.Setup(user => user.Create(new User() { Name = "Testing User", Email = "testing@email.com" }));
            var handler = new CreateUserCommandAsync.Handler(_userRepositoryMock.Object, _unitOfWorkMock.Object);
            //Act
            var response = await handler.Handle(command, default);
			//Assert
            Assert.Null(response.user);
		}
    }
}