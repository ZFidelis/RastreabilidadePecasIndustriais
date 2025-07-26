import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroEstacao } from './cadastro-estacao';

describe('CadastroEstacao', () => {
  let component: CadastroEstacao;
  let fixture: ComponentFixture<CadastroEstacao>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CadastroEstacao]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CadastroEstacao);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
