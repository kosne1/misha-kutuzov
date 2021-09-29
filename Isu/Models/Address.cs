using Isu.Entities;

namespace Isu.Models
{
    public class Address
    {
        public Address(Building building, Classroom classroom)
        {
            Building = building;
            Classroom = classroom;
        }

        public Building Building { get; }
        public Classroom Classroom { get; }
    }
}