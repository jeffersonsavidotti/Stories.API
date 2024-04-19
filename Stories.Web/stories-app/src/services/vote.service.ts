import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Vote } from '../models/vote.model';

@Injectable({
  providedIn: 'root'
})
export class VoteService {
  private apiUrl = 'https://localhost:7098/api/votes';

  constructor(private http: HttpClient) { }

  // AddVote
  vote(vote: Vote): Observable<Vote> {
    return this.http.post<Vote>(this.apiUrl, vote);
  }

  // GetaAll
  getAllVotes(): Observable<Vote[]> {
    return this.http.get<Vote[]>(this.apiUrl);
  }

  // GetById
  getVotesByStoryId(storyId: number): Observable<Vote[]> {
    return this.http.get<Vote[]>(`${this.apiUrl}/story/${storyId}`);
  }

  // GetByIdUser
  getVotesByUserId(userId: number): Observable<Vote[]> {
    return this.http.get<Vote[]>(`${this.apiUrl}/user/${userId}`);
  }

  // Update
  updateVote(voteId: number, vote: Vote): Observable<Vote> {
    return this.http.put<Vote>(`${this.apiUrl}/${voteId}`, vote);
  }

  // Deletar (Não vou usar ainda)
  deleteVote(voteId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${voteId}`);
  }
}
