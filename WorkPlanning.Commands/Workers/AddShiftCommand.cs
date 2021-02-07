using System;

namespace WorkPlanning.Commands.Workers
{
    public class AddShiftCommand
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int WorkerId { get; set; }
    }
}