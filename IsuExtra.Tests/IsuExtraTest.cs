using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Entities;
using IsuExtra.Services;
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
        public void AddElectiveModule_ElectiveModuleIsInMegaFaculty()
        {
            MegaFaculty ktu = _isuService.AddMegaFaculty("MEGAFACULTY OF COMPUTER TECHNOLOGY AND MANAGEMENT");

            ElectiveModule cyberSecurityBasics = ktu.EducationService.AddElectiveModule("Cyber Security Basics");

            Assert.AreEqual(cyberSecurityBasics, ktu.EducationService.ElectiveModule);
        }

        [Test]
        public void AddElectiveModule_StudentCanEnrollElectiveModule()
        {
            MegaFaculty tint = _isuService.AddMegaFaculty("MEGAFACULTY OF TRANSLATIONAL INFORMATION TECHNOLOGIES");
            MegaFaculty ktu = _isuService.AddMegaFaculty("MEGAFACULTY OF COMPUTER TECHNOLOGY AND MANAGEMENT");

            EducationalProgram informationSystems =
                _isuService.AddEducationalProgram(tint, "Programming and Internet technologies");
            EducationalProgram cyberSecurityOfSystems =
                _isuService.AddEducationalProgram(ktu, "Cyber Security of Systems");

            Teacher alex = ktu.EducationService.AddTeacher("Alex Rubanov");

            ElectiveModule cyberSecurityBasics = ktu.EducationService.AddElectiveModule("Cyber Security Basics");
            
            Lesson cyberSecurityBasicsLecture =
                ktu.EducationService.AddLesson("MOD", alex, "Kronva", new DateTime(2021, 10, 1, 10, 0, 0));

            Stream cyberSecurityBasicsStream = cyberSecurityBasics.AddStream(cyberSecurityBasicsLecture);

            Group m3200 = informationSystems.GroupService.AddGroup("M3200");
            Student mishaKutuzov = informationSystems.GroupService.AddStudent(m3200, "Mikhail Kutuzov");

            cyberSecurityBasicsStream.Group.AddStudent(mishaKutuzov);

            Assert.Contains(mishaKutuzov, cyberSecurityBasicsStream.Group.Students.ToList());
        }

        [Test]
        public void AddStudentToElectiveModule_StudentRemovedFromElectiveModule()
        {
            MegaFaculty tint = _isuService.AddMegaFaculty("MEGAFACULTY OF TRANSLATIONAL INFORMATION TECHNOLOGIES");
            MegaFaculty ktu = _isuService.AddMegaFaculty("MEGAFACULTY OF COMPUTER TECHNOLOGY AND MANAGEMENT");

            EducationalProgram informationSystems =
                _isuService.AddEducationalProgram(tint, "Programming and Internet technologies");
            EducationalProgram cyberSecurityOfSystems =
                _isuService.AddEducationalProgram(ktu, "Cyber Security of Systems");

            Teacher alex = ktu.EducationService.AddTeacher("Alex Rubanov");

            ElectiveModule cyberSecurityBasics = ktu.EducationService.AddElectiveModule("Cyber Security Basics");

            Lesson cyberSecurityBasicsLecture =
                ktu.EducationService.AddLesson("MOD", alex, "Kronva", new DateTime(2021, 10, 1, 10, 0, 0));

            Stream cyberSecurityBasicsStream = cyberSecurityBasics.AddStream(cyberSecurityBasicsLecture);

            Group m3200 = informationSystems.GroupService.AddGroup("M3200");
            Student mishaKutuzov = informationSystems.GroupService.AddStudent(m3200, "Mikhail Kutuzov");

            cyberSecurityBasicsStream.Group.AddStudent(mishaKutuzov);
            cyberSecurityBasicsStream.Group.RemoveStudent(mishaKutuzov);

            Assert.AreEqual(0, cyberSecurityBasicsStream.Group.Students.Count);
        }

        [Test]
        public void AddStudentsToElectiveModules_GetStudentByCourseNumber()
        {
            MegaFaculty tint = _isuService.AddMegaFaculty("MEGAFACULTY OF TRANSLATIONAL INFORMATION TECHNOLOGIES");
            MegaFaculty ktu = _isuService.AddMegaFaculty("MEGAFACULTY OF COMPUTER TECHNOLOGY AND MANAGEMENT");

            EducationalProgram informationSystems =
                _isuService.AddEducationalProgram(tint, "Programming and Internet technologies");
            EducationalProgram cyberSecurityOfSystems =
                _isuService.AddEducationalProgram(ktu, "Cyber Security Of Systems");

            Teacher fredi = tint.EducationService.AddTeacher("Fredi Kats");
            Teacher alex = ktu.EducationService.AddTeacher("Alex Rubanov");

            ElectiveModule functionalAnalysis = tint.EducationService.AddElectiveModule("Functional Analysis");
            ElectiveModule cyberSecurityBasics = ktu.EducationService.AddElectiveModule("Cyber Security Basics");

            Lesson oop = tint.EducationService.AddLesson("OOP", fredi, "Kronva", new DateTime(2021, 9, 30, 10, 0, 0));
            Lesson cyberSecurityBasicsLecture =
                ktu.EducationService.AddLesson("MOD", alex, "Kronva", new DateTime(2021, 10, 1, 10, 0, 0));

            Stream cyberSecurityBasicsStream = cyberSecurityBasics.AddStream(cyberSecurityBasicsLecture);
            Stream functionalAnalysisStream = functionalAnalysis.AddStream(oop);

            Group m3200 = informationSystems.GroupService.AddGroup("M3200");
            Student mishaKutuzov = informationSystems.GroupService.AddStudent(m3200, "Mikhail Kutuzov");
            Student valeraShevchenko = informationSystems.GroupService.AddStudent(m3200, "Valera Shevchenko");
            Student Bibletoon = informationSystems.GroupService.AddStudent(m3200, "Bibletoon");

            Group m3410 = informationSystems.GroupService.AddGroup("M3410");
            Student ktuStudent1 = cyberSecurityOfSystems.GroupService.AddStudent(m3410, "Loh 1");
            Student ktuStudent2 = cyberSecurityOfSystems.GroupService.AddStudent(m3410, "Loh 2");
            Student ktuStudent3 = cyberSecurityOfSystems.GroupService.AddStudent(m3410, "Loh 3");

            cyberSecurityBasicsStream.Group.AddStudent(mishaKutuzov);
            cyberSecurityBasicsStream.Group.AddStudent(valeraShevchenko);
            cyberSecurityBasicsStream.Group.AddStudent(Bibletoon);

            functionalAnalysisStream.Group.AddStudent(ktuStudent1);
            functionalAnalysisStream.Group.AddStudent(ktuStudent2);
            functionalAnalysisStream.Group.AddStudent(ktuStudent3);

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
                _isuService.AddEducationalProgram(tint, "Programming and Internet technologies");

            Teacher alex = ktu.EducationService.AddTeacher("Alex Rubanov");

            ElectiveModule cyberSecurityBasics = ktu.EducationService.AddElectiveModule("Cyber Security Basics");

            Lesson cyberSecurityBasicsLecture =
                ktu.EducationService.AddLesson("MOD", alex, "Kronva", new DateTime(2021, 10, 1, 10, 0, 0));

            Stream cyberSecurityBasicsStream = cyberSecurityBasics.AddStream(cyberSecurityBasicsLecture);

            Group m3200 = informationSystems.GroupService.AddGroup("M3200");
            Student mishaKutuzov = informationSystems.GroupService.AddStudent(m3200, "Mikhail Kutuzov");
            Student valeraShevchenko = informationSystems.GroupService.AddStudent(m3200, "Valera Shevchenko");
            Student Bibletoon = informationSystems.GroupService.AddStudent(m3200, "Bibletoon");

            cyberSecurityBasicsStream.AddStudent(m3200, mishaKutuzov);
            cyberSecurityBasicsStream.AddStudent(m3200, valeraShevchenko);
            cyberSecurityBasicsStream.AddStudent(m3200, Bibletoon);

            List<Student> studentsFromCyberSecurity = _isuService.GetStudentsFromElectiveModule(cyberSecurityBasics);

            Assert.Contains(mishaKutuzov, studentsFromCyberSecurity);
        }

        [Test]
        public void AddStudentsToElectiveModule_FindFreeStudents()
        {
            MegaFaculty tint = _isuService.AddMegaFaculty("MEGAFACULTY OF TRANSLATIONAL INFORMATION TECHNOLOGIES");
            MegaFaculty ktu = _isuService.AddMegaFaculty("MEGAFACULTY OF COMPUTER TECHNOLOGY AND MANAGEMENT");

            EducationalProgram informationSystems =
                _isuService.AddEducationalProgram(tint, "Programming and Internet technologies");
            EducationalProgram cyberSecurityOfSystems =
                _isuService.AddEducationalProgram(ktu, "Cyber Security Of Systems");

            Teacher fredi = tint.EducationService.AddTeacher("Fredi Kats");
            Teacher alex = ktu.EducationService.AddTeacher("Alex Rubanov");

            ElectiveModule functionalAnalysis = tint.EducationService.AddElectiveModule("Functional Analysis");
            ElectiveModule cyberSecurityBasics = ktu.EducationService.AddElectiveModule("Cyber Security Basics");

            Lesson oop = tint.EducationService.AddLesson("OOP", fredi, "Kronva", new DateTime(2021, 9, 30, 10, 0, 0));
            Lesson cyberSecurityBasicsLecture =
                ktu.EducationService.AddLesson("MOD", alex, "Kronva", new DateTime(2021, 10, 1, 10, 0, 0));

            Stream cyberSecurityBasicsStream = cyberSecurityBasics.AddStream(cyberSecurityBasicsLecture);
            Stream functionalAnalysisStream = functionalAnalysis.AddStream(oop);

            Group m3200 = informationSystems.GroupService.AddGroup("M3200");
            Student mishaKutuzov = informationSystems.GroupService.AddStudent(m3200, "Mikhail Kutuzov");
            Student valeraShevchenko = informationSystems.GroupService.AddStudent(m3200, "Valera Shevchenko");
            Student Bibletoon = informationSystems.GroupService.AddStudent(m3200, "Bibletoon");

            cyberSecurityBasicsStream.Group.AddStudent(mishaKutuzov);
            cyberSecurityBasicsStream.Group.AddStudent(valeraShevchenko);

            List<Student> studentsFreeFromCyberSecurity = _isuService.GetStudentsFreeFromElectiveModules(m3200);

            Assert.Contains(Bibletoon, studentsFreeFromCyberSecurity);
        }
    }
}