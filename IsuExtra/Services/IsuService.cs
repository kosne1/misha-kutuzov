using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public class IsuService
    {
        public IsuService()
        {
            MegaFaculties = new List<MegaFaculty>();
        }

        public List<MegaFaculty> MegaFaculties { get; }

        public MegaFaculty AddMegaFaculty(string name)
        {
            var megaFaculty = new MegaFaculty(name);
            MegaFaculties.Add(megaFaculty);
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
    }
}