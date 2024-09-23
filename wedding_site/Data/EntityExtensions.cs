using wedding_site.Domain;

namespace wedding_site.Data;

public static class EntityExtensions
{
    public static Entity<Rsvp> ToEntity(this Rsvp model) =>
        new Entity<Rsvp>
        {
            Id = model.Id,
            Payload = model
        };
}