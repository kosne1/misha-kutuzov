using System.Collections.Generic;
using Isu.Lessons;

namespace Isu.Entities
{
    public class Schedule
    {
        public Schedule()
        {
            Lessons = new List<Lesson>();
        }

        public List<Lesson> Lessons { get; }
    }
}