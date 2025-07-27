import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeletePeca } from './delete-peca';

describe('DeletePeca', () => {
  let component: DeletePeca;
  let fixture: ComponentFixture<DeletePeca>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeletePeca]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeletePeca);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
