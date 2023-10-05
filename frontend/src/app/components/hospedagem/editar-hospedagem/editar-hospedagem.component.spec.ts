import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarHospedagemComponent } from './editar-hospedagem.component';

describe('EditarHospedagemComponent', () => {
  let component: EditarHospedagemComponent;
  let fixture: ComponentFixture<EditarHospedagemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditarHospedagemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditarHospedagemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
