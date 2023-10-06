import { Tutor } from "./Tutor";

export interface Pet {
  id: number;
  nome: string;
  idade_mes: number;
  raca: string;
  sexo: string;
  peso: number;
  especie: string;
  tutorId: Tutor;
}
