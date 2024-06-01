using BlazorApp.Data.Karp.Models;
using Microsoft.JSInterop;

namespace BlazorApp.Visualization;

public class CytoInterop
{
    public static (List<object> nodes, List<object> edges) TransformGraphToCytoscape(KarpGraph graph)
    {
        List<object> nodes = new List<object>();
        List<object> edges = new List<object>();

        // Создаем "nodes" для Cytoscape, предполагая, что вершины нумеруются от 0 до VerticesCount-1
        for (int i = 0; i < graph.VerticesCount; i++)
        {
            nodes.Add(new { data = new { id = i.ToString() } });
        }

        // Создаем "edges" для Cytoscape, исключая обратные ребра
        for (int i = 0; i < graph.VerticesCount; i++)
        {
            foreach (var edge in graph.adjList[i])
            {
                // Убеждаемся, что мы не добавляем обратное ребро
                if (edge.Capacity > 0) // Вы можете использовать другие способы определения обратных рёбер
                {
                    edges.Add(new
                    {
                        data = new
                        {
                            source = i.ToString(),
                            target = edge.To.ToString(),
                            capacity = edge.Capacity,
                            flow = edge.Flow
                        }
                    });
                }
            }
        }

        return (nodes, edges);
    }

    public static async Task RenderGraphAsync(IJSRuntime jsRuntime, List<object> nodes, List<object> edges)
    {
        await jsRuntime.InvokeVoidAsync("renderCytoscapeGraph", nodes, edges);
    }
}
