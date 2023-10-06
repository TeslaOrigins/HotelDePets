import { Endereco } from "./Endereco";

export interface Tutor {
   id: number;
   nome : string;
   nome_normalizado: string
   cpf: string;
   telefone:string;
   email:string;
   enderecos : Endereco[];
}
