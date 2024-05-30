import { CreateEdgeDto } from '../edges/CreateEdge.dto';
import { Edge } from '../edges/edges.entity';

export class CreateGraphDto {
  verticesCount: number;
  edges: CreateEdgeDto[];
}
