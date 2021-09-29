using Isu.Entities;
using IsuExtra.Entities;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class IsuExtraTest
    {
        private IsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            MegaFaculty tint = _isuService.AddMegaFaculty("MEGAFACULTY OF TRANSLATIONAL INFORMATION TECHNOLOGIES");
            EducationalProgram informationSystems =
                _isuService.AddEducationalProgram(tint, "Programming and Internet technologies");
            Group group = informationSystems.GroupService.AddGroup("M3200");
            Student student = informationSystems.GroupService.AddStudent(group, "Mikhail Kutuzov");

            var kronva = new Building(400);
            Teacher teacher = tint.EducationService.AddTeacher("Fredi Kats");
            ElectiveModule electiveModule = tint.AddElectiveModule("Cyber Security");
            
        }
    }
}