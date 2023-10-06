import { Servico } from './Servico';

export interface Reserva {
  reservaId: number;
  dataCheckin: Date;
  dataCheckout: Date;
  servicos: Servico[];
}
