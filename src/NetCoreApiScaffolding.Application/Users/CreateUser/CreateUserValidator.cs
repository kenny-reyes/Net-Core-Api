using System;
using System.Threading;
using System.Threading.Tasks;
using NetCoreApiScaffolding.Application.Common;
using NetCoreApiScaffolding.Domain.Users;
using FluentValidation;
using NetCoreApiScaffolding.Application.Interfaces.Queries;

namespace NetCoreApiScaffolding.Application.Users.CreateUser
{
    public sealed class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        private readonly IEmailAlreadyExistsCount _emailAlreadyExistsCount;

        public CreateUserValidator(IEmailAlreadyExistsCount emailAlreadyExistsCount)
        {
            _emailAlreadyExistsCount = emailAlreadyExistsCount;

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage(ValidationMessages.GetRequired(nameof(CreateUserRequest.Email)))
                .MaximumLength(User.EmailMaxLength)
                .WithMessage(ValidationMessages.GetTooLong(nameof(CreateUserRequest.Email)))
                .MustAsync(ValidEmail)
                .WithMessage(ValidationMessages.GetValidRequired(nameof(CreateUserRequest.Email)))
                .MustAsync(EmailNotAlreadyExists)
                .WithMessage(ValidationMessages.GetItsInUse(nameof(CreateUserRequest.Email)));

            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage(ValidationMessages.GetRequired(nameof(CreateUserRequest.Name)))
                .MinimumLength(User.NameMinLength)
                .WithMessage(ValidationMessages.GetTooShort(nameof(CreateUserRequest.Name)))
                .MaximumLength(User.NameMaxLength)
                .WithMessage(ValidationMessages.GetTooLong(nameof(CreateUserRequest.Name)));

            RuleFor(u => u.Birthdate)
                .Must(BeAValidDate)
                .WithMessage(ValidationMessages.GetOutOfRange(nameof(CreateUserRequest.Birthdate)));
            RuleFor(u => u.GenderId)
                .NotEmpty();
        }

        private async Task<bool> EmailNotAlreadyExists(string email, CancellationToken cancellationToken)
        {
            var existsCount = await _emailAlreadyExistsCount.Query(email, 0, cancellationToken);
            return existsCount == 0;
        }

        private async Task<bool> ValidEmail(CreateUserRequest createUser, string email, CancellationToken cancellationToken)
        {
            var match = User.EmailRegex.Match(email);
            return await Task.FromResult(match.Success);
        }

        private bool BeAValidDate(DateTime date)
        {
            return date < User.BirthdateMaxDate && date > User.BirthdateMinDate;
        }
    }
}