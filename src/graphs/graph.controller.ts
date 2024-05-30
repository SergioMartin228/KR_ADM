import { Body, Controller, Delete, Get, Param, Post, Put } from "@nestjs/common";
import { GraphService } from "./graph.service";
import { Graph } from "./graph.entity";
import { CreateGraphDto } from "./dto/CreateGraph.dto";

@Controller('graph')
export class GraphController {
  constructor(private readonly graphService: GraphService) {}

  @Get()
  findAll() {
    return this.graphService.findAll();
  }

  @Get(':id')
  findOne(@Param('id') id: string) {
    const graph = this.graphService.findOne(+id);
    return graph;
  }

  @Put(':id')
  update(@Param('id') id: string, @Body() updatedGraph: Graph) {
    const result = this.graphService.update(+id, updatedGraph);
    return result;
  }

  @Post()
  create(@Body() newGraph: CreateGraphDto) {
    const result = this.graphService.create(newGraph);
    return result;
  }

  @Delete(':id')
  remove(@Param('id') id: string) {
    const result = this.graphService.remove(+id);
    return result;
  }
}