using BlazorApp.Data.CreateDto;
using BlazorApp.Data.Models;
namespace BlazorApp.Services;

public interface IGraphProvider
{
    Task<List<Graph>> GetAll();
    Task<Graph> GetOne(int id);
    Task<Graph> Add(CreateGraphDto item);
    Task<Graph> Edit(Graph item);
    Task<bool> Remove(int id);
}
