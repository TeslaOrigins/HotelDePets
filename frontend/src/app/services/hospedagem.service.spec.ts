import { TestBed, inject } from '@angular/core/testing';
import { HospedagemService } from './hospedagem.service'; // Certifique-se de importar o serviço correto

describe('Service: Hospedagem', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HospedagemService] // Registre o serviço de hospedagem aqui
    });
  });

  it('should be created', inject([HospedagemService], (service: HospedagemService) => {
    expect(service).toBeTruthy();
  }));

  // Adicione mais testes específicos para o serviço de hospedagem aqui, conforme necessário
});