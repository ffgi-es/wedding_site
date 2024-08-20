using Microsoft.Azure.Cosmos;
using wedding_site.Domain;

namespace wedding_site.Data;

internal class RsvpRepo : IRsvpRepo
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
            var resp = await _containter.ReadItemAsync<Rsvp>(id, new PartitionKey(id));

            return resp.Resource;
        }
        catch(CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return default;
        }
    }

    public async Task SaveRsvp(Rsvp rsvp)
    {
        await _containter.UpsertItemAsync(rsvp.ToDataModel(), new PartitionKey(rsvp.Id));
    }
}