using DotNetCqrsApi.Domain.People;

namespace DotNetCqrsApi.Application.People.Responses
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
    }
}
