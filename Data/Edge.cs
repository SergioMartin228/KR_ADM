namespace BlazorApp.Data.Models;

public class Edge
{
    public int Id { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public int Capacity { get; set; }
    public int Flow { get; set; }
    public Graph Graph { get; set; }
}
