using BlazorApp.Data.Karp.Models;
using Microsoft.JSInterop;

namespace BlazorApp.Visualization;

public class D3Interop
{
    public static (List<object> nodes, List<object> links) TransformGraphToD3(KarpGraph graph)
    {
        List<object> nodes = new List<object>();
        List<object> links = new List<object>();

        // Создаем "nodes" для D3, предполагая, что вершины нумеруются от 0 до VerticesCount-1
        for (int i = 0; i < graph.VerticesCount; i++)
        {
            nodes.Add(new { id = i });
        }

        // Создаем "links" для D3, исключая обратные ребра
        for (int i = 0; i < graph.VerticesCount; i++)
        {
            foreach (var edge in graph.adjList[i])
            {
                // Убеждаемся, что мы не добавляем обратное ребро
                if (edge.Capacity > 0) // Вы можете использовать другие способы определения обратных рёбер
                {
                    links.Add(new { source = i, target = edge.To,
                        capacity = edge.Capacity, flow = edge.Flow});
                }
            }
        }

        return (nodes, links);
    }
    public static async Task RenderGraphAsync(IJSRuntime jsRuntime, List<object> nodes, List<object> links)
    {
        await jsRuntime.InvokeVoidAsync("renderGraph", nodes, links);
    }

}
