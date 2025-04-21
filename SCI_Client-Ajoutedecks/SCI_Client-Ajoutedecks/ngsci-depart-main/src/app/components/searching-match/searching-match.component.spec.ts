import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchingMatchComponent } from './searching-match.component';

describe('SearchingMatchComponent', () => {
  let component: SearchingMatchComponent;
  let fixture: ComponentFixture<SearchingMatchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SearchingMatchComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SearchingMatchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
