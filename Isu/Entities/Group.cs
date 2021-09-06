using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private const int MaxStudentsNumber = 25; // максимальное число студентов в группе

        public Group(string name)
        {
            if (!IsNameValid(name)) throw new IsuException($"{name} is invalid for the group!");
            Name = name;
            Course = name[2];
            Students = new List<Student>();
        }

        public List<Student> Students { get; }
        public string Name { get; }

        public int Course { get; }

        public void AddStudent(Student student)
        {
            if (Students.Count > MaxStudentsNumber)
                throw new IsuException($"Can't fit {student.Name} in {Name}");

            Students.Add(student);
        }

        public Student FindStudent(string name)
        {
            Student found = Students.FirstOrDefault(student => student.Name == name);
            if (found == null) throw new IsuException($"No such student {name} in group {Name}");
            return found;
        }

        public Student FindStudent(int id)
        {
            Student found = Students.FirstOrDefault(student => student.Id == id);
            if (found == null) throw new IsuException($"No such student with id {id} in group {Name}");
            return found;
        }

        private static bool IsNameValid(string name)
        {
            return name[0] == 'M' && name[1] == '3' && name[2] >= '1' && name[2] <= '4' && name.Length < 6;
        }
    }
}