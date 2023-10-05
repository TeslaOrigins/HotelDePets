import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultarServicosComponent } from './consultar-servicos.component';

describe('ConsultarServicosComponent', () => {
  let component: ConsultarServicosComponent;
  let fixture: ComponentFixture<ConsultarServicosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConsultarServicosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConsultarServicosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
