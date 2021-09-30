using System.Collections.Generic;

namespace Isu.Entities
{
    public class Schedule
    {
        private readonly List<Lesson> _lessons;
        public Schedule()
        {
            _lessons = new List<Lesson>();
        }

        public IReadOnlyCollection<Lesson> Lessons => _lessons;

        public void AddLesson(Lesson lesson)
        {
            _lessons.Add(lesson);
        }
    }
}