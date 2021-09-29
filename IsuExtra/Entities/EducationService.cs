using System.Collections.Generic;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class EducationService
    {
        private int _teachersId = 1;

        public EducationService()
        {
            Teachers = new List<Teacher>();
        }

        public List<Teacher> Teachers { get; }

        public Teacher AddTeacher(string name)
        {
            var teacher = new Teacher(name, _teachersId++);
            Teachers.Add(teacher);
            return teacher;
        }
    }
}