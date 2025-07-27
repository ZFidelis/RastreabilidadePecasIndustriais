import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaMovimentos } from './lista-movimentos';

describe('ListaMovimentos', () => {
  let component: ListaMovimentos;
  let fixture: ComponentFixture<ListaMovimentos>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListaMovimentos]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListaMovimentos);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
