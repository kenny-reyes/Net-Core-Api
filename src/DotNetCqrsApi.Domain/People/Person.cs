using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DotNetCqrsApi.Domain.Interfaces;
using DotNetCqrsApi.Domain.Validations;

namespace DotNetCqrsApi.Domain.People
{
    public class Person : IEntity
    {
        public const int NameMaxLength = 20;
        public const int NameMinLength = 2;
        public const int SurnameMaxLength = 30;
        public const int SurnameMinLength = 2;
        public const int EmailMaxLenght = 64;

        public static Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$", RegexOptions.Compiled);

        public int Id { get; set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public int GenderId { get; private set; }
        public Gender Gender { get; private set; }

        private Person()
        { }

        public static Person Create(
            string email,
            string name,
            string surname,
            int gender)
        {
            ValidateEmail(email);
            ValidateName(name);
            ValidateSurname(surname);
            ValidateGender(gender);

            var person = new Person
            {
                Email = email,
                Name = name,
                Surname = surname,
                GenderId = gender
            };

            return person;
        }

        public void Update(
            int id,
            string email,
            string name,
            string surname,
            int genderId)
        {
            ValidateEmail(email);
            ValidateName(name);
            ValidateSurname(surname);
            ValidateGender(genderId);

            Email = email;
            Name = name;
            Surname = surname;
            GenderId = genderId;
        }

        private static void ValidateEmail(string email)
        {
            DomainPreconditions.NotEmpty(email, nameof(Email));
            DomainPreconditions.RegexMatch(email, EmailRegex, nameof(Email));
            DomainPreconditions.LongerThan(email, EmailMaxLenght, nameof(Email));
        }

        private static void ValidateName(string name)
        {
            DomainPreconditions.NotEmpty(name, nameof(name));
            DomainPreconditions.LongerThan(name, NameMaxLength, nameof(Name));
            DomainPreconditions.ShorterThan(name, NameMinLength, nameof(Name));
        }

        private static void ValidateSurname(string surname)
        {
            DomainPreconditions.NotEmpty(surname, nameof(Surname));
            DomainPreconditions.LongerThan(surname, SurnameMaxLength, nameof(Surname));
            DomainPreconditions.ShorterThan(surname, SurnameMinLength, nameof(Surname));
        }

        private static void ValidateGender(int genderId) =>
            DomainPreconditions.IsIntInIntList(Gender.GetGenders().Select(r => r.Id), genderId, nameof(genderId));
    }
}
