using System;
using FluentAssertions;
using WorkPlanning.Domain;
using Xunit;

namespace WorkPlanning.Tests
{
    public class WorkerTests
    {
        [Fact]
        public void When_OverlapsWithAnotherShift_Then_AnErrorIsReturned()
        {
            // arrange
            var sut = new Worker();
            sut.AddShift(GetDate(0, 0, 0), GetDate(8, 0, 0));


            // act
            var result = sut.AddShift(GetDate(7, 0, 0), GetDate(15, 0, 0));

            // assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNullOrEmpty();
            sut.Shifts.Count.Should().Be(1);
        }

        [Fact]
        public void When_NoOverlap_Then_TheShiftIsAssignedToWorker()
        {
            // arrange
            var sut = new Worker();
            sut.AddShift(GetDate(0, 0, 0), GetDate(8, 0, 0));


            // act
            var result = sut.AddShift(GetDate(16, 0, 0), GetDate(20, 0, 0));

            // assert
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNullOrEmpty();
            sut.Shifts.Count.Should().Be(2);
        }

        [Fact]
        public void When_DistanceBetweenShiftsIsLessThanTheSizeOfTheNewShift_Then_ReturnError()
        {
            // arrange
            var sut = new Worker();
            sut.AddShift(GetDate(0, 0, 0), GetDate(8, 0, 0));


            // act
            var result = sut.AddShift(GetDate(12, 0, 0), GetDate(20, 0, 0));

            // assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNullOrEmpty();
            sut.Shifts.Count.Should().Be(1);
        }

        private DateTime GetDate(int h, int m, int s)
        {
            return new DateTime(2010, 1, 1, h, m, s);
        }
    }
}