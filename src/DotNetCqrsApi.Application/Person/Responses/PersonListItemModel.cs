namespace DotNetCqrsApi.Application.Person.Responses
{
    public class PersonListItemModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; }
    }
}
