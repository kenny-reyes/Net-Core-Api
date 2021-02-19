using System;
using System.Linq;
using System.Text.RegularExpressions;
using NetCoreApiScaffolding.Domain.Interfaces;
using NetCoreApiScaffolding.Domain.Validations;

namespace NetCoreApiScaffolding.Domain.Users
{
    public class User : IEntity
    {
        // NOTE: I think does worth to put this in a configuration file
        private const int MinimumBornYear = 1900;

        public static readonly int NameMaxLength = 20;
        public static readonly int NameMinLength = 2;
        public static DateTime BirthdateMaxDate => DateTime.Now;
        public static readonly DateTime BirthdateMinDate = new DateTime(MinimumBornYear,1,1);
        public static readonly int EmailMaxLength = 64;
        public static readonly Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$", RegexOptions.Compiled);

        public int Id { get; set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public DateTime Birthdate { get; private set; }
        public int GenderId { get; private set; }
        public Gender Gender { get; private set; }

        private User()
        { }

        public static User Create(
            string email,
            string name,
            DateTime birthdate,
            int gender)
        {
            ValidateEmail(email);
            ValidateName(name);
            ValidateBirthdate(birthdate);
            ValidateGender(gender);

            var user = new User
            {
                Email = email,
                Name = name,
                Birthdate = birthdate,
                GenderId = gender
            };

            return user;
        }

        public void Update(
            int id,
            string email,
            string name,
            DateTime birthdate,
            int genderId)
        {
            ValidateEmail(email);
            ValidateName(name);
            ValidateBirthdate(birthdate);
            ValidateGender(genderId);

            Email = email;
            Name = name;
            Birthdate = birthdate;
            GenderId = genderId;
        }

        private static void ValidateEmail(string email)
        {
            DomainPreconditions.NotEmpty(email, nameof(Email));
            DomainPreconditions.RegexMatch(email, EmailRegex, nameof(Email));
            DomainPreconditions.LongerThan(email, EmailMaxLength, nameof(Email));
        }

        private static void ValidateName(string name)
        {
            DomainPreconditions.NotEmpty(name, nameof(name));
            DomainPreconditions.LongerThan(name, NameMaxLength, nameof(Name));
            DomainPreconditions.ShorterThan(name, NameMinLength, nameof(Name));
        }

        private static void ValidateBirthdate(DateTime birthdate)
        {
            DomainPreconditions.EarlierThan(birthdate, BirthdateMaxDate, nameof(Birthdate));
            DomainPreconditions.LaterThan(birthdate, BirthdateMinDate, nameof(Birthdate));
        }

        private static void ValidateGender(int genderId) =>
            DomainPreconditions.IsIntInIntList(Gender.GetGenders().Select(r => r.Id), genderId, nameof(genderId));
    }
}
