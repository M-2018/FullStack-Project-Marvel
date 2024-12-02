import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-menu',
  imports: [RouterLink, CommonModule],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css'
})
export class MenuComponent {
  constructor(private router: Router) {}

  onLogout(): void {
    localStorage.removeItem('token'); 
    this.router.navigate(['/login']);  
  }

  isLoggedIn(): boolean {
    return localStorage.getItem('token') !== null;
  }
  
}
