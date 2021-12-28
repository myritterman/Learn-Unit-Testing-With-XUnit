using System;
using FluentAssertions;
using Xunit;
using static Madison.Business.Helpers;

namespace Employees.Tests.Unit.Prep;

public class HelpersTests
{
    [Fact]
    public void DifferenceInYearsBetweenTwoDates_ShouldThrowAnException_WhenFirstDateIsLessThanSecond()
    {
        // arrange
        var dateTime1 = new DateTime(2013, 06, 18);
        var dateTime2 = new DateTime(2017, 08, 03);

        // act
        var result = () => DifferenceInYearsBetweenTwoDates(dateTime1, dateTime2);

        //assert
        result.Should().Throw<ArgumentException>().WithMessage("The first parameter needs to be greater than the second");
    }

    [Theory, MemberData(nameof(YearsBetweenDatesData))]
    public void DifferenceInYearsBetweenTwoDates_ShouldReturnCorrectAmountOfYears_WhenFirstDateIsGreaterThanSecond(DateTime dateTime1, DateTime dateTime2, int expectedResult)
    {
        // arrange

        // act
        var result = DifferenceInYearsBetweenTwoDates(dateTime1, dateTime2);

        //assert
        result.Should().Be(expectedResult);
    }

    private static object[][] YearsBetweenDatesData()
    {
        return new[]
        {
            new object[] { new DateTime(2021, 09, 01), new DateTime(2015, 11, 22), 5 },
            new object[] { new DateTime(2018, 06, 18), new DateTime(2017, 08, 03), 0 },
        };
    }
}
