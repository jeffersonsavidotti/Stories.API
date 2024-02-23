import { StoryService } from '../../services/story.service';
import { Story } from '../../models/story.model';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { VoteService } from '../../services/vote.service';
import { User } from '../../models/user.model';
import { Vote } from '../../models/vote.model';
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

  constructor(
    private storyService: StoryService,
    private userService: UserService,
    private voteService: VoteService,
    private router: Router
  ) { }

  ngOnInit() {
    this.loadStories();
    this.loadUsers();
  }

  loadStories() {
    this.storyService.getAllStories().subscribe({
      next: (stories) => {
        this.stories = stories.sort((a, b) => {
          const voteSumA = (a.positiveVotesCount ?? 0) - (a.negativeVotesCount ?? 0);
          const voteSumB = (b.positiveVotesCount ?? 0) - (b.negativeVotesCount ?? 0);
          return voteSumB - voteSumA;
        });
        this.applyFilters();
      },
      error: (error) => console.error('Erro ao carregar histórias', error),
    });
  }

  applyFilters() {
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


  loadUsers() {
    this.userService.getAllUsers().subscribe({
      next: (users) => this.users = users,
      error: (error) => console.error('Erro ao carregar usuários', error)
    });
  }

  vote(storyId: number, isPositive: boolean) {
    if (!this.selectedUserId) {
      alert("Escolha um usuário para votar");
      console.error('Nenhum usuário selecionado');
      return;
    }

    const vote = new Vote(storyId, this.selectedUserId, isPositive);

    this.voteService.vote(vote).subscribe({
      next: () => {
        console.log('Voto registrado com sucesso');
        this.showVoteSuccess = true;
        setTimeout(() => this.showVoteSuccess = false, 3000);
        this.loadStories();
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
    this.applyFilters();
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

  updateStory(storyId: number) {
    this.router.navigate(['/update-story', storyId]);
  }

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
