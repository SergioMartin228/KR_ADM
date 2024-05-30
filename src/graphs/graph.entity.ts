import { Column, Entity, OneToMany, PrimaryGeneratedColumn } from 'typeorm';
import { Edge } from './edges/edges.entity';

@Entity('graph')
export class Graph {
  @PrimaryGeneratedColumn()
  id: number;
  @Column()
  verticesCount: number;
  @OneToMany(() => Edge, (edge) => edge.graph)
  edges: Edge[];
}
