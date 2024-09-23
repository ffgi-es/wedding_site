namespace wedding_site.Domain;

interface IRsvpRepo
{
    Task<Rsvp?> GetRsvp(string id);

    Task SaveRsvp(Rsvp rsvp);
}