using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public class IsuService
    {
        private readonly List<MegaFaculty> _megaFaculties;

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

        public List<Stream> FindStreamsByCourseNumber(int courseNumber)
        {
            var courseStudents = new List<Student>();
            foreach (MegaFaculty megaFaculty in MegaFaculties)
            {
                foreach (EducationalProgram educationalProgram in megaFaculty.EducationalPrograms)
                {
                    foreach (Group group in educationalProgram.Groups)
                    {
                        if (group.Course == courseNumber)
                            courseStudents.AddRange(group.Students);
                    }
                }
            }

            var foundStreams = new List<Stream>();
            foreach (MegaFaculty megaFaculty in MegaFaculties)
            {
                foreach (Stream stream in megaFaculty.ElectiveModule.Streams)
                {
                    foundStreams.AddRange(from student in stream.Group.Students
                        where courseStudents.Contains(student)
                        select stream);
                }
            }

            return foundStreams;
        }

        public List<Student> GetStudentsFromElectiveModule(ElectiveModule electiveModule)
        {
            return electiveModule.Streams.SelectMany(stream => stream.Group.Students).ToList();
        }

        public List<Student> GetStudentsFreeFromElectiveModules(Group group)
        {
            IEnumerable<Student> busyStudents = MegaFaculties.SelectMany(megaFaculty =>
                GetStudentsFromElectiveModule(megaFaculty.ElectiveModule));

            return group.Students.Where(student => !busyStudents.Contains(student)).ToList();
        }

        public void AddGroupToEducationalProgram(EducationalProgram educationalProgram, Group group)
        {
            educationalProgram.AddGroup(group);
        }
    }
}