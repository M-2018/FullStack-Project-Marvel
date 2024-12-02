import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import * as jwtDecode from 'jwt-decode';

interface TokenPayload {
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier': string;
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress': string;
  sub: string;
  jti: string;
  exp: number;
  iss: string;
  aud: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7176/api/Auth';
  private userId: string | null = null;

  constructor(private http: HttpClient) {
    this.extractUserIdFromToken();
  }

  register(data: any): Observable<string> {
    return this.http.post(`${this.apiUrl}/register`, data, { 
      responseType: 'text' 
    });
  }

  login(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, data);
  }

  extractUserIdFromToken(): void {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const decoded = jwtDecode.jwtDecode<TokenPayload>(token);
        this.userId = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
        
        
        console.log('User ID:', this.userId);
      } catch (error) {
        console.error('Error decoding token:', error);
        this.userId = null;
      }
    }
  }

  getUserId(): string | null {
    return this.userId;
  }

  getNameIdentifier(): string | null {
    return this.getUserId();
  }
}