/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AlterarTutorComponent } from './alterar-tutor.component';

describe('AlterarTutorComponent', () => {
  let component: AlterarTutorComponent;
  let fixture: ComponentFixture<AlterarTutorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AlterarTutorComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlterarTutorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
