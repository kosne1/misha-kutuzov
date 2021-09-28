using System.Collections.Generic;

namespace IsuExtra.Entities
{
    public class Building
    {
        public Building(string address, int numberOfClassrooms)
        {
            Address = address;
            Classrooms = new List<Classroom>();
            for (int i = 1; i <= numberOfClassrooms; i++)
                Classrooms.Add(new Classroom(i));
        }

        public string Address { get; }
        public List<Classroom> Classrooms { get; }

        public Classroom GetClassroom(int number)
        {
            return Classrooms.Find(c => c.Number == number);
        }
    }
}