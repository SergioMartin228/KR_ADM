using BlazorApp.Data.Models;

namespace BlazorApp.Data.Karp.Models;

public class KarpEdge
{
    public int To { get; private set; }// Вершина, куда ведёт ребро
    public int Capacity { get; private set; }// Пропускная способность ребра
    public int Flow { get; set; }// Текущий поток через ребро
    public KarpEdge Reverse { get; set; }// Обратное ребро в остаточной сети

    public KarpEdge(int to, int capacity)
    {
        To = to;
        Capacity = capacity;
        Flow = 0;
    }
}
