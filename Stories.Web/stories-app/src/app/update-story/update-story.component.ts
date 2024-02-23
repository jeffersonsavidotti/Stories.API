import { Component, OnInit, ViewChild } from '@angular/core';
import { StoryService } from '../../services/story.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';
import { Story } from '../../models/story.model';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-story',
  templateUrl: './update-story.component.html',
  styleUrls: ['./update-story.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    FormsModule
  ]
})
export class UpdateStoryComponent implements OnInit {
  @ViewChild('storyForm') storyForm!: NgForm;
  showSuccessMessage = false;
  currentStoryId?: number;
  story: Story = {
    id: undefined,
    title: '',
    description: '',
    department: '',
    positiveVotesCount: 0,
    negativeVotesCount: 0
  };

  constructor(
    private storyService: StoryService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.currentStoryId = this.route.snapshot.params['id'];
    if (this.currentStoryId) {
      this.loadCurrentStory(this.currentStoryId);
    }
    this.route.params.subscribe(params => {
      const storyId = params['id'];
      if (storyId) {
        this.storyService.getStoryById(storyId).subscribe(story => {
          this.story = story;
        }, error => {
          console.error('Erro ao carregar a história', error);
        });
      }
    });
  }

  loadCurrentStory(id: number) {
    this.storyService.getStoryById(id).subscribe({
      next: (story) => {
        this.story = story;
      },
      error: (error) => console.error('Erro ao carregar a história', error)
    });
  }

  updateStory() {
    if (this.currentStoryId) {
      this.storyService.updateStory(this.currentStoryId, this.story).subscribe({
        next: () => {
          console.log('História atualizada com sucesso');
          this.showSuccessMessage = true;
          setTimeout(() => {
            this.showSuccessMessage = false;
            this.router.navigate(['/stories']);
          }, 3000);
        },
        error: (error) => console.error('Erro ao atualizar história', error)
      });
    }
  }


  goToHome() {
    this.router.navigate(['/stories']);
  }
}
