using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Common;
using ApiExercise.Application.Common.Queries;
using FluentValidation;

namespace ApiExercise.Application.Users.DeleteUserById
{
    public class DeleteUserByIdValidator : AbstractValidator<DeleteUserByIdRequest>
    {
        private readonly IIdExists _idExists;

        public DeleteUserByIdValidator(IIdExists idExists)
        {
            _idExists = idExists;

            RuleFor(u => u.Id)
                .NotEmpty()
                .MustAsync(IdAlreadyExists)
                .WithMessage(ValidationMessages.GetItDoesntExists(nameof(DeleteUserByIdRequest.Id)));
        }

        private async Task<bool> IdAlreadyExists(int id, CancellationToken cancellationToken)
        {
            return await _idExists.Query(id, cancellationToken);
        }
    }
}