using wedding_site.Domain;

namespace wedding_site.Data;

public static class RsvpModelExtensions
{
    public static RsvpModel ToDataModel(this Rsvp rsvp) =>
        new RsvpModel
        {
            Id = rsvp.Id,
            ConfirmationCode = rsvp.ConfirmationCode,
            Name = rsvp.Name,
            Attending = rsvp.Attending,
            FoodAlergies = rsvp.FoodAlergies
        };
    
    public static Rsvp ToDomainModel(this RsvpModel rsvpModel) =>
        new Rsvp(rsvpModel.Id, rsvpModel.ConfirmationCode)
        {
            Name = rsvpModel.Name,
            Attending = rsvpModel.Attending,
            FoodAlergies = rsvpModel.FoodAlergies
        };
}