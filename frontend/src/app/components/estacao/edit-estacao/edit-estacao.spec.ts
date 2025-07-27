import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditEstacao } from './edit-estacao';

describe('EditEstacao', () => {
  let component: EditEstacao;
  let fixture: ComponentFixture<EditEstacao>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditEstacao]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditEstacao);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
