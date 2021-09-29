using System.Collections.Generic;

namespace IsuExtra.Entities
{
    public class MegaFaculty
    {
        public MegaFaculty(string name)
        {
            EducationalPrograms = new List<EducationalProgram>();
            ElectiveModules = new List<ElectiveModule>();
            EducationService = new EducationService();
            Name = name;
        }

        public List<EducationalProgram> EducationalPrograms { get; }
        public List<ElectiveModule> ElectiveModules { get; }
        public EducationService EducationService { get; }
        public string Name { get; }

        public EducationalProgram AddEducationalProgram(EducationalProgram educationalProgram)
        {
            EducationalPrograms.Add(educationalProgram);
            return educationalProgram;
        }

        public ElectiveModule AddElectiveModule(string name)
        {
            var electiveModule = new ElectiveModule(name);
            ElectiveModules.Add(electiveModule);
            return electiveModule;
        }
    }
}