namespace ApiExercise.Application.Users.ResponseModels
{
    public class UserListItemModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Birthdate { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; }
    }
}
