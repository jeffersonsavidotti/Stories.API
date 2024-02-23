import { UserService } from '../../services/user.service';
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
  showSuccessMessage: boolean = false;

  constructor(private userService: UserService, private router: Router) { }

  addUser(userFormData: any) {
    this.userService.addUser(userFormData).subscribe({
      next: (res) => {
        console.log('Usuário adicionado', res);
        this.showSuccessMessage = true;
        setTimeout(() => this.showSuccessMessage = false, 3000);
        this.userForm.resetForm();
      },
      error: (e) => console.error(e)
    });
  }

  goToHome() {
    this.router.navigate(['/stories']);
    }
}
