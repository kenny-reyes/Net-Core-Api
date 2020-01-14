using System.Threading;
using System.Threading.Tasks;
using DotNetCqrsApi.Application.People.Queries;
using DotNetCqrsApi.Application.People.Responses;
using DotNetCqrsApi.Application.Shared;
using DotNetCqrsApi.Domain.People;
using DotNetCqrsApi.Domain.Repositories.People;
using FluentValidation;
using MediatR;

namespace DotNetCqrsApi.Application.People
{
    public sealed class CreatePersonRequest : IRequest<PersonModel>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int GenderId { get; set; }
    }

    public sealed class CreatePersonValidator : AbstractValidator<CreatePersonRequest>
    {
        private readonly IEmailAlreadyExistsCount _emailAlreadyExistsCount;

        public CreatePersonValidator(IEmailAlreadyExistsCount emailAlreadyExistsCount)
        {
            _emailAlreadyExistsCount = emailAlreadyExistsCount;

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage(ValidationMessages.GetRequired(nameof(CreatePersonRequest.Email)))
                .MaximumLength(Person.EmailMaxLenght)
                .WithMessage(ValidationMessages.GetTooLong(nameof(CreatePersonRequest.Email)))
                .MustAsync(ValidEmail)
                .WithMessage(ValidationMessages.GetValidRequired(nameof(CreatePersonRequest.Email)))
                .MustAsync(EmailNotAlreadyExists)
                .WithMessage(ValidationMessages.GetItsInUse(nameof(CreatePersonRequest.Email)));

            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage(ValidationMessages.GetRequired(nameof(CreatePersonRequest.Name)))
                .MinimumLength(Person.NameMinLength)
                .WithMessage(ValidationMessages.GetTooShort(nameof(CreatePersonRequest.Name)))
                .MaximumLength(Person.NameMaxLength)
                .WithMessage(ValidationMessages.GetTooLong(nameof(CreatePersonRequest.Name)));

            RuleFor(u => u.Surname)
                .NotEmpty()
                .WithMessage(ValidationMessages.GetRequired(nameof(CreatePersonRequest.Surname)))
                .MinimumLength(Person.SurnameMinLength)
                .WithMessage(ValidationMessages.GetTooShort(nameof(CreatePersonRequest.Surname)))
                .MaximumLength(Person.SurnameMaxLength)
                .WithMessage(ValidationMessages.GetTooLong(nameof(CreatePersonRequest.Surname)));

            RuleFor(u => u.GenderId)
                .NotEmpty();
        }

        private async Task<bool> EmailNotAlreadyExists(CreatePersonRequest createPerson, string email, CancellationToken cancellationToken)
        {
            var existsCount = await _emailAlreadyExistsCount.Query(email, 0, cancellationToken);
            return existsCount == 0;
        }

        private async Task<bool> ValidEmail(CreatePersonRequest createPerson, string email, CancellationToken cancellationToken)
        {
            var match = Person.EmailRegex.Match(email);
            return await Task.FromResult(match.Success);
        }
    }

    public sealed class CreatePersonHandler : IRequestHandler<CreatePersonRequest, PersonModel>
    {
        private readonly IPersonRepository _personRepository;

        public CreatePersonHandler(IPersonRepository personRepository) => _personRepository = personRepository;

        public async Task<PersonModel> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
        {
            var person = Person.Create(
                request.Email,
                request.Name,
                request.Surname,
                request.GenderId);

            await _personRepository.Add(person, cancellationToken);
            return new PersonModel
            {
                Id = person.Id,
                Email = person.Email,
                Name = person.Name,                
                Surname = person.Surname,   
                GenderId = person.GenderId
            };
        }
    }
}
