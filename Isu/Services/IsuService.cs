using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Models;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly List<Group> _isuGroups;
        private int _studentsCounter = 1;

        public IsuService()
        {
            _isuGroups = new List<Group>();
        }

        public Group AddGroup(string name)
        {
            var group = new Group(name);
            _isuGroups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(name, _studentsCounter++);
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
            return _isuGroups.Select(isuGroup => isuGroup.FindStudent(id)).FirstOrDefault(found => found != null);
        }

        public Student FindStudent(string name)
        {
            return _isuGroups.Select(isuGroup => isuGroup.FindStudent(name)).FirstOrDefault(found => found != null);
        }

        public List<Student> FindStudents(string groupName)
        {
            return (from isuGroup in _isuGroups where isuGroup.Name.Equals(groupName) select isuGroup.Students)
                .FirstOrDefault();
        }

        public List<Student> FindStudents(int courseNumber)
        {
            var students = new List<Student>();

            IEnumerable<Group> foundGroups = _isuGroups.Where(isuGroup => isuGroup.Course == courseNumber);

            foreach (Group isuGroup in foundGroups)
            {
                students.AddRange(isuGroup.Students);
            }

            return students;
        }

        public Group FindGroup(string groupName)
        {
            return _isuGroups.FirstOrDefault(isuGroup => isuGroup.Name == groupName);
        }

        public List<Group> FindGroups(int courseNumber)
        {
            return _isuGroups.Where(isuGroup => isuGroup.Course == courseNumber).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            foreach (Group isuGroup in from isuGroup in _isuGroups
                let foundStudent = isuGroup.Students.Find(s => s.Id == student.Id)
                where foundStudent != null
                select isuGroup)
            {
                isuGroup.RemoveStudent(student);
                AddStudent(newGroup, student);
            }
        }
    }
}