using System.Collections.Generic;
using IsuExtra.Services;

namespace IsuExtra.Entities
{
    public class MegaFaculty
    {
        private readonly List<EducationalProgram> _educationalPrograms;
        public MegaFaculty(string name)
        {
            _educationalPrograms = new List<EducationalProgram>();
            EducationService = new EducationService();
            Name = name;
        }

        public IReadOnlyCollection<EducationalProgram> EducationalPrograms => _educationalPrograms;
        public EducationService EducationService { get; }
        public string Name { get; }

        public EducationalProgram AddEducationalProgram(EducationalProgram educationalProgram)
        {
            _educationalPrograms.Add(educationalProgram);
            return educationalProgram;
        }
    }
}