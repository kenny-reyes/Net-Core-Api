namespace DotNetCqrsApi.Application.Person.Responses
{
    public class SectionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private SectionModel()
        { }

        public static SectionModel Create(int id, string name) => new SectionModel { Id = id, Name = name };
    }
}
