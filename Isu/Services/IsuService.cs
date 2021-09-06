using System.Collections.Generic;
using System.Linq;
using Isu.Entities;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private List<Group> _isuGroups;
        private int _studentsCounter;

        public IsuService()
        {
            _isuGroups = new List<Group>();
            _studentsCounter = 1;
        }

        public Group AddGroup(string name)
        {
            var group = new Group(name);
            _isuGroups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(name, _studentsCounter++, group.Name);
            group.AddStudent(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            return _isuGroups.Select(isuGroup => isuGroup.FindStudent(id)).FirstOrDefault(found => found != null);
        }

        public Student FindStudent(string name)
        {
            return _isuGroups.Select(isuGroup => isuGroup.FindStudent(name)).FirstOrDefault(found => found != null);
        }

        public List<Student> FindStudents(string groupName)
        {
            return (from isuGroup in _isuGroups where isuGroup.Name.Equals(groupName) select isuGroup.ListOfStudents)
                .FirstOrDefault();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var students = new List<Student>();

            foreach (var isuGroup in _isuGroups.Where(isuGroup => isuGroup.Course == courseNumber.Number))
            {
                students.AddRange(isuGroup.ListOfStudents);
            }

            return students;
        }

        public Group FindGroup(string groupName)
        {
            return _isuGroups.FirstOrDefault(isuGroup => isuGroup.Name == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _isuGroups.Where(isuGroup => isuGroup.Course == courseNumber.Number).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            newGroup.AddStudent(student);

            foreach (var isuGroup in _isuGroups.Where(isuGroup => isuGroup.Name == student.GroupName))
            {
                isuGroup.ListOfStudents.Remove(student);
            }

            student.GroupName = newGroup.Name;
        }
    }
}