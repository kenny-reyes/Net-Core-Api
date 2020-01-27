using System;
using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Repositories.Users;
using ApiExercise.Application.Users.Queries;
using ApiExercise.Application.Shared;
using ApiExercise.Domain.Users;
using FluentValidation;
using MediatR;

namespace ApiExercise.Application.Users
{
    /*
     * NOTE: yes, looks ugly to have 3 classes in the same file, but seems it is like the people do it,
     * well, there is a discussion about this, I don't care to divide it, I don't have an opinion
     */
    public sealed class UpdateUserRequest : IRequest
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public int GenderId { get; set; }
    }

    public sealed class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        private readonly IEmailAlreadyExistsCount _emailAlreadyExistsCount;

        public UpdateUserValidator(IEmailAlreadyExistsCount emailAlreadyExistsCount)
        {
            _emailAlreadyExistsCount = emailAlreadyExistsCount;

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage(ValidationMessages.GetRequired(nameof(UpdateUserRequest.Email)))
                .MaximumLength(User.EmailMaxLength)
                .WithMessage(ValidationMessages.GetTooLong(nameof(UpdateUserRequest.Email)))
                .MustAsync(ValidEmail)
                .WithMessage(ValidationMessages.GetValidRequired(nameof(UpdateUserRequest.Email)))
                .MustAsync(EmailNotAlreadyExists)
                .WithMessage(ValidationMessages.GetItsInUse(nameof(UpdateUserRequest.Email)));

            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage(ValidationMessages.GetRequired(nameof(UpdateUserRequest.Name)))
                .MinimumLength(User.NameMinLength)
                .WithMessage(ValidationMessages.GetTooShort(nameof(UpdateUserRequest.Name)))
                .MaximumLength(User.NameMaxLength)
                .WithMessage(ValidationMessages.GetTooLong(nameof(UpdateUserRequest.Name)));

            RuleFor(u => u.Birthdate)
                .Must(BeAValidDate)
                .WithMessage(ValidationMessages.GetOutOfRange(nameof(UpdateUserRequest.Birthdate)));
            RuleFor(u => u.GenderId)
                .NotEmpty();
        }

        private async Task<bool> EmailNotAlreadyExists(UpdateUserRequest updateUser, string email, CancellationToken cancellationToken)
        {
            var existsCount = await _emailAlreadyExistsCount.Query(email, 0, cancellationToken);
            return existsCount == 0;
        }

        private async Task<bool> ValidEmail(UpdateUserRequest updateUser, string email, CancellationToken cancellationToken)
        {
            var match = User.EmailRegex.Match(email);
            return await Task.FromResult(match.Success);
        }
        
        private bool BeAValidDate(DateTime date)
        {
            return date < User.BirthdateMaxDate && date > User.BirthdateMinDate;
        }
    }

    public sealed class UpdateUserHandler : IRequestHandler<UpdateUserRequest>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<Unit> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdWithIncludes(request.Id, cancellationToken);

            // TODO: Add validation to checking existing indices (This line is not at all definitive, only for test purposes)
            if (user == null) throw new System.Exception($"The requested identifier {request.Id} to Update doesn't exists");

            user.Update(
                request.Id,
                request.Email,
                request.Name,
                request.Birthdate,
                request.GenderId);

            return Unit.Value;
        }
    }
}
