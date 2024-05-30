using BlazorApp.Data.Karp.Models;
using BlazorApp.Data.Models;
using BlazorApp.Pages;

namespace BlazorApp.Services.Karp;

public class KarpProvider : IKarpProvider
{
    public KarpGraph graph { get; set; }
    private int[] parent;

    public void TransformGraph(Graph serverGraph)
    {
        graph = new KarpGraph(serverGraph.VerticesCount);
        foreach (var edgeFromServer in serverGraph.Edges)
        {
            graph.AddEdge(edgeFromServer.From, edgeFromServer.To, edgeFromServer.Capacity);
        }
        parent = new int[graph.VerticesCount];
    }

    private int BFS(int s, int t)
    {
        for (int i = 0; i < parent.Length; i++)
        {
            parent[i] = -1;
        }
        parent[s] = -2;
        Queue<(int, int)> queue = new Queue<(int, int)>();
        queue.Enqueue((s, int.MaxValue));

        while (queue.Count > 0)
        {
            (int curr, int flow) = queue.Dequeue();

            foreach (KarpEdge edge in graph.GetEdges(curr))
            {
                int ostCapacity = edge.Capacity - edge.Flow;
                if (ostCapacity > 0 && parent[edge.To] == -1)
                {
                    parent[edge.To] = curr;
                    int new_flow = Math.Min(flow, ostCapacity);
                    if (edge.To == t)
                    {
                        return new_flow;
                    }
                    queue.Enqueue((edge.To, new_flow));
                }
            }
        }

        return 0;
    }
    private void ResetFlows()
    {
        foreach (var edge in graph.Edges)
        {
            edge.Flow = 0;
            if (edge.Reverse != null)
            {
                edge.Reverse.Flow = 0;
            }
        }
    }
    public int Execute(int s, int t)
    {
        ResetFlows();
        int flow = 0;
        int new_flow;

        while ((new_flow = BFS(s, t)) > 0)
        {
            flow += new_flow;
            int current = t;
            while (current != s)
            {
                int prev = parent[current];
                KarpEdge edge = graph.GetEdges(prev).First(e => e.To == current);
                edge.Flow += new_flow;
                edge.Reverse.Flow -= new_flow;
                current = prev;
            }
        }
        return flow;
    }

}
