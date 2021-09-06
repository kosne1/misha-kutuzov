using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        public Group(string name)
        {
            if (!IsNameInvalid(name)) throw new IsuException($"{name} is invalid for the group!");
            Course = name[2];
            ListOfStudents = new List<Student>();
        }

        public List<Student> ListOfStudents { get; private set; }
        public string Name { get; private set; }

        public int Course { get; private set; }

        public void AddStudent(Student student)
        {
            if (ListOfStudents.Count > 10)
                throw new IsuException($"Can't fit {student.Name} in {Name}");

            ListOfStudents.Add(student);
        }

        public Student FindStudent(string name)
        {
            Student found = ListOfStudents.Find(student => student.Name == name);
            return found;
        }

        public Student FindStudent(int id)
        {
            Student found = ListOfStudents.Find(student => student.Id == id);
            return found;
        }

        private static bool IsNameInvalid(string name)
        {
            return name[0] == 'M' && name[1] == '3' && name[2] >= '1' && name[2] <= '4' && name.Length < 6;
        }
    }
}