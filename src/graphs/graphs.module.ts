import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { GraphController } from './graph.controller';
import { GraphService } from './graph.service';
import { Graph } from './graph.entity';
import { Edge } from './edges/edges.entity';

@Module({
  controllers: [GraphController],
  providers: [GraphService],
  imports: [TypeOrmModule.forFeature([Graph, Edge])],
})
export class GraphsModule {}
