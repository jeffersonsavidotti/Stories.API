import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from '../user/user.component';
import { StoryListComponent } from '../story-list/story-list.component';
import { AddStoryComponent } from '../add-story/add-story.component';
import { StoryComponent } from '../story/story.component';

export const routes: Routes = [
    { path: 'users', component: UserComponent },
    { path: 'stories', component: StoryListComponent },
    {path: 'story', component: StoryComponent},
    { path: 'add-story', component: AddStoryComponent },
    { path: '', redirectTo: '/stories', pathMatch: 'full' } // Redireciona a rota raiz para '/stories'
    // Adicione mais rotas conforme necessário
  ];
  
  @NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }