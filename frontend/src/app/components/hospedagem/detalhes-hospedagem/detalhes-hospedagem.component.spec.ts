import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhesHospedagemComponent } from './detalhes-hospedagem.component';

describe('DetalhesHospedagemComponent', () => {
  let component: DetalhesHospedagemComponent;
  let fixture: ComponentFixture<DetalhesHospedagemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetalhesHospedagemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetalhesHospedagemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
