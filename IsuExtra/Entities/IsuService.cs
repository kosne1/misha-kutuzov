using System.Collections.Generic;

namespace IsuExtra.Entities
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
    }
}