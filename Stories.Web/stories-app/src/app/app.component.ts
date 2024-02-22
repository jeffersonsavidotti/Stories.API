import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { StoryComponent } from '../story/story.component';
import { AddStoryComponent } from '../add-story/add-story.component';
import { UserComponent } from '../user/user.component';
import { StoryListComponent } from '../story-list/story-list.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet, // Usado para roteamento
    StoryComponent, // Componente de história individual
    AddStoryComponent, // Componente para adicionar uma nova história
    UserComponent, // Componente para gerenciamento de usuário
    StoryListComponent // Componente para listar todas as histórias
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'] // Especifica os estilos para o AppComponent
})
export class AppComponent {
  title = 'stories-app'; // Título do aplicativo, pode ser usado no template
}
