import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastrarHospedagemComponent } from './cadastrar-hospedagem.component';

describe('CadastrarHospedagemComponent', () => {
  let component: CadastrarHospedagemComponent;
  let fixture: ComponentFixture<CadastrarHospedagemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CadastrarHospedagemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CadastrarHospedagemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
