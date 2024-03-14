namespace LocationTracker.Service.Exceptions;

public class LocationTrackerException : Exception
{
    public int StatusCode { get; set; }
    public LocationTrackerException(int code, string message) :
        base(message)
    {
        StatusCode = code;
    }
}
