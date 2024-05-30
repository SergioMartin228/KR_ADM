using BlazorApp.Data.Models;

namespace BlazorApp.Data.Karp.Models;

public class KarpGraph
{
    public List<KarpEdge>[] adjList; // список смежности
    public int VerticesCount { get; set; }

    public KarpGraph(int verticesCount)
    {
        VerticesCount = verticesCount;
        adjList = new List<KarpEdge>[verticesCount];
        for (int i = 0; i < verticesCount; i++)
            adjList[i] = new List<KarpEdge>();
    }
    //public KarpGraph(KarpGraph karp)
    //{
    //    VerticesCount = karp.VerticesCount;
    //    adjList = new List<KarpEdge>[VerticesCount];
    //    for (int i = 0; i < VerticesCount; i++)
    //    {
    //        adjList[i] = new List<KarpEdge>();
    //        foreach(var edge in karp.adjList[i])
    //            adjList[i].Add(edge);
    //    }
    //}

    public void AddEdge(int from, int to, int capacity)
    {
        KarpEdge e1 = new KarpEdge(to, capacity);
        KarpEdge e2 = new KarpEdge(from, 0); // обратное ребро с нулевой пропускной способностью
        e1.Reverse = e2;
        e2.Reverse = e1;
        adjList[from].Add(e1);
        adjList[to].Add(e2); // обратное ребро для увеличения потока при необходимости
    }

    public IEnumerable<KarpEdge> Edges => adjList.SelectMany(e => e);
    public List<KarpEdge> GetEdges(int vertex) => adjList[vertex];
}
