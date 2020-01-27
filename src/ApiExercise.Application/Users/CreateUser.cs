using System;
using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Repositories.Users;
using ApiExercise.Application.Users.Queries;
using ApiExercise.Application.Shared;
using ApiExercise.Application.Users.ResponseModels;
using ApiExercise.Domain.Users;
using FluentValidation;
using MediatR;

namespace ApiExercise.Application.Users
{
    /*
     * NOTE: yes, looks ugly to have 3 classes in the same file, but seems it is like the people do it,
     * well, there is a discussion about this, I don't care to divide it, I don't have an opinion
     */
    public sealed class CreateUserRequest : IRequest<UserResponseModel>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public int GenderId { get; set; }
    }

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

        private async Task<bool> EmailNotAlreadyExists(CreateUserRequest createUser, string email, CancellationToken cancellationToken)
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

    public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, UserResponseModel>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<UserResponseModel> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = User.Create(
                request.Email,
                request.Name,
                request.Birthdate,
                request.GenderId);

            await _userRepository.Add(user, cancellationToken);
            return new UserResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,                
                Birthdate = user.Birthdate,   
                GenderId = user.GenderId
            };
        }
    }
}
