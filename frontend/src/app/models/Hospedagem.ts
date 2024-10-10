export interface Hospedagem {
    hospedagemId: number;
    dataEntrada: Date;
    dataSaida: Date;
    observacoes: string;
    checkIn: boolean;
    petId: number;
    precoHospedagem: number;
  }