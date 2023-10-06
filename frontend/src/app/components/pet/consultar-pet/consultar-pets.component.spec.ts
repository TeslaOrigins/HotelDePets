import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultarPetsComponent } from './consultar-pets.component';

describe('ConsultarPetsComponent', () => {
  let component: ConsultarPetsComponent;
  let fixture: ComponentFixture<ConsultarPetsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConsultarPetsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConsultarPetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
