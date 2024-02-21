import { StoryService } from '../services/story.service';
import { Story } from '../models/story.model'; // Assegure-se de ter o modelo Story
import { Component, NgModule, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FormGroup, FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
  selector: 'app-story-list',
  templateUrl: './story-list.component.html',
  styleUrls: ['./story-list.component.css'],
  standalone:true,
  imports:[
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    FormsModule
  ]
})
export class StoryListComponent implements OnInit {
  stories: Story[] = [];

  constructor(private storyService: StoryService) { }

  ngOnInit() {
    this.loadStories();
  }

  loadStories() {
    this.storyService.getAllStories().subscribe({
      next: (stories) => this.stories = stories,
      error: (error) => console.error('Erro ao carregar histórias', error)
    });
  }
}
