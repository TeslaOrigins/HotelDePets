import { Endereco } from "./Endereco";

export interface Tutor {
   id: number;
   nome : string;
   nomeNormalizado: string
   cpf: string;
   endereco : Endereco[];
}
