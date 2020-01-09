using System.Collections.Generic;
using System.Linq;
using DotNetCqrsApi.Domain.Exceptions;
using DotNetCqrsApi.Domain.Interfaces;

namespace DotNetCqrsApi.Domain.People
{
    public class Gender : IEntity
    {
        public const int NameMaxLength = 10;

        public int Id { get; private set; } 
        public string Name { get; private set; }

        public readonly static Gender Male = new Gender(1, nameof(Male));
        public readonly static Gender Female = new Gender(2, nameof(Female));
        public readonly static Gender Unknown = new Gender(3, nameof(Unknown));

        private Gender(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IReadOnlyCollection<Gender> GetGenders() =>
            new[] { Male, Female, Unknown };

        public static Gender FindByName(string name)
        {
            var gender = GetGenders().SingleOrDefault(r => r.Name == name);

            if (gender == null)
            {
                throw new DomainException($"Possible values for {nameof(Gender)}: {string.Join(",", GetGenders().Select(s => s.Name))}");
            }
            return gender;
        }

        public static Gender FindById(int id)
        {
            var gender = GetGenders().SingleOrDefault(r => r.Id == id);

            if (gender == null)
            {
                throw new DomainException($"Possible values for  {nameof(Gender)}: {string.Join(",", GetGenders().Select(s => s.Name))}");
            }
            return gender;
        }
    }
}
