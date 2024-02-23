import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { StoryComponent } from './story/story.component';
import { AddStoryComponent } from './add-story/add-story.component';
import { UserComponent } from './user/user.component';
import { StoryListComponent } from './story-list/story-list.component';
import { UpdateStoryComponent } from './update-story/update-story.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet, //roteamento
    StoryComponent,
    AddStoryComponent,
    UserComponent,
    StoryListComponent,
    UpdateStoryComponent
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'stories-app';
}
