using System.Collections.Generic;
using IsuExtra.Services;

namespace IsuExtra.Entities
{
    public class MegaFaculty
    {
        public MegaFaculty(string name)
        {
            EducationalPrograms = new List<EducationalProgram>();
            EducationService = new EducationService();
            Name = name;
        }

        public List<EducationalProgram> EducationalPrograms { get; }
        public EducationService EducationService { get; }
        public string Name { get; }

        public EducationalProgram AddEducationalProgram(EducationalProgram educationalProgram)
        {
            EducationalPrograms.Add(educationalProgram);
            return educationalProgram;
        }
    }
}