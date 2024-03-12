using LocationTracker.Domain.Commons;

namespace LocationTracker.Domain.Entities.Regions;

public class Region : Auditable<int>
{
    public string Name { get; set; }
}
