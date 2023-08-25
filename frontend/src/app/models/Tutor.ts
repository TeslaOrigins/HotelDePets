import { Endereco } from "./Endereco";

export interface Tutor {
   id: number;
   name : string;
   nomeNormalizado: string
   cpf: string;
   endereco : Endereco[];
}
