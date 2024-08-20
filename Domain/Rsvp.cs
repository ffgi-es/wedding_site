namespace wedding_site.Domain;

public record Rsvp(string Id, string ConfirmationCode)
{
    public string Name { get; set;} = string.Empty;
    public bool Attending { get; set; }
    public string FoodAlergies { get; set; } = string.Empty;
}
