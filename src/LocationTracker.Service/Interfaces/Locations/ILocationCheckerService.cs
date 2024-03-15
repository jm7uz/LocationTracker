namespace LocationTracker.Service.Interfaces.Locations;

public interface ILocationCheckerService
{
    Task<(bool inside, double distance)> DeterminePositionAsync(Tuple<double, double> wantedPersonLocation, List<Tuple<double, double>> borderPoints);

}
