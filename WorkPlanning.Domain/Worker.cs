using System;
using System.Collections.Generic;
using WorkPlanning.Infrastructure;

namespace WorkPlanning.Domain
{
    public class Worker
    {
        public Worker()
        {
            Shifts = new List<Shift>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Shift> Shifts { get; set; }

        //returning a cmd result here is trading off some domain purity but it is either this or throwing business exceptions
        public CommandResult<Shift> AddShift(DateTime startDate, DateTime endDate)
        {
            var newShift = new Shift
            {
                Start = startDate,
                End = endDate,
                WorkerId = Id
            };
            var shiftLength = newShift.GetLength();
            
            foreach (var shift in Shifts)
            {
                if (ShiftsOverlap(shift, newShift) )
                {
                    return CommandResult<Shift>.Failure("Shifts overlap");
                }
                
                //at this point we know they do not overlap

                if (newShift.IsBefore(shift)) 
                {
                    if (shift.Start - newShift.End < shiftLength)
                    {
                        return CommandResult<Shift>.Failure("Shifts are too close to each other");
                    }
                }
                
                if (newShift.IsAfter(shift))
                {
                    if (newShift.Start - shift.End < shiftLength)
                    {
                        return CommandResult<Shift>.Failure("Shifts are too close to each other");
                    }
                }
            }
            
            Shifts.Add(newShift);
            return CommandResult<Shift>.Success(newShift);
        }

        private bool ShiftsOverlap(Shift shift, Shift newShift)
        {
            return shift.Start <= newShift.Start && newShift.Start <= shift.End ||
                   shift.Start <= newShift.End && newShift.End <= shift.End;
        }
    }
}