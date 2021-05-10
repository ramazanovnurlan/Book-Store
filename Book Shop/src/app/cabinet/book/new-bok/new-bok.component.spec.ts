import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewBokComponent } from './new-bok.component';

describe('NewBokComponent', () => {
  let component: NewBokComponent;
  let fixture: ComponentFixture<NewBokComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewBokComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewBokComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
