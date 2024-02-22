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
import { ActivatedRoute, Router } from '@angular/router';

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
  showVoteSuccess: boolean = false;
  storyId?: number;
  isUpdateMode = false;

  constructor(
    private storyService: StoryService,
    private userService: UserService,
    private voteService: VoteService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.loadStories();
    this.loadUsers();
    this.storyId = this.route.snapshot.params['storyId'];
    this.isUpdateMode = !!this.storyId;
  }

  loadStories() {
    this.storyService.getAllStories().subscribe({
      next: (stories) => {
        this.stories = stories;
        this.filteredStories = stories; // Inicializa o filtro com todas as histórias
      },
      error: (error) => console.error('Erro ao carregar histórias', error),
    });
  }

  loadUsers() {
    this.userService.getAllUsers().subscribe({
      next: (users) => this.users = users,
      error: (error) => console.error('Erro ao carregar usuários', error)
    });
  }bambuvbri

  vote(storyId: number, isPositive: boolean) {
    if (!this.selectedUserId) {
      alert('É necessário selecionar um usúario');
      console.error('Nenhum usuário selecionado');
      return;
    }

    const vote = new Vote(storyId, this.selectedUserId, isPositive);

    this.voteService.vote(vote).subscribe({
      next: () => {
        console.log('Voto registrado com sucesso');
        this.showVoteSuccess = true;
        setTimeout(() => this.showVoteSuccess = false, 3000);
        this.loadStories(); // Recarrega histórias para atualizar votos
      },
      error: (error) => console.error('Erro ao registrar voto', error)
    });
  }

  goToUsers() {
    this.router.navigate(['/users']);
  }

  goToAddStory() {
    this.router.navigate(['/add-story']);
  }

  searchStories() {
    if (!this.searchText.trim()) {
      this.filteredStories = this.stories;
    } else {
      this.filteredStories = this.stories.filter(story =>
        story.title.toLowerCase().includes(this.searchText.toLowerCase()) ||
        story.description.toLowerCase().includes(this.searchText.toLowerCase()) ||
        story.department.toLowerCase().includes(this.searchText.toLowerCase()) ||
        (story.id?.toString().includes(this.searchText))
      );
    }
  }
  // Método para atualizar uma história
  updateStory(storyId: number) {
    this.router.navigate(['/update-story', storyId]);
  }

  // Método para adicionar ou atualizar a história
  submitStory(formValue: any) {
    this.router.navigate(['/update-story', this.storyId]);
  }

  // Método para deletar uma história
  deleteStory(storyId: number) {
    if (confirm('Tem certeza que deseja deletar esta história?')) {
      this.storyService.deleteStory(storyId).subscribe({
        next: () => {
          console.log('História deletada com sucesso');
          this.loadStories();
        },
        error: (error) => console.error('Erro ao deletar história', error),
      });
    }
  }
}
