/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AlimentoService } from './alimento.service';

describe('Service: Alimento', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AlimentoService]
    });
  });

  it('should ...', inject([AlimentoService], (service: AlimentoService) => {
    expect(service).toBeTruthy();
  }));
});
