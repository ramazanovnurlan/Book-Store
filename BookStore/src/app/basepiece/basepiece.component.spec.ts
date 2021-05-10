import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasepieceComponent } from './basepiece.component';

describe('BasepieceComponent', () => {
  let component: BasepieceComponent;
  let fixture: ComponentFixture<BasepieceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BasepieceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BasepieceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
