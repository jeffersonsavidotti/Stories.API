import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Story } from '../models/story.model'; // Ajuste o caminho conforme necessário

@Injectable({
  providedIn: 'root'
})
export class StoryService {
  private getUrl = 'https://localhost:7098/api/Story/';

  constructor(private http: HttpClient) { }

  getAllStories(): Observable<Story[]> {
    return this.http.get<Story[]>(this.getUrl);
  }

  getStoryById(id: number): Observable<Story> {
    return this.http.get<Story>(`${this.getUrl}/${id}`);
  }

  createStory(story: Story): Observable<Story> {
    return this.http.post<Story>(this.getUrl, story);
  }

  updateStory(id: number, story: Story): Observable<Story> {
    return this.http.put<Story>(`${this.getUrl}/${id}`, story);
  }

  deleteStory(id: number): Observable<any> {
    return this.http.delete(`${this.getUrl}/${id}`);
  }
}
