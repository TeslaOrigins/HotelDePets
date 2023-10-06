import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListarMedicamentosComponent } from './listar-medicamentos.component';

describe('ListarMedicamentosComponent', () => {
  let component: ListarMedicamentosComponent;
  let fixture: ComponentFixture<ListarMedicamentosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListarMedicamentosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListarMedicamentosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
