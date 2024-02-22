import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { VoteService } from './vote.service';
import { Vote } from '../models/vote.model';

describe('VoteService', () => {
  let service: VoteService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [VoteService]
    });
    service = TestBed.inject(VoteService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve all votes', () => {
    const dummyVotes: Vote[] = [
      { id: 1, idStory: 1, idUser: 1, voteValue: true },
      { id: 2, idStory: 2, idUser: 2, voteValue: false }
    ];

    service.getAllVotes().subscribe(votes => {
      expect(votes.length).toBe(2);
      expect(votes).toEqual(dummyVotes);
    });

    const req = httpMock.expectOne('https://localhost:7098/api/Vote');
    expect(req.request.method).toBe('GET');
    req.flush(dummyVotes);
  });

  it('should submit a vote', () => {
    const dummyVote: Vote = { id: 1, idStory: 1, idUser: 1, voteValue: true };

    service.vote(dummyVote).subscribe(vote => {
      expect(vote).toEqual(dummyVote);
    });

    const req = httpMock.expectOne('https://localhost:7098/api/Vote');
    expect(req.request.method).toBe('POST');
    req.flush(dummyVote);
  });
});
