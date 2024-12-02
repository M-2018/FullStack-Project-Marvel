import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }



    onLogin(): void {
      if (this.loginForm.valid) {
        this.authService.login(this.loginForm.value).subscribe({
          next: (response) => {
            localStorage.setItem('token', response.token);
            
            this.authService.extractUserIdFromToken();
    
            this.router.navigate(['/comics']);
          },
          error: (error) => {
            alert('Usuario o contraseña incorrectos');
          }
        });
      }
    }
    

    
}
