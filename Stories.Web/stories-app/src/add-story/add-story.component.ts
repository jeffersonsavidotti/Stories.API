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
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-story',
  templateUrl: './add-story.component.html',
  styleUrls: ['./add-story.component.css'],
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
export class AddStoryComponent implements OnInit {
  @ViewChild('storyForm') storyForm!: NgForm;
  showSuccessMessage = false; // Controle para a mensagem de sucesso

  constructor(private storyService: StoryService, private router: Router) { }

  ngOnInit() {}

  addStory(storyData: any) {
    this.storyService.createStory(storyData).subscribe({
      next: (response) => {
        console.log('História adicionada', response);
        this.showSuccessMessage = true; // Mostra a mensagem de sucesso
        this.storyForm.resetForm(); // Limpa o formulário
        setTimeout(() => this.showSuccessMessage = false, 3000); // Esconde a mensagem após 3 segundos
      },
      error: (error) => console.error('Erro ao adicionar história', error)
    });
  }
  // Navigate to the users page
  goToHome() {
  this.router.navigate(['/stories']);
  }
}

