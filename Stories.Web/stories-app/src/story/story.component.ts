import { Component, Input, OnInit } from '@angular/core';
import { StoryService } from '../services/story.service';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Story } from '../models/story.model';

@Component({
  selector: 'app-story',
  templateUrl: './story.component.html',
  styleUrls: ['./story.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    FormsModule,
  ]
})
export class StoryComponent implements OnInit {
  @Input() story?: Story; // Usado para exibir uma única história
  stories: Story[] = []; // Usado para exibir uma lista de histórias

  constructor(private storyService: StoryService) { }

  ngOnInit(): void {
    // Se nenhuma história for fornecida como @Input, busca a lista de histórias
    if (!this.story) {
      this.storyService.getAllStories().subscribe({
        next: (stories) => this.stories = stories,
        error: (e) => console.error(e)
      });
    }
  }
}
