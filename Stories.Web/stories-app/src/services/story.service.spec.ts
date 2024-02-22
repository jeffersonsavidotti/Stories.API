import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { StoryService } from './story.service';
import { Story } from '../models/story.model';

describe('StoryService', () => {
  let service: StoryService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [StoryService]
    });
    service = TestBed.inject(StoryService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve all stories', () => {
    const mockStories: Story[] = [
      { id: 1, title: 'Story 1', description: 'Description 1', department: 'Department 1' },
      { id: 2, title: 'Story 2', description: 'Description 2', department: 'Department 2' }
    ];

    service.getAllStories().subscribe(stories => {
      expect(stories.length).toBe(2);
      expect(stories).toEqual(mockStories);
    });

    const req = httpMock.expectOne('https://localhost:7098/api/Story/');
    expect(req.request.method).toBe('GET');

    req.flush(mockStories);
  });

  it('should retrieve a story by ID', () => {
    const storyId = 1;
    const mockStory: Story = { id: storyId, title: 'Test Story', description: 'Test Description', department: 'Test Department' };

    service.getStoryById(storyId).subscribe(story => {
      expect(story).toEqual(mockStory);
    });

    const req = httpMock.expectOne(`https://localhost:7098/api/Story/${storyId}`);
    expect(req.request.method).toBe('GET');

    req.flush(mockStory);
  });

  it('should create a new story', () => {
    const newStory: Story = { title: 'New Story', description: 'New Description', department: 'New Department' };

    service.createStory(newStory).subscribe(story => {
      expect(story).toEqual(newStory);
    });

    const req = httpMock.expectOne('https://localhost:7098/api/Story/');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(newStory);

    req.flush(newStory);
  });

  it('should update an existing story', () => {
    const storyId = 1;
    const updatedStory: Story = { id: storyId, title: 'Updated Story', description: 'Updated Description', department: 'Updated Department' };

    service.updateStory(storyId, updatedStory).subscribe(story => {
      expect(story).toEqual(updatedStory);
    });

    const req = httpMock.expectOne(`https://localhost:7098/api/Story/${storyId}`);
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(updatedStory);

    req.flush(updatedStory);
  });

  it('should delete a story', () => {
    const storyId = 1;

    service.deleteStory(storyId).subscribe(response => {
      expect(response).toBeTruthy();
    });

    const req = httpMock.expectOne(`https://localhost:7098/api/Story/${storyId}`);
    expect(req.request.method).toBe('DELETE');

    req.flush({});
  });
});
