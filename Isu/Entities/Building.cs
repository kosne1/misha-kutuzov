using System.Collections.Generic;

namespace Isu.Entities
{
    public class Building
    {
        public Building(int numberOfClassrooms)
        {
            Classrooms = new List<Classroom>();
            for (int i = 1; i <= numberOfClassrooms; i++)
                Classrooms.Add(new Classroom(i));
        }

        public List<Classroom> Classrooms { get; }

        public Classroom GetClassroom(int number)
        {
            return Classrooms.Find(c => c.Number == number);
        }
    }
}