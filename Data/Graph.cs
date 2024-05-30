namespace BlazorApp.Data.Models;

public class Graph
{
    public int Id { get; set; }
    public int VerticesCount { get; set; }
    public IEnumerable<Edge> Edges { get; set; }
}
