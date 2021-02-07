using System;

namespace WorkPlanning.Domain
{
    public class Shift
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public bool IsBefore(Shift shift)
        {
            return End < shift.Start;
        }
        
        public bool IsAfter(Shift shift)
        {
            return Start > shift.End;
        }

        public TimeSpan GetLength()
        {
            return End - Start;
        }
    }
}