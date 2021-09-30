using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public class IsuService
    {
        private List<MegaFaculty> _megaFaculties;

        public IsuService()
        {
            _megaFaculties = new List<MegaFaculty>();
        }

        public IReadOnlyCollection<MegaFaculty> MegaFaculties => _megaFaculties;

        public MegaFaculty AddMegaFaculty(string name)
        {
            var megaFaculty = new MegaFaculty(name);
            _megaFaculties.Add(megaFaculty);
            return megaFaculty;
        }

        public EducationalProgram AddEducationalProgram(MegaFaculty megaFaculty, string name)
        {
            var educationalProgram = new EducationalProgram(name);
            megaFaculty.AddEducationalProgram(educationalProgram);
            return educationalProgram;
        }

        public List<Stream> FindStreamsByCourseNumber(int courseNumber)
        {
            var courseStudents = new List<Student>();
            foreach (List<Student> found in MegaFaculties.SelectMany(megaFaculty =>
                megaFaculty.EducationalPrograms.Select(educationalProgram =>
                    educationalProgram.GroupService.FindStudents(courseNumber))))
            {
                courseStudents.AddRange(found);
            }

            return (from megaFaculty in MegaFaculties
                from stream in megaFaculty.EducationService.ElectiveModule.Streams
                from student in stream.Group.Students
                where courseStudents.Contains(student)
                select stream).ToList();
        }

        public List<Student> GetStudentsFromElectiveModule(ElectiveModule electiveModule)
        {
            return electiveModule.Streams.SelectMany(stream => stream.Group.Students).ToList();
        }

        public List<Student> GetStudentsFreeFromElectiveModules(Group group)
        {
            var busyStudents = new List<Student>();
            foreach (List<Student> found in MegaFaculties.Select(megaFaculty =>
                GetStudentsFromElectiveModule(megaFaculty.EducationService.ElectiveModule)))
            {
                busyStudents.AddRange(found);
            }

            return group.Students.Where(student => !busyStudents.Contains(student)).ToList();
        }
    }
}