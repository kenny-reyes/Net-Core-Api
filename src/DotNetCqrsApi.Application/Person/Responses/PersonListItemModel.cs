namespace DotNetCqrsApi.Application.Person.Responses
{
    public class PersonListItemModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int Countries { get; set; }
        public int Sections { get; set; }
        public int ModuleId { get; set; }
        public bool IsEnabled { get; set; }
    }
}
