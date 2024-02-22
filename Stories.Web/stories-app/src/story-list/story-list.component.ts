import { StoryService } from '../services/story.service';
import { Story } from '../models/story.model';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { VoteService } from '../services/vote.service';
import { User } from '../models/user.model';
import { Vote } from '../models/vote.model';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { Router } from '@angular/router';

@Component({
  selector: 'app-story-list',
  templateUrl: './story-list.component.html',
  styleUrls: ['./story-list.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    FormsModule,
    MatSelectModule
  ]
})
export class StoryListComponent implements OnInit {
  stories: Story[] = [];
  filteredStories: Story[] = [];
  searchText: string = '';
  users: User[] = [];
  selectedUserId: number | null = null;
  showVoteSuccess: boolean = false; // Variable to control the display of the vote success message

  constructor(
    private storyService: StoryService,
    private userService: UserService,
    private voteService: VoteService,
    private router: Router // Inject the Router for navigation
  ) { }

  ngOnInit() {
    this.loadStories();
    this.loadUsers();
  }

  loadStories() {
    this.storyService.getAllStories().subscribe({
      next: (stories) => {
        this.stories = stories;
        this.filteredStories = stories; // Inicializa filteredStories com todas as histórias
      },
      error: (error) => console.error('Erro ao carregar histórias', error),
    });
  }

  loadUsers() {
    this.userService.getAllUsers().subscribe({
      next: (users) => this.users = users,
      error: (error) => console.error('Erro ao carregar usuários', error)
    });
  }

  vote(storyId: number, isPositive: boolean) {
    if (!this.selectedUserId) {
      console.error('Nenhum usuário selecionado');
      return;
    }

    const vote = new Vote(storyId, this.selectedUserId, isPositive);

    this.voteService.vote(vote).subscribe({
      next: () => {
        console.log('Voto registrado com sucesso');
        this.showVoteSuccess = true; // Display the success message
        setTimeout(() => this.showVoteSuccess = false, 3000); // Hide the message after 3 seconds
        this.loadStories(); // Optionally: Reload the stories to update vote counts
      },
      error: (error) => console.error('Erro ao registrar voto', error)
    });
  }

  // Navigate to the users page
  goToUsers() {
    this.router.navigate(['/users']);
  }

  // Navigate to the add story page
  goToAddStory() {
    this.router.navigate(['/add-story']);
  }

  searchStories() {
    if (!this.searchText) {
      this.filteredStories = this.stories; // Se a busca estiver vazia, mostra todas as histórias
    } else {
      this.filteredStories = this.stories.filter(story =>
        story.title.toLowerCase().includes(this.searchText.toLowerCase()) ||
        story.description.toLowerCase().includes(this.searchText.toLowerCase()) ||
        story.department.toLowerCase().includes(this.searchText.toLowerCase()) ||
        (story.id && story.id.toString().includes(this.searchText.trim())) // Busca por ID
      );
    }
  }
  
  
  
}
