namespace wedding_site.Domain;

public record Rsvp(string Id, string ConfirmationCode)
{
    public bool Replied { get; set; }
    public bool Attending { get; set; }
    public List<Attendee> Attendees { get; set; } = [];
}

public class Attendee
{
    public string Name { get; set; } = string.Empty;
    public FoodRequirements FoodRequirements { get; set; } = new();
}

public class FoodRequirements
{
    public bool Vegetarian { get; set; }
    public bool Vegan { get; set; }
    public bool Coeliac { get; set; }
    public bool Other { get; set; }
    public string OtherDescription { get; set; } = string.Empty;
}
