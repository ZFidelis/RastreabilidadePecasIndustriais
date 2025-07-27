import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPeca } from './edit-peca';

describe('EditPeca', () => {
  let component: EditPeca;
  let fixture: ComponentFixture<EditPeca>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditPeca]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditPeca);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
