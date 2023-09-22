import { Endereco } from "./Endereco";

export interface Tutor {
   tutorId: number;
   nome : string;
   nomeNormalizado: string
   cpf: string;
   telefone:string;
   email:string;
   enderecos : Endereco[];
}
