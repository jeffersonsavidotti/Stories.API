import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateStoryComponent } from './update-story.component';

describe('AddStoryComponent', () => {
  let component: UpdateStoryComponent;
  let fixture: ComponentFixture<UpdateStoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateStoryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UpdateStoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
