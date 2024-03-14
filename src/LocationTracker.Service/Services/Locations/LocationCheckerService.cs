using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocationTracker.Service.Interfaces.Locations;

namespace LocationTracker.Service.Services.Locations
{
    public class LocationCheckerService : ILocationCheckerService
    {
        public async Task<(bool inside, double distance)> DeterminePositionAsync(Tuple<double, double> wantedPersonLocation, List<Tuple<double, double>> borderPoints)
        {
            bool isInside = await RayCastingPointInPolygonAsync(wantedPersonLocation, borderPoints);

            if (isInside)
            {
                return (true, 0);
            }
            else
            {
                double distance = await CalculateMinimumDistanceToPolygonAsync(wantedPersonLocation, borderPoints);
                return (false, distance);
            }
        }

        public async Task<bool> RayCastingPointInPolygonAsync(Tuple<double, double> point, List<Tuple<double, double>> polygon)
        {
            double x = point.Item1;
            double y = point.Item2;
            bool inside = false;

            int n = polygon.Count;
            double p1x, p1y, p2x, p2y;
            for (int i = 0, j = n - 1; i < n; j = i++)
            {
                p1x = polygon[i].Item1;
                p1y = polygon[i].Item2;
                p2x = polygon[j].Item1;
                p2y = polygon[j].Item2;

                if (((p1y > y) != (p2y > y)) && (x < (p2x - p1x) * (y - p1y) / (p2y - p1y) + p1x))
                {
                    inside = !inside;
                }
            }
            return inside;
        }

        public async Task<double> CalculateMinimumDistanceToPolygonAsync(Tuple<double, double> point, List<Tuple<double, double>> polygon)
        {
            double minDistance = double.MaxValue;

            for (int i = 0; i < polygon.Count; i++)
            {
                var start = polygon[i];
                var end = polygon[(i + 1) % polygon.Count];

                double distance = PointToLineDistance(point, start, end);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }

            return minDistance;
        }

        private double PointToLineDistance(Tuple<double, double> pt, Tuple<double, double> lineStart, Tuple<double, double> lineEnd)
        {
            double distanceStart = HaversineDistance(pt, lineStart);
            double distanceEnd = HaversineDistance(pt, lineEnd);

            double lineLength = HaversineDistance(lineStart, lineEnd);
            if (lineLength == 0) return distanceStart;

            double t = ((pt.Item1 - lineStart.Item1) * (lineEnd.Item1 - lineStart.Item1) + (pt.Item2 - lineStart.Item2) * (lineEnd.Item2 - lineStart.Item2)) / (lineLength * lineLength);

            t = Math.Max(0, Math.Min(1, t));

            double lat = lineStart.Item1 + t * (lineEnd.Item1 - lineStart.Item1);
            double lng = lineStart.Item2 + t * (lineEnd.Item2 - lineStart.Item2);

            double distanceToLine = HaversineDistance(pt, Tuple.Create(lat, lng));

            return distanceToLine;
        }

        private double HaversineDistance(Tuple<double, double> point1, Tuple<double, double> point2)
        {
            double R = 6371;
            double lat1 = DegreesToRadians(point1.Item1);
            double lat2 = DegreesToRadians(point2.Item1);
            double deltaLat = DegreesToRadians(point2.Item1 - point1.Item1);
            double deltaLon = DegreesToRadians(point2.Item2 - point1.Item2);

            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}
