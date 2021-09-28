using System.Collections.Generic;

namespace Isu.Entities
{
    public class MegaFaculty
    {
        public MegaFaculty(string name)
        {
            MegaFaculties = new List<EducationalProgram>();
            Name = name;
        }

        public List<EducationalProgram> MegaFaculties { get; }
        public string Name { get; }
    }
}