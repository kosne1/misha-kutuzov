using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Models;

namespace Isu.Services
{
    public class GroupService : IIsuService
    {
        private readonly List<Group> _groups;
        private int _studentsIds = 1;

        public GroupService()
        {
            _groups = new List<Group>();
        }

        public Group AddGroup(string name)
        {
            var group = new Group(name);
            _groups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(name, _studentsIds++);
            group.AddStudent(student);
            return student;
        }

        public Student AddStudent(Group group, Student student)
        {
            group.AddStudent(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            return _groups.Select(isuGroup => isuGroup.FindStudent(id)).FirstOrDefault(found => found != null);
        }

        public Student FindStudent(string name)
        {
            return _groups.Select(isuGroup => isuGroup.FindStudent(name)).FirstOrDefault(found => found != null);
        }

        public List<Student> FindStudents(string groupName)
        {
            return (from isuGroup in _groups where isuGroup.Name.Equals(groupName) select isuGroup.Students).First()
                .ToList();
        }

        public List<Student> FindStudents(int courseNumber)
        {
            var students = new List<Student>();

            IEnumerable<Group> foundGroups = _groups.Where(isuGroup => isuGroup.Course == courseNumber);

            foreach (Group isuGroup in foundGroups)
            {
                students.AddRange(isuGroup.Students);
            }

            return students;
        }

        public Group FindGroup(string groupName)
        {
            return _groups.FirstOrDefault(isuGroup => isuGroup.Name == groupName);
        }

        public List<Group> FindGroups(int courseNumber)
        {
            return _groups.Where(isuGroup => isuGroup.Course == courseNumber).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            foreach (Group isuGroup in _groups)
            {
                foreach (Student isuGroupStudent in isuGroup.Students.ToList())
                {
                    if (isuGroupStudent.Id == student.Id)
                    {
                        RemoveStudent(isuGroup, isuGroupStudent);
                    }
                }
            }

            newGroup.AddStudent(student);
        }

        public void RemoveStudent(Group group, Student student)
        {
            group.RemoveStudent(student);
        }
    }
}