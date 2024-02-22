// user.component.ts
import { UserService } from '../services/user.service';
import { Component, NgModule, OnInit, ViewChild } from '@angular/core';
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
  @ViewChild('userForm') userForm!: NgForm;
  showSuccessMessage: boolean = false; // Propriedade para controlar a exibição da mensagem de sucesso

  constructor(private userService: UserService, private router: Router) { }

  addUser(userFormData: any) {
    this.userService.addUser(userFormData).subscribe({
      next: (res) => {
        console.log('Usuário adicionado', res);
        this.showSuccessMessage = true; // Exibe a mensagem de sucesso
        setTimeout(() => this.showSuccessMessage = false, 3000); // Esconde a mensagem após 3 segundos
        this.userForm.resetForm(); // Limpa os campos do formulário
      },
      error: (e) => console.error(e)
    });
  }

  // Navigate to the users page
  goToHome() {
    this.router.navigate(['/stories']);
    }
}
