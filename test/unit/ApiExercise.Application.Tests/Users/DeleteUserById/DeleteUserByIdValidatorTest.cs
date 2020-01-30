using System.Threading;
using ApiExercise.Application.Common.Queries;
using ApiExercise.Application.Users.DeleteUserById;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace ApiExercise.Application.Tests.Users.DeleteUserById
{
    public class DeleteUserByIdValidatorTest
    {
        /*
         * NOTE: I select this unit test,
         * 1) I solved a bug in this validation so I completed the DOD (Definition of done) making a test about the bug
         * 2) Have to use a mock, so I can show how nice the dependencyInjection works for testing
         */
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