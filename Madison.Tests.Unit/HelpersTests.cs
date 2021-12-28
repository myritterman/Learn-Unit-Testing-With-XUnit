using System;
using FluentAssertions;
using Madison.Business;
using Xunit;

namespace Employees.Tests.Unit;

public class HelpersTests
{
    [Fact]
    public void DifferenceInYearsBetweenTwoDates_ShouldReturnCorrectAmountYears_WhenFirstIsGreaterThanSecond()
    {
        // arrange
        var startDate = DateTime.Now.AddMonths(-37);
        var expectedResult = 3;
        
        // act
        var result = Helpers.DifferenceInYearsBetweenTwoDates(DateTime.Today, startDate);

        //assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void DifferenceInYearsBetweenTwoDates_ShouldThrowArgumentException_WhenFirstIsLessThanSecond()
    {
        // arrange
        var startDate = DateTime.Now.AddMonths(-37);
        var expectedResult = 3;
        
        // act
        var result = () => Helpers.DifferenceInYearsBetweenTwoDates(DateTime.Today.AddYears(-5), startDate);

        //assert
        result.Should().Throw<ArgumentException>().WithMessage("The first parameter needs to be greater than the second");
    }
}
