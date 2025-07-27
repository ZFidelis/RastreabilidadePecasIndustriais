import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaPeca } from './lista-peca';

describe('ListaPeca', () => {
  let component: ListaPeca;
  let fixture: ComponentFixture<ListaPeca>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListaPeca]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListaPeca);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
