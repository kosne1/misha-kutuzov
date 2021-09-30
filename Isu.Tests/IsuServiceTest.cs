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
        private IGroupService _groupService;

        [SetUp]
        public void Setup()
        {
            _groupService = new GroupService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _groupService.AddGroup("M3200");
            Student student = _groupService.AddStudent(group, "Misha");

            Assert.Contains(student, group.Students.ToList());
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _groupService.AddGroup("M3200");

                for (int i = 0; i < 50; i++)
                    _groupService.AddStudent(group, "Misha");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group group = _groupService.AddGroup("M3200");
            Group newGroup = _groupService.AddGroup("M3201");

            Student student = _groupService.AddStudent(group, "Misha");
            _groupService.ChangeStudentGroup(student, newGroup);

            Assert.Contains(student, newGroup.Students.ToList());
        }
    }
}