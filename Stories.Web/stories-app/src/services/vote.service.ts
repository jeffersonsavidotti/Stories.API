import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Vote } from '../models/vote.model'; // Ajuste o caminho conforme necessário

@Injectable({
  providedIn: 'root'
})
export class VoteService {
  private apiUrl = 'http://localhost:5298/api/Vote';

  constructor(private http: HttpClient) { }

  vote(vote: Vote): Observable<Vote> {
    return this.http.post<Vote>(this.apiUrl, vote);
  }

  // Dependendo da sua API, você pode precisar de métodos adicionais aqui.
}
