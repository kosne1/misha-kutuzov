using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Services;
using IsuExtra.Entities;
using IsuExtra.Services;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class IsuExtraTest
    {
        private IsuService _isuService;
        private EducationService _educationService;
        private GroupService _groupService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
            _educationService = new EducationService();
            _groupService = new GroupService();
        }

        [Test]
        public void AddElectiveModule_ElectiveModuleIsInMegaFaculty()
        {
            MegaFaculty ktu = _isuService.AddMegaFaculty("MEGAFACULTY OF COMPUTER TECHNOLOGY AND MANAGEMENT");

            ElectiveModule cyberSecurityBasics = _educationService.AddElectiveModule(ktu, "Cyber Security Basics");

            Assert.AreEqual(cyberSecurityBasics, ktu.ElectiveModule);
        }

        [Test]
        public void AddElectiveModule_StudentCanEnrollElectiveModule()
        {
            MegaFaculty tint = _isuService.AddMegaFaculty("MEGAFACULTY OF TRANSLATIONAL INFORMATION TECHNOLOGIES");
            MegaFaculty ktu = _isuService.AddMegaFaculty("MEGAFACULTY OF COMPUTER TECHNOLOGY AND MANAGEMENT");

            EducationalProgram informationSystems =
                _educationService.AddEducationalProgram(tint, "Programming and Internet technologies");
            EducationalProgram cyberSecurityOfSystems =
                _educationService.AddEducationalProgram(ktu, "Cyber Security of Systems");

            Teacher alex = _educationService.AddTeacher("Alex Rubanov");

            ElectiveModule cyberSecurityBasics = _educationService.AddElectiveModule(ktu, "Cyber Security Basics");

            Lesson cyberSecurityBasicsLecture =
                _educationService.AddLesson("MOD", alex, "Kronva", new DateTime(2021, 10, 1, 10, 0, 0));

            Stream cyberSecurityBasicsStream =
                _educationService.AddStream(cyberSecurityBasics, cyberSecurityBasicsLecture);
            Group m3200 = _groupService.AddGroup("M3200");
            Student mishaKutuzov = _groupService.AddStudent(m3200, "Mikhail Kutuzov");

            _groupService.AddStudent(cyberSecurityBasicsStream.Group, mishaKutuzov);

            Assert.Contains(mishaKutuzov, cyberSecurityBasicsStream.Group.Students.ToList());
        }

        [Test]
        public void AddStudentToElectiveModule_StudentRemovedFromElectiveModule()
        {
            MegaFaculty tint = _isuService.AddMegaFaculty("MEGAFACULTY OF TRANSLATIONAL INFORMATION TECHNOLOGIES");
            MegaFaculty ktu = _isuService.AddMegaFaculty("MEGAFACULTY OF COMPUTER TECHNOLOGY AND MANAGEMENT");

            EducationalProgram informationSystems =
                _educationService.AddEducationalProgram(tint, "Programming and Internet technologies");
            EducationalProgram cyberSecurityOfSystems =
                _educationService.AddEducationalProgram(ktu, "Cyber Security of Systems");

            Teacher alex = _educationService.AddTeacher("Alex Rubanov");

            ElectiveModule cyberSecurityBasics = _educationService.AddElectiveModule(ktu, "Cyber Security Basics");

            Lesson cyberSecurityBasicsLecture =
                _educationService.AddLesson("MOD", alex, "Kronva", new DateTime(2021, 10, 1, 10, 0, 0));

            Stream cyberSecurityBasicsStream =
                _educationService.AddStream(cyberSecurityBasics, cyberSecurityBasicsLecture);

            Group m3200 = _groupService.AddGroup("M3200");
            Student mishaKutuzov = _groupService.AddStudent(m3200, "Mikhail Kutuzov");

            Schedule m3200Schedule = _educationService.AddSchedule();

            _educationService.AddStudentToStream(m3200Schedule, cyberSecurityBasicsStream, mishaKutuzov);
            _groupService.RemoveStudent(cyberSecurityBasicsStream.Group, mishaKutuzov);

            Assert.AreEqual(0, cyberSecurityBasicsStream.Group.Students.Count);
        }

        [Test]
        public void AddStudentsToElectiveModules_GetStudentByCourseNumber()
        {
            MegaFaculty tint = _isuService.AddMegaFaculty("MEGAFACULTY OF TRANSLATIONAL INFORMATION TECHNOLOGIES");
            MegaFaculty ktu = _isuService.AddMegaFaculty("MEGAFACULTY OF COMPUTER TECHNOLOGY AND MANAGEMENT");

            EducationalProgram informationSystems =
                _educationService.AddEducationalProgram(tint, "Programming and Internet technologies");
            EducationalProgram cyberSecurityOfSystems =
                _educationService.AddEducationalProgram(ktu, "Cyber Security Of Systems");

            Teacher fredi = _educationService.AddTeacher("Fredi Kats");
            Teacher alex = _educationService.AddTeacher("Alex Rubanov");

            ElectiveModule functionalAnalysis = _educationService.AddElectiveModule(tint, "Functional Analysis");
            ElectiveModule cyberSecurityBasics = _educationService.AddElectiveModule(ktu, "Cyber Security Basics");

            Lesson oop = _educationService.AddLesson("OOP", fredi, "Kronva", new DateTime(2021, 9, 30, 10, 0, 0));
            Lesson cyberSecurityBasicsLecture =
                _educationService.AddLesson("MOD", alex, "Kronva", new DateTime(2021, 10, 1, 10, 0, 0));

            Stream cyberSecurityBasicsStream =
                _educationService.AddStream(cyberSecurityBasics, cyberSecurityBasicsLecture);
            Stream functionalAnalysisStream = _educationService.AddStream(functionalAnalysis, oop);

            Group m3200 = _groupService.AddGroup("M3200");
            Student mishaKutuzov = _groupService.AddStudent(m3200, "Mikhail Kutuzov");
            Student valeraShevchenko = _groupService.AddStudent(m3200, "Valera Shevchenko");
            Student Bibletoon = _groupService.AddStudent(m3200, "Bibletoon");

            Group m3410 = _groupService.AddGroup("M3410");
            Student ktuStudent1 = _groupService.AddStudent(m3410, "Loh 1");
            Student ktuStudent2 = _groupService.AddStudent(m3410, "Loh 2");
            Student ktuStudent3 = _groupService.AddStudent(m3410, "Loh 3");

            _groupService.AddStudent(cyberSecurityBasicsStream.Group, mishaKutuzov);
            _groupService.AddStudent(cyberSecurityBasicsStream.Group, valeraShevchenko);
            _groupService.AddStudent(cyberSecurityBasicsStream.Group, Bibletoon);

            _groupService.AddStudent(functionalAnalysisStream.Group, ktuStudent1);
            _groupService.AddStudent(functionalAnalysisStream.Group, ktuStudent2);
            _groupService.AddStudent(functionalAnalysisStream.Group, ktuStudent3);

            _isuService.AddGroupToEducationalProgram(informationSystems, m3200);
            _isuService.AddGroupToEducationalProgram(cyberSecurityOfSystems, m3410);

            List<Stream> streams2Course = _isuService.FindStreamsByCourseNumber(2);
            Student maybeMisha = null;
            foreach (Stream stream in streams2Course)
            {
                maybeMisha = stream.Group.FindStudent(mishaKutuzov.Id);
            }

            Assert.AreEqual(maybeMisha, mishaKutuzov);
        }

        [Test]
        public void AddStudentsToElectiveModules_GetStudentFromElectiveModule()
        {
            MegaFaculty tint = _isuService.AddMegaFaculty("MEGAFACULTY OF TRANSLATIONAL INFORMATION TECHNOLOGIES");
            MegaFaculty ktu = _isuService.AddMegaFaculty("MEGAFACULTY OF COMPUTER TECHNOLOGY AND MANAGEMENT");

            EducationalProgram informationSystems =
                _educationService.AddEducationalProgram(tint, "Programming and Internet technologies");

            Teacher alex = _educationService.AddTeacher("Alex Rubanov");

            ElectiveModule cyberSecurityBasics = _educationService.AddElectiveModule(ktu, "Cyber Security Basics");

            Lesson cyberSecurityBasicsLecture =
                _educationService.AddLesson("MOD", alex, "Kronva", new DateTime(2021, 10, 1, 10, 0, 0));

            Stream cyberSecurityBasicsStream =
                _educationService.AddStream(cyberSecurityBasics, cyberSecurityBasicsLecture);

            Group m3200 = _groupService.AddGroup("M3200");
            Student mishaKutuzov = _groupService.AddStudent(m3200, "Mikhail Kutuzov");
            Student valeraShevchenko = _groupService.AddStudent(m3200, "Valera Shevchenko");
            Student Bibletoon = _groupService.AddStudent(m3200, "Bibletoon");

            _groupService.AddStudent(cyberSecurityBasicsStream.Group, mishaKutuzov);
            _groupService.AddStudent(cyberSecurityBasicsStream.Group, valeraShevchenko);
            _groupService.AddStudent(cyberSecurityBasicsStream.Group, Bibletoon);

            List<Student> studentsFromCyberSecurity = _isuService.GetStudentsFromElectiveModule(cyberSecurityBasics);

            Assert.Contains(mishaKutuzov, studentsFromCyberSecurity);
        }

        [Test]
        public void AddStudentsToElectiveModule_FindFreeStudents()
        {
            MegaFaculty tint = _isuService.AddMegaFaculty("MEGAFACULTY OF TRANSLATIONAL INFORMATION TECHNOLOGIES");
            MegaFaculty ktu = _isuService.AddMegaFaculty("MEGAFACULTY OF COMPUTER TECHNOLOGY AND MANAGEMENT");

            EducationalProgram informationSystems =
                _educationService.AddEducationalProgram(tint, "Programming and Internet technologies");
            EducationalProgram cyberSecurityOfSystems =
                _educationService.AddEducationalProgram(ktu, "Cyber Security Of Systems");

            Teacher fredi = _educationService.AddTeacher("Fredi Kats");
            Teacher alex = _educationService.AddTeacher("Alex Rubanov");

            ElectiveModule functionalAnalysis = _educationService.AddElectiveModule(tint, "Functional Analysis");
            ElectiveModule cyberSecurityBasics = _educationService.AddElectiveModule(ktu, "Cyber Security Basics");

            Lesson oop = _educationService.AddLesson("OOP", fredi, "Kronva", new DateTime(2021, 9, 30, 10, 0, 0));
            Lesson cyberSecurityBasicsLecture =
                _educationService.AddLesson("MOD", alex, "Kronva", new DateTime(2021, 10, 1, 10, 0, 0));

            Stream cyberSecurityBasicsStream =
                _educationService.AddStream(cyberSecurityBasics, cyberSecurityBasicsLecture);
            Stream functionalAnalysisStream = _educationService.AddStream(functionalAnalysis, oop);

            Group m3200 = _groupService.AddGroup("M3200");
            Student mishaKutuzov = _groupService.AddStudent(m3200, "Mikhail Kutuzov");
            Student valeraShevchenko = _groupService.AddStudent(m3200, "Valera Shevchenko");
            Student Bibletoon = _groupService.AddStudent(m3200, "Bibletoon");

            _groupService.AddStudent(cyberSecurityBasicsStream.Group, mishaKutuzov);
            _groupService.AddStudent(cyberSecurityBasicsStream.Group, valeraShevchenko);

            List<Student> studentsFreeFromCyberSecurity = _isuService.GetStudentsFreeFromElectiveModules(m3200);

            Assert.Contains(Bibletoon, studentsFreeFromCyberSecurity);
        }
    }
}