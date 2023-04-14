
using MediatrUnitTesting.CQRS.Users.Commands;
using MediatrUnitTesting.Models;

namespace MediatrUnitTesting.Test.Users.CQRS.Commands
{
    public sealed class CreateUserCommandHandlerTests
	{
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        public CreateUserCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            
        }

        [Fact]
        public async void Handler_Should_ReturnCreatedUser_WhenUniqueUserProvided()
        {
            // Arrange
            var command = new CreateUserCommandAsync.Command(new User() { Name = "Testing User", Email = "testing@email.com" });
            var hander = new CreateUserCommandAsync.Handler(_userRepositoryMock.Object, _unitOfWorkMock.Object);
           // Act
          
            var response =  await hander.Handle(command,default);
           // Assert
           Assert.Same(command.user, response.user);
        }
    }
}