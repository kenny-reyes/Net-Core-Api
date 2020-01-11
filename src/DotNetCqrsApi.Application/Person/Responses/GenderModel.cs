namespace DotNetCqrsApi.Application.Person.Responses
{
    public class GenderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private GenderModel()
        { }

        public static GenderModel Create(int id, string name) => new GenderModel { Id = id, Name = name };
    }
}
