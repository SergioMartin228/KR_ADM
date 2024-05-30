using BlazorApp.Data.Karp.Models;
using BlazorApp.Data.Models;

namespace BlazorApp.Services.Karp;

public interface IKarpProvider
{
    KarpGraph graph { get; set; }
    void TransformGraph(Graph serverGraph);
    int Execute(int s, int t);
}
