using System.Threading;
using NetCoreApiScaffolding.Application.Users.DeleteUserById;
using FluentAssertions;
using FluentValidation;
using Moq;
using NetCoreApiScaffolding.Application.Interfaces.Queries;
using Xunit;

namespace NetCoreApiScaffolding.Application.Tests.Users.DeleteUserById
{
    public class DeleteUserByIdValidatorTest
    {
        [Fact]
        public void DeleteUserByIdValidator_ValidateAndAllIsOk_ItHAveToBeValid()
        {
            const bool idExistResult = true;
            const int id = 1;

            var request = new DeleteUserByIdRequest {Id = id};
            var idExistMock = new Mock<IIdExists>();
            idExistMock.Setup(x => x.Query(request.Id, new CancellationToken())).ReturnsAsync(idExistResult);

            var validator = new DeleteUserByIdValidator(idExistMock.Object);
            var validationContext = new ValidationContext<DeleteUserByIdRequest>(request);

            var validationResult = validator.Validate(validationContext);

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void DeleteUserByIdValidator_ValidateAndIdDoesntExists_ItHAveToBeInvalid()
        {
            const bool idExistResult = false;
            const int id = 1;

            var request = new DeleteUserByIdRequest {Id = id};
            var idExistMock = new Mock<IIdExists>();
            idExistMock.Setup(x => x.Query(request.Id, new CancellationToken())).ReturnsAsync(idExistResult);

            var validator = new DeleteUserByIdValidator(idExistMock.Object);
            var validationContext = new ValidationContext<DeleteUserByIdRequest>(request);

            var validationResult = validator.Validate(validationContext);

            validationResult.IsValid.Should().BeFalse();
        }

        [Fact]
        public void DeleteUserByIdValidator_ValidateItDoesntHaveAValue_ItHAveToBeValid()
        {
            const bool idExistResult = true;
            const int id = 0;

            var request = new DeleteUserByIdRequest {Id = id};
            var idExistMock = new Mock<IIdExists>();
            idExistMock.Setup(x => x.Query(request.Id, new CancellationToken())).ReturnsAsync(idExistResult);

            var validator = new DeleteUserByIdValidator(idExistMock.Object);
            var validationContext = new ValidationContext<DeleteUserByIdRequest>(request);

            var validationResult = validator.Validate(validationContext);

            validationResult.IsValid.Should().BeFalse();
        }
    }
}