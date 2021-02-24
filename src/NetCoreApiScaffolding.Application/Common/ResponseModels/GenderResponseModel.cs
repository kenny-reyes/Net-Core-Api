namespace NetCoreApiScaffolding.Application.Common.ResponseModels
{
    public class GenderResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private GenderResponseModel()
        {
        }

        public static GenderResponseModel Create(int id, string name) => new GenderResponseModel {Id = id, Name = name};
    }
}