import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteEstacao } from './delete-estacao';

describe('DeleteEstacao', () => {
  let component: DeleteEstacao;
  let fixture: ComponentFixture<DeleteEstacao>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeleteEstacao]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteEstacao);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
