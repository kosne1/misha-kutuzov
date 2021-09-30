using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private const int MaxStudentsNumber = 25;
        private readonly List<Student> _students;

        public Group(string name)
        {
            if (IsNameValid(name)) throw new IsuException("Group name can't be empty!");
            Name = name;
            Course = (int)char.GetNumericValue(name[2]);
            _students = new List<Student>();
            Schedule = new Schedule();
        }

        public IReadOnlyCollection<Student> Students => _students;
        public string Name { get; }
        public int Course { get; }
        public Schedule Schedule { get; }

        public void AddStudent(Student student)
        {
            if (Students.Count == MaxStudentsNumber)
                throw new IsuException($"Can't fit {student.Name} in {Name}");

            _students.Add(student);
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

        public void RemoveStudent(Student student)
        {
            _students.Remove(student);
        }

        private static bool IsNameValid(string name)
        {
            return string.IsNullOrEmpty(name);
        }
    }
}