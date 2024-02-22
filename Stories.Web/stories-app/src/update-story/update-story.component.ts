import { Component, NgModule, OnInit, ViewChild } from '@angular/core';
import { StoryService } from '../services/story.service';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FormGroup, FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-update-story',
  templateUrl: './update-story.component.html',
  styleUrls: ['./update-story.component.css'],
  standalone:true,
  imports:[
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    FormsModule
]})
export class UpdateStoryComponent implements OnInit {
  showSuccessMessage: boolean = false;
  storyId?: number;
  isUpdateMode = false;

  constructor(
    private storyService: StoryService,
    private router: Router,
    private route: ActivatedRoute
    ) { }

    ngOnInit() {
      this.storyId = this.route.snapshot.params['storyId'];
      this.isUpdateMode = !!this.storyId;
    }

  updateStory(storyData: any) {
    if (!storyData.id) {
      console.error('ID da história é obrigatório para atualização');
      return;
    }

    this.storyService.updateStory(storyData.id, storyData).subscribe({
      next: () => {
        console.log('História atualizada com sucesso');
        this.showSuccessMessage = true;
        setTimeout(() => {
          this.showSuccessMessage = false;
          this.router.navigate(['/stories']);
        }, 3000); // Redireciona após a mensagem de sucesso
      },
      error: (error) => console.error('Erro ao atualizar história', error)
    });
  }

  // Método para adicionar ou atualizar a história
  submitStory(formValue: any) {
      this.router.navigate(['/update-story', this.storyId]);
  }

  goToHome() {
    this.router.navigate(['/stories']);
  }
}

