using BlazorApp.Data.Models;

namespace BlazorApp.Data.CreateDto;

public class CreateGraphDto
{
    public int verticesCount { get; set; }
    public IEnumerable<CreateEdgeDto> edges { get; set; }
    public static bool IsValid(CreateGraphDto graph)
    {
        // Проверяем, что количество вершин больше 0
        if (graph.verticesCount <= 0) return false;

        // Проверяем, что все вершины from и to лежат в пределах допустимых значений и что пропускная способность положительна
        foreach (var edge in graph.edges)
        {
            if (edge.from < 0 || edge.to < 0 || edge.from >= graph.verticesCount || edge.to >= graph.verticesCount || edge.capacity < 0)
            {
                return false;
            }
        }

        // Все проверки пройдены
        return true;
    }
}
