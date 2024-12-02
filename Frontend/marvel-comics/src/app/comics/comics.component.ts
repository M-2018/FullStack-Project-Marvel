import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';

declare var bootstrap: any;

@Component({
  selector: 'app-comics',
  imports: [CommonModule],
  templateUrl: './comics.component.html',
  styleUrl: './comics.component.css'
})
export class ComicsComponent implements OnInit {
  

  private apiUrl: string = 'https://gateway.marvel.com:443/v1/public/comics?ts=1&apikey=3c30d48604f5535cfa4db14c2e1fe615&hash=ac024cf84f42bb72ed78ad93903e778e';
  private favoritosUrl: string = 'https://localhost:7176/api/Favoritos';  
  comics: any[] = [];
  selectedComic: any = null;
  favorites: number[] = [];
  token: string = '';

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.fetchComics();
    this.getToken();
  }

  getToken(): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.token = token;
    } else {
      console.warn('Token no encontrado');
      this.token = ''; 
    }
  }

  fetchComics(): void {
      this.http.get(this.apiUrl).subscribe(
        (response: any) => {
          this.comics = response.data.results.map((comic: any) => ({
            id: comic.id,
            title: comic.title,
            description: comic.variantDescription,
            thumbnail: `${comic.thumbnail.path}.${comic.thumbnail.extension}`
          }));
        },
        (error) => {
          console.error('Error al obtener los cómics:', error);
        }
      );
    }
  
    showDetails(comic: any): void {
      this.selectedComic = comic;
      const modalElement = document.getElementById('comicModal');
      if (modalElement) {
        const modal = new bootstrap.Modal(modalElement);
        modal.show();
      }
    }
  
    toggleFavorite(comicId: number): void {
      if (this.isFavorite(comicId)) {
        this.favorites = this.favorites.filter(id => id !== comicId);
      } else {
        this.favorites.push(comicId);
      }
    }
  
    isFavorite(comicId: number): boolean {
      return this.favorites.includes(comicId);
    }

    saveFavorites(): void {
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${this.token}`,
        'Content-Type': 'application/json'
      });
  
      const body = {
        ComicIds: this.favorites
      };
  
      this.http.post(this.favoritosUrl, body, { headers,  responseType: 'text' }).subscribe(
        (response) => {
          alert('Favoritos guardados exitosamente');
        },
        (error) => {
          alert('Error al guardar favoritos:');
        }
      );
    }
}
