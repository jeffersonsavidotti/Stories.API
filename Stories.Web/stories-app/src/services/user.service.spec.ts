import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { UserService } from './user.service';
import { User } from '../models/user.model';

describe('UserService', () => {
  let service: UserService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [UserService]
    });
    service = TestBed.inject(UserService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve all users', () => {
    const dummyUsers: User[] = [
      { id: 1, name: 'User 1' },
      { id: 2, name: 'User 2' }
    ];

    service.getAllUsers().subscribe(users => {
      expect(users.length).toBe(2);
      expect(users).toEqual(dummyUsers);
    });

    const req = httpMock.expectOne('https://localhost:7098/api/User');
    expect(req.request.method).toBe('GET');
    req.flush(dummyUsers);
  });

  it('should retrieve a user by ID', () => {
    const userId = 1;
    const dummyUser: User = { id: userId, name: 'User 1' };

    service.getUserById(userId).subscribe(user => {
      expect(user).toEqual(dummyUser);
    });

    const req = httpMock.expectOne(`https://localhost:7098/api/User/${userId}`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyUser);
  });

  it('should add a new user', () => {
    const newUser: User = { name: 'New User' };

    service.addUser(newUser).subscribe(user => {
      expect(user).toEqual(newUser);
    });

    const req = httpMock.expectOne('https://localhost:7098/api/User');
    expect(req.request.method).toBe('POST');
    req.flush(newUser);
  });

  it('should update an existing user', () => {
    const userId = 1;
    const updatedUser: User = { id: userId, name: 'Updated User' };

    service.updateUser(userId, updatedUser).subscribe(user => {
      expect(user).toEqual(updatedUser);
    });

    const req = httpMock.expectOne(`https://localhost:7098/api/User/${userId}`);
    expect(req.request.method).toBe('PUT');
    req.flush(updatedUser);
  });

  it('should delete a user', () => {
    const userId = 1;

    service.deleteUser(userId).subscribe(response => {
      expect(response).toBeTruthy();
    });

    const req = httpMock.expectOne(`https://localhost:7098/api/User/${userId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });
});
