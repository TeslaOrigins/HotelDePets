import { Alimento } from "./Alimento";

export interface Dieta {
  quantidade: number;
  observacoes: string;
  horarioAlimentacao: Date;
  alimentos: Alimento[];
}
