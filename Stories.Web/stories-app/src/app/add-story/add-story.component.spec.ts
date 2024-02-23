import { ComponentFixture, TestBed, fakeAsync, tick, flush } from '@angular/core/testing';
import { AddStoryComponent } from './add-story.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { StoryService } from '../../services/story.service';
import { FormsModule } from '@angular/forms';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { By } from '@angular/platform-browser';

describe('AddStoryComponent', () => {
  let component: AddStoryComponent;
  let fixture: ComponentFixture<AddStoryComponent>;
  let storyService: StoryService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddStoryComponent ],
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        FormsModule,
        NoopAnimationsModule,
        MatInputModule,
        MatFormFieldModule,
        MatButtonModule,
        MatIconModule,
        MatCardModule
      ],
      providers: [ StoryService ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddStoryComponent);
    component = fixture.componentInstance;
    storyService = TestBed.inject(StoryService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // it('should call addStory and show success message on successful addition', fakeAsync(() => {
  //   spyOn(storyService, 'createStory').and.returnValue(Promise.resolve({id: 123, title: 'New Story', description: 'New story description', department: 'IT'}));
  //   const form = fixture.debugElement.query(By.directive(NgForm));
  //   form.triggerEventHandler('ngSubmit', null);

  //   tick();

  //   expect(storyService.createStory).toHaveBeenCalled();
  //   expect(component.showSuccessMessage).toBeTrue();

  //   flush();
  // }));

});
