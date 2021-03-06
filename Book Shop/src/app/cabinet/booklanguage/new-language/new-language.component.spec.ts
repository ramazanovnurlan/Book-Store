import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewLanguageComponent } from './new-language.component';

describe('NewLanguageComponent', () => {
  let component: NewLanguageComponent;
  let fixture: ComponentFixture<NewLanguageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewLanguageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewLanguageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
