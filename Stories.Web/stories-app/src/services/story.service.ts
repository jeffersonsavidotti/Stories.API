import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Story } from '../models/story.model';

@Injectable({
  providedIn: 'root'
})
export class StoryService {
  private getUrl = 'https://localhost:7098/api/stories';

  constructor(private http: HttpClient) { }
  //GetAllstories
  getAllStories(): Observable<Story[]> {
    return this.http.get<Story[]>(this.getUrl);
  }
  //GetById
  getStoryById(id: number): Observable<Story> {
    return this.http.get<Story>(`${this.getUrl}/${id}`);
  }
  //AddStory
  createStory(story: Story): Observable<Story> {
    return this.http.post<Story>(this.getUrl, story);
  }
  // Update
  updateStory(id: number, story: Story): Observable<Story> {
    return this.http.put<Story>(`${this.getUrl}/${id}`, story);
  }
  //Delete
  deleteStory(id: number): Observable<any> {
    return this.http.delete(`${this.getUrl}/${id}`);
  }
}
