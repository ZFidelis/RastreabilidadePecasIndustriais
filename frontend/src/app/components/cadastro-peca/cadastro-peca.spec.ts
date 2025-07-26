import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroPeca } from './cadastro-peca';

describe('CadastroPeca', () => {
  let component: CadastroPeca;
  let fixture: ComponentFixture<CadastroPeca>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CadastroPeca]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CadastroPeca);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
