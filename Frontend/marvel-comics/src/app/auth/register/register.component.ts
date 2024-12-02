import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent  implements OnInit{
  registerForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      nombreCompleto: ['', [Validators.required]],
      numeroIdentificacion: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  
    onRegister(): void {
      if (this.registerForm.valid) {
        this.authService.register(this.registerForm.value).subscribe({
          next: (response: string) => {
            alert(response); // This will show "Usuario registrado con éxito"
            this.router.navigate(['/login']);
          },
          error: (error) => {
            console.error('Error al registrar usuario:', error);
            alert('Error al registrar usuario');
          }
        });
      }
    }

    
}
