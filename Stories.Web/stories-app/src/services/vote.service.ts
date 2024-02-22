import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Vote } from '../models/vote.model'; // Ajuste o caminho conforme necessário

@Injectable({
  providedIn: 'root'
})
export class VoteService {
  private apiUrl = 'https://localhost:7098/api/Vote';

  constructor(private http: HttpClient) { }

  // Registrar um voto
  vote(vote: Vote): Observable<Vote> {
    return this.http.post<Vote>(this.apiUrl, vote);
  }

  // Buscar todos os votos (dependendo da sua API, este método pode não ser aplicável)
  getAllVotes(): Observable<Vote[]> {
    return this.http.get<Vote[]>(this.apiUrl);
  }

  // Buscar votos por ID de história (dependendo da sua API, este método pode precisar de ajustes)
  getVotesByStoryId(storyId: number): Observable<Vote[]> {
    return this.http.get<Vote[]>(`${this.apiUrl}/story/${storyId}`);
  }

  // Buscar votos por ID de usuário (dependendo da sua API, este método pode precisar de ajustes)
  getVotesByUserId(userId: number): Observable<Vote[]> {
    return this.http.get<Vote[]>(`${this.apiUrl}/user/${userId}`);
  }

  // Atualizar um voto (se sua aplicação permitir)
  updateVote(voteId: number, vote: Vote): Observable<Vote> {
    return this.http.put<Vote>(`${this.apiUrl}/${voteId}`, vote);
  }

  // Deletar um voto (se sua aplicação permitir)
  deleteVote(voteId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${voteId}`);
  }

  // Dependendo da sua API, você pode precisar de métodos adicionais aqui.
}
