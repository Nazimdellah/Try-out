import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MesCartesComponent } from './mes-cartes.component';

describe('MesCartesComponent', () => {
  let component: MesCartesComponent;
  let fixture: ComponentFixture<MesCartesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MesCartesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MesCartesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
