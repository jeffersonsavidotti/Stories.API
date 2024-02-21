import { Component, NgModule, OnInit } from '@angular/core';
import { StoryService } from '../services/story.service';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FormGroup, FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';

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
export class AddStoryComponent {
  constructor(private storyService: StoryService) { }

  addStory(storyData: any) {
    this.storyService.createStory(storyData).subscribe({
      next: (response: any) => console.log('História adicionada', response),
      error: (error: any) => console.error('Erro ao adicionar história', error)
    });
    
  }
}
