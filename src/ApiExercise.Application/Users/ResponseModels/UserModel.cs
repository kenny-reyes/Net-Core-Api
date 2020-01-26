using System;
using ApiExercise.Domain.Users;

namespace ApiExercise.Application.Users.ResponseModels
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
    }
}
