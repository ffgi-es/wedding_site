using Microsoft.Azure.Cosmos;
using wedding_site.Domain;

namespace wedding_site.Data;

public class RsvpRepo : IRsvpRepo
{
    private Container _containter;

    public RsvpRepo(CosmosClient cosmosClient)
    {
        _containter = cosmosClient.GetContainer("wedding", "rsvp");
    }

    public async Task<Rsvp?> GetRsvp(string id)
    {
        try
        {
            var resp = await _containter.ReadItemAsync<Entity<Rsvp>>(id, new PartitionKey(id));

            return resp.Resource.Payload;
        }
        catch(CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return default;
        }
    }

    public async Task SaveRsvp(Rsvp rsvp)
    {
        await _containter.UpsertItemAsync(rsvp.ToEntity(), new PartitionKey(rsvp.Id));
    }

    public async Task CreateRsvp(Rsvp rsvp)
    {
        await _containter.CreateItemAsync(rsvp.ToEntity(), new PartitionKey(rsvp.Id));
    }
}