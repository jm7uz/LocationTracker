using LocationTracker.Domain.Commons;

namespace LocationTracker.Domain.Entities.Districts;

public class District : Auditable<int>
{
    public string Name { get; set; }
}
