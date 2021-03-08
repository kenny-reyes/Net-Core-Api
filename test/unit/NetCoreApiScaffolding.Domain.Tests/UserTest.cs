using System;
using NetCoreApiScaffolding.Domain.Users;
using NetCoreApiScaffolding.Domain.Validations;
using NetCoreApiScaffolding.Tools.Exceptions;
using FluentAssertions;
using Xunit;

namespace NetCoreApiScaffolding.Domain.Tests
{
    public class UserTest
    {
        private const string ValidEmail = "keniakos@hotmail.com";
        private readonly string _validName = new string('a', User.NameMaxLength);
        private readonly DateTime _validBirthdate = new DateTime(1982, 1, 1);
        private readonly Gender _validGender = Gender.Male;

        private readonly string _nullStringParameter = null;
        private readonly string _tooLongName = new string('a', User.NameMaxLength + 1);
        private readonly string _tooShortName = new string('a', User.NameMinLength - 1);
        private readonly string _tooLongEmail = new string('a', User.EmailMaxLength - ValidEmail.Length + 1) + ValidEmail;
        private readonly string _notValidEmail = new string("not.valid.em@il");
        private readonly DateTime _tooLateBirthdate = User.BirthdateMaxDate.AddDays(1);
        private readonly DateTime _tooEarlyBirthdate = User.BirthdateMinDate.AddDays(-1);

        [Fact]
        public void User_CreatingAUserWithValidParameters_BeCreatedSuccessfully()
        {
            var user = User.Create(
                ValidEmail, _validName, _validBirthdate, _validGender.Id);

            user.Name.Should().Be(_validName);
            user.Email.Should().Be(ValidEmail);
            user.Birthdate.Should().Be(_validBirthdate);
            user.GenderId.Should().Be(_validGender.Id);
        }

        [Fact]
        public void User_CreatingAUserWithANullEmail_AExceptionShouldBeThrown()
        {
            Action action = () => User.Create(
                _nullStringParameter, _validName, _validBirthdate, _validGender.Id);

            action.Should().Throw<DomainException>().WithMessage(DomainPreconditionMessages.GetNotEmpty(nameof(User.Email)));
        }

        [Fact]
        public void User_CreatingAUserWithATooLongEmail_AExceptionShouldBeThrown()
        {
            Action action = () => User.Create(
                _tooLongEmail, _validName, _validBirthdate, _validGender.Id);

            action.Should().Throw<DomainException>()
                .WithMessage(DomainPreconditionMessages.GetLongerThan(User.EmailMaxLength, nameof(User.Email)));
        }

        [Fact]
        public void User_CreatingAUserWithANotRightFormat_AExceptionShouldBeThrown()
        {
            Action action = () => User.Create(
                _notValidEmail, _validName, _validBirthdate, _validGender.Id);

            action.Should().Throw<DomainException>().WithMessage(DomainPreconditionMessages.GetSuccessMatch(nameof(User.Email)));
        }

        [Fact]
        public void User_CreatingAUserWithANullName_AExceptionShouldBeThrown()
        {
            Action action = () => User.Create(
                ValidEmail, _nullStringParameter, _validBirthdate, _validGender.Id);

            action.Should().Throw<DomainException>().WithMessage(DomainPreconditionMessages.GetNotEmpty(nameof(User.Name)));
        }

        [Fact]
        public void User_CreatingAUserWithATooLongName_AExceptionShouldBeThrown()
        {
            Action action = () => User.Create(
                ValidEmail, _tooLongName, _validBirthdate, _validGender.Id);

            action.Should().Throw<DomainException>()
                .WithMessage(DomainPreconditionMessages.GetLongerThan(User.NameMaxLength, nameof(User.Name)));
        }

        [Fact]
        public void User_CreatingAUserWithATooShortName_AExceptionShouldBeThrown()
        {
            Action action = () => User.Create(
                ValidEmail, _tooShortName, _validBirthdate, _validGender.Id);

            action.Should().Throw<DomainException>()
                .WithMessage(DomainPreconditionMessages.GetShorterThan(User.NameMinLength, nameof(User.Name)));
        }

        [Fact]
        public void User_CreatingAUserWithATooEarlyBirthday_AExceptionShouldBeThrown()
        {
            Action action = () => User.Create(
                ValidEmail, _validName, _tooEarlyBirthdate, _validGender.Id);

            action.Should().Throw<DomainException>()
                .WithMessage(DomainPreconditionMessages.GetLaterThan(User.BirthdateMinDate, nameof(User.Birthdate)));
        }

        [Fact]
        public void User_CreatingAUserWithATooLateBirthday_AExceptionShouldBeThrown()
        {
            Action action = () => User.Create(
                ValidEmail, _validName, _tooLateBirthdate, _validGender.Id);

            action.Should().Throw<DomainException>()
                .WithMessage(DomainPreconditionMessages.GetEarlierThan(User.BirthdateMaxDate, nameof(User.Birthdate)));
        }

        /*
         * TODO: Now we have to do the same with Update method
         * Basically copy the existing ones and use the update
         */
    }
}