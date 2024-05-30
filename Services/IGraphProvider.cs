using BlazorApp.Data.Models;
namespace BlazorApp.Services;

public interface IGraphProvider
{
    Task<List<Graph>> GetAll();
    Task<Graph> GetOne(int id);
    Task<bool> Add(Graph item);
    Task<Graph> Edit(Graph item);
    Task<bool> Remove(int id);
}
