import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Story } from '../models/story.model'; // Ajuste o caminho conforme necessário

@Injectable({
  providedIn: 'root'
})
export class StoryService {
  addStory(storyData: any) {
    throw new Error('Method not implemented.');
  }
  private getAllUrl = 'http://localhost:5298/api/Story';
  private getByIdUrl = 'http://localhost:5298/api/User';
  // private getAllUrl = 'http://localhost:5298/api/Story';
  // private getAllUrl = 'http://localhost:5298/api/Story';

  constructor(private http: HttpClient) { }

  getAllStories(): Observable<Story[]> {
    return this.http.get<Story[]>(this.getAllUrl);
  }

  getStoryById(id: number): Observable<Story> {
    return this.http.get<Story>(`${this.getByIdUrl}/${id}`);
  }

  createStory(story: Story): Observable<Story> {
    return this.http.post<Story>(this.getByIdUrl, story);
  }

  updateStory(id: number, story: Story): Observable<Story> {
    return this.http.put<Story>(`${this.getByIdUrl}/${id}`, story);
  }

  deleteStory(id: number): Observable<any> {
    return this.http.delete(`${this.getByIdUrl}/${id}`);
  }
}
