import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CountryCitiesComponent } from './country-cities.component';

describe('CountryCitiesComponent', () => {
  let component: CountryCitiesComponent;
  let fixture: ComponentFixture<CountryCitiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CountryCitiesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CountryCitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
