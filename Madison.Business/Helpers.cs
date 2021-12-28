namespace Madison.Business;

public static class Helpers
{
    public static int DifferenceInYearsBetweenTwoDates(DateTime dateTime1, DateTime dateTime2)
    {
        if (dateTime1 < dateTime2)
        {
            throw new ArgumentException("The first parameter needs to be greater than the second");
        }
        
        return (int)((dateTime1 - dateTime2).TotalDays / 365);
    }
}
