namespace DotNetCqrsApi.Application.Person.Responses
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
