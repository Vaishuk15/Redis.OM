using Models.ViewModels;
using Moq;
using PersonalPasswordManager.Repository.Models;
using PersonalPasswordManager.Services.Implementation;
using PersonalPasswordManager.Services.Interface;

namespace PersonalPasswordManager.UnitTests
{
    public class TestPasswordManagerService
    {
        private readonly Mock<IPasswordManagerService> _passwordManagerServiceMock;

        public TestPasswordManagerService()
        {
            _passwordManagerServiceMock = new Mock<IPasswordManagerService>();
        }

        [Fact]
        public async Task GetAll_ShouldReturnPasswordViewModelList()
        {
            // Arrange
            var passwords = new List<PasswordViewModel>();
            passwords.Add(new PasswordViewModel{ Id = 1, EncryptedPassword = "TXlQYXNzd29yZA==" }); 
            passwords.Add(new PasswordViewModel{ Id = 2, EncryptedPassword = "TXlQYXNz349yZA==" });
            _passwordManagerServiceMock.Setup(service => service.GetAll()).ReturnsAsync(passwords);
            var mockObject = _passwordManagerServiceMock.Object;

            // Act
            var result= await mockObject.GetAll();
            // Assert
            Assert.NotNull(result);

        }
        [Fact]
        public async Task GetById_ShouldReturnPasswordViewModel_WhenPasswordExists()
        {
            // Arrange
            var password = new PasswordViewModel { Id = 1, EncryptedPassword = "TXlQYXNzd29yZA==" }; // Base64 for "MyPassword"

            _passwordManagerServiceMock.Setup(service => service.GetById(1)).ReturnsAsync(password);
            var mockObject = _passwordManagerServiceMock.Object;

            // Act
            var result = await mockObject.GetById(1);

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task GetById_ShouldReturnNull_WhenPasswordNotFound()
        {
            // Arrange
            _passwordManagerServiceMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((PasswordViewModel)null);
            var mockObject = _passwordManagerServiceMock.Object;

            // Act
            var result = await mockObject.GetById(1);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task CreatePassword_ShouldReturnPasswordViewModel()
        {
            PasswordViewModel passwordViewModel = GetPasswordViewModel();
            // Arrange
            _passwordManagerServiceMock.Setup(repo => repo.Create(passwordViewModel)).ReturnsAsync((PasswordViewModel)passwordViewModel);
            var mockObject = _passwordManagerServiceMock.Object;
            // Act
            var result = await mockObject.Create(passwordViewModel);

            // Assert
            Assert.NotNull(result);

        }

        private static PasswordViewModel GetPasswordViewModel()
        {
            return new PasswordViewModel
            {
                Id = 1,
                DecryptedPassword = "MyPassCode",
                App = "outlook"
            };
        }

        [Fact]
        public async Task Update_ShouldReturnTrue_WhenPasswordIsUpdated()
        {
            // Arrange
            PasswordViewModel passwordViewModel = GetPasswordViewModel();

            _passwordManagerServiceMock.Setup(repo => repo.Update(1,passwordViewModel)).ReturnsAsync(true);
            var mockObject = _passwordManagerServiceMock.Object;
            // Act
            var result = await mockObject.Update(1, passwordViewModel);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public async Task Delete_ShouldRemovePassword_WhenPasswordExists()
        {
            _passwordManagerServiceMock.Setup(repo => repo.Delete(1));
            var mockObject = _passwordManagerServiceMock.Object;
            // Act
            await mockObject.Delete(1);

            // Assert
            _passwordManagerServiceMock.Verify(repo => repo.Delete(1), Times.Once);
        }


    }

}