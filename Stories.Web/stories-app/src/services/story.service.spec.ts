import { TestBed, fakeAsync, tick } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { StoryService } from './story.service';
import { Story } from '../models/story.model';

describe('StoryService', () => {
  let service: StoryService;
  let httpTestingController: HttpTestingController;
  const testUrl = 'https://localhost:7098/api/Story';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [StoryService],
    });
    service = TestBed.inject(StoryService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should retrieve all stories', fakeAsync(() => {
    const testStories: Story[] = [
      { id: 1, title: 'Story 1', description: 'Description 1', department: 'Department 1', positiveVotesCount: 10, negativeVotesCount: 2 },
      { id: 2, title: 'Story 2', description: 'Description 2', department: 'Department 2', positiveVotesCount: 5, negativeVotesCount: 1 }
    ];

    service.getAllStories().subscribe(stories => {
      expect(stories.length).toBe(2);
      expect(stories).toEqual(testStories);
    });

    const req = httpTestingController.expectOne(testUrl);
    expect(req.request.method).toEqual('GET');
    req.flush(testStories);
    tick();
  }));

  it('should retrieve a story by id', fakeAsync(() => {
    const testStory: Story = { id: 1, title: 'Story 1', description: 'Description 1', department: 'Department 1', positiveVotesCount: 10, negativeVotesCount: 2 };

    service.getStoryById(1).subscribe(story => {
      expect(story).toEqual(testStory);
    });

    const req = httpTestingController.expectOne(`${testUrl}/1`);
    expect(req.request.method).toEqual('GET');
    req.flush(testStory);
    tick();
  }));

  it('should add a new story and return it', fakeAsync(() => {
    const newStory: Story = { id: 0, title: 'New Story', description: 'New Description', department: 'New Department', positiveVotesCount: 0, negativeVotesCount: 0 };

    service.createStory(newStory).subscribe(story => {
      expect(story).toEqual({...newStory, id: 3}); // Assuming the backend assigns id 3 to the new story
    });

    const req = httpTestingController.expectOne(testUrl);
    expect(req.request.method).toEqual('POST');
    expect(req.request.body).toEqual(newStory);
    req.flush({...newStory, id: 3});
    tick();
  }));

  it('should update the story and return it', fakeAsync(() => {
    const updatedStory: Story = {
      id: 1,
      title: 'Updated Story',
      description: 'Updated Description',
      department: 'Updated Department',
      positiveVotesCount: 5,
      negativeVotesCount: 1
    };

    if (typeof updatedStory.id === 'number') {
      service.updateStory(updatedStory.id, updatedStory).subscribe(story => {
        expect(story).toEqual(updatedStory);
      });

      const req = httpTestingController.expectOne(`${testUrl}/${updatedStory.id}`);
      expect(req.request.method).toEqual('PUT');
      expect(req.request.body).toEqual(updatedStory);
      req.flush(updatedStory);
      tick();
    } else {
      fail('Updated story id is undefined or not a number');
    }
  }));

  it('should delete the story and return nothing', fakeAsync(() => {
    const storyId = 1;

    service.deleteStory(storyId).subscribe(response => {
      expect(response).toBeNull();
    });

    const req = httpTestingController.expectOne(`${testUrl}/${storyId}`);
    expect(req.request.method).toEqual('DELETE');
    req.flush(null);
    tick();
  }));

  // Example test for handling errors
  it('should handle errors', fakeAsync(() => {
    const errorMessage = 'An error occurred';

    service.getAllStories().subscribe(
      () => fail('Expected an error, not stories'),
      error => expect(error.message).toContain(errorMessage)
    );

    const req = httpTestingController.expectOne(testUrl);
    req.flush(errorMessage, { status: 500, statusText: 'Server Error' });
    tick();
  }));
});
