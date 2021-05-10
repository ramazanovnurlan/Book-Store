import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookCorouselComponent } from './book-corousel.component';

describe('BookCorouselComponent', () => {
  let component: BookCorouselComponent;
  let fixture: ComponentFixture<BookCorouselComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BookCorouselComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BookCorouselComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
