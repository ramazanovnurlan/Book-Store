import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SidenavbaseComponent } from './sidenavbase.component';

describe('SidenavbaseComponent', () => {
  let component: SidenavbaseComponent;
  let fixture: ComponentFixture<SidenavbaseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SidenavbaseComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SidenavbaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
