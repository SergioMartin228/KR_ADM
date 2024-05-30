import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Graph } from './graph.entity';
import { Repository } from 'typeorm';
import { Edge } from './edges/edges.entity';
import { CreateGraphDto } from './dto/CreateGraph.dto';
import { CreateEdgeDto } from './edges/CreateEdge.dto';

@Injectable()
export class GraphService {
  constructor(
    @InjectRepository(Graph)
    private readonly graphRepository: Repository<Graph>,
    @InjectRepository(Edge)
    private readonly edgeRepository: Repository<Edge>,
  ) {}

  async create(createGraphDto: CreateGraphDto): Promise<Graph> {
    const graph = await this.graphRepository.create();
    graph.verticesCount = createGraphDto.verticesCount;
    graph.edges = [];
    await this.graphRepository.save(graph);
    for (const edgeDto of createGraphDto.edges) {
      const edge = await this.addEdge(graph, edgeDto);
      console.log(edge);
      graph.edges.push(edge);
    }

    await this.graphRepository.save(graph);
    return graph;
  }

  async findOne(id: number): Promise<Graph> {
    return await this.graphRepository.findOne({
      where: { id },
      relations: { edges: true },
    });
  }

  async findAll(): Promise<Graph[]> {
    const graphs = await this.graphRepository.find({
      relations: { edges: true },
    });
    return graphs;
  }

  async update(id: number, updatedGraph: Graph) {
    const graph = await this.graphRepository.findOne({ where: { id } });
    graph.edges = updatedGraph.edges;
    graph.verticesCount = updatedGraph.verticesCount;
    await this.graphRepository.save(graph);
    return graph;
  }

  async remove(id: number) {
    await this.graphRepository.delete({ id });
  }

  async addEdge(graph: Graph, createEdgeDto: CreateEdgeDto): Promise<Edge> {
    const edge = await this.edgeRepository.create({
      from: createEdgeDto.from,
      to: createEdgeDto.to,
      capacity: createEdgeDto.capacity,
      graph: graph,
    });
    await this.edgeRepository.save(edge);
    return edge;
  }
}
