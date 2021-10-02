using System.Linq;
using Isu.Entities;
using Isu.Models;
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
            _isuService = new GroupService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3200");
            Student student = _isuService.AddStudent(group, "Misha");

            Assert.Contains(student, group.Students.ToList());
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3200");

                for (int i = 0; i < 50; i++)
                    _isuService.AddStudent(group, "Misha");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group group = _isuService.AddGroup("M3200");
            Group newGroup = _isuService.AddGroup("M3201");

            Student student = _isuService.AddStudent(group, "Misha");
            _isuService.ChangeStudentGroup(student, newGroup);

            Assert.Contains(student, newGroup.Students.ToList());
        }
    }
}