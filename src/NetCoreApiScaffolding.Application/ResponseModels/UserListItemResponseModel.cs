﻿namespace NetCoreApiScaffolding.Application.ResponseModels
{
    public class UserListItemResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Birthdate { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; }
    }
}