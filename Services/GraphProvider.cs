using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using BlazorApp.Services.Karp;
using Newtonsoft.Json;
using BlazorApp.Data.Models;
using BlazorApp.Services;

namespace BlazorApp.Services;

public class GraphProvider : IGraphProvider
{
    private HttpClient _client;
    public GraphProvider(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<Graph>> GetAll()
    {
        return await _client.GetFromJsonAsync<List<Graph>>("http://localhost:3001/graph");
    }

    public async Task<Graph> GetOne(int id)
    {
        return await _client.GetFromJsonAsync<Graph>($"http://localhost:3001/graph/{id}");
    }

    public async Task<bool> Add(Graph item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var responce = await _client.PostAsync($"http://localhost:3001/graph", httpContent);
        return await Task.FromResult(responce.IsSuccessStatusCode);
    }

    public async Task<Graph> Edit(Graph item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var responce = await _client.PutAsync($"http://localhost:3001/graph", httpContent);
        Graph graphs = JsonConvert.DeserializeObject<Graph>(responce.Content.ReadAsStringAsync().Result);
        return await Task.FromResult(graphs);
    }

    public async Task<bool> Remove(int id)
    {
        var delete = await _client.DeleteAsync($"http://localhost:3001/graph/${id}");

        return await Task.FromResult(delete.IsSuccessStatusCode);
    }

}
