/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AlterarPetComponent } from './alterar-pet.component';

describe('AlterarPetComponent', () => {
  let component: AlterarPetComponent;
  let fixture: ComponentFixture<AlterarPetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AlterarPetComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlterarPetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
