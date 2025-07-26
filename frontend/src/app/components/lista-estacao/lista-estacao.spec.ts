import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaEstacao } from './lista-estacao';

describe('ListaEstacao', () => {
  let component: ListaEstacao;
  let fixture: ComponentFixture<ListaEstacao>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListaEstacao]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListaEstacao);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
