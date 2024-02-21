// user.component.ts
import { UserService } from '../services/user.service';
import { Component, NgModule, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FormGroup, FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';



@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
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
export class UserComponent {
  constructor(private userService: UserService) { }

  addUser(userFormData: any) {
    this.userService.addUser(userFormData).subscribe({
      next: (res) => {
        console.log('Usuário adicionado', res);
        // Atualize a lista de usuários ou redirecione conforme necessário
      },
      error: (e) => console.error(e)
    });
  }
}
