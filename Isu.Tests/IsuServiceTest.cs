using System;
using System.Runtime.ConstrainedExecution;
using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3200");
            Student student = _isuService.AddStudent(group, "Misha");
            
            Assert.Contains(student, group.ListOfStudents);
            Assert.AreEqual(student.GroupName, group.Name);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var group = _isuService.AddGroup("M3200");

                for (int i = 0; i < 20; i++)
                    _isuService.AddStudent(group, "Misha");
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var group = _isuService.AddGroup("Y322222");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            var group = _isuService.AddGroup("M3200");
            var newGroup = _isuService.AddGroup("M3201");

            var student = _isuService.AddStudent(group, "Misha");
            _isuService.ChangeStudentGroup(student, newGroup);
            
            Assert.AreEqual(newGroup.Name, student.GroupName);
        }
    }
}