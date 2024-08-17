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
        var resp = await _containter.ReadItemAsync<Rsvp>(id, new PartitionKey(id));

        return resp.StatusCode == System.Net.HttpStatusCode.OK
            ? resp.Resource
            : default;
    }
}