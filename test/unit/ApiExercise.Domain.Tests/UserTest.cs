using System;
using ApiExercise.Domain.Exceptions;
using ApiExercise.Domain.Users;
using ApiExercise.Domain.Validations;
using FluentAssertions;
using Xunit;

namespace ApiExercise.Domain.Tests
{
    public class UserTest
    {
        private readonly string _validEmail = "keniakos@hotmail.com";
        private readonly string _validName = new string('a', User.NameMaxLength);
        private readonly DateTime _validBirthdate = new DateTime(1982,1,1);
        private readonly Gender _validGender = Gender.Male;

        private readonly string _nullStringParameter = null;
        private readonly string _tooLongName = new string('a', User.NameMaxLength + 1);

        /*
        NOTE: I was using the convention When_ItShould basically
        When You do something the result Should be the another one
        The name of a test Method could be something like this
            public void WhenEncryptTextWithNullStringItShouldReturnEmpty()
        But I won't write anymore the "When" & "ItShould" string and but I will 
        maintain the structure .
        https://dzone.com/articles/7-popular-unit-test-naming
        
        I also use the AAA pattern to organize the unit test
        https://medium.com/@pjbgf/title-testing-code-ocd-and-the-aaa-pattern-df453975ab80        
        */
        /*
                public int Id { get; set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public DateTime Birthdate { get; private set; }
        public int GenderId { get; private set; }
        public Gender Gender { get; private set; }
        */

        [Fact]
        public void User_CreatingAUserWithValidParameters_BeCreatedSuccessfully()
        {
            var user = User.Create(
                _validEmail, _validName, _validBirthdate, _validGender.Id);

            user.Name.Should().Be(_validName);
            user.Email.Should().Be(_validEmail);
            user.Birthdate.Should().Be(_validBirthdate);
            user.GenderId.Should().Be(_validGender.Id);
        }
    }
}