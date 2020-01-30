using System;
using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Common;
using ApiExercise.Application.Common.Queries;
using ApiExercise.Domain.Users;
using FluentValidation;

namespace ApiExercise.Application.Users.UpdateUser
{
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
}