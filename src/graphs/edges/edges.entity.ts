import { Column, Entity, ManyToOne, PrimaryGeneratedColumn } from 'typeorm';
import { Graph } from '../graph.entity';

@Entity('edges')
export class Edge {
  @PrimaryGeneratedColumn()
  id: number;
  @Column()
  from: number;
  @Column()
  to: number;
  @Column()
  capacity: number;
  @Column({ default: 0 })
  flow: number;
  //   @Column()
  //   reverse: Edge;
  @ManyToOne(() => Graph, (graph) => graph.edges, { onDelete: 'CASCADE' })
  graph: Graph;
}
