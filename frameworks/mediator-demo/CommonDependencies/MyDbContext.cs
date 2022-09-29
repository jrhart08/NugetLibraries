#pragma warning disable CS8618

namespace CommonDependencies;

public class Subscriber
{
    public Guid Id { get; set; }
    public Guid AreaId { get; set; }
    public string PhoneNumber { get; set; }

    public virtual SupportedArea Area { get; set; }
}

public class SupportedArea
{
    public Guid Id { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    
    public virtual ICollection<Subscriber> Subscribers { get; set; }
}

public class MyDbContext
{
    public IQueryable<Subscriber> Subscribers { get; set; } = Array.Empty<Subscriber>().AsQueryable();
    public IQueryable<SupportedArea> SupportedAreas { get; set; } = Array.Empty<SupportedArea>().AsQueryable();
}
