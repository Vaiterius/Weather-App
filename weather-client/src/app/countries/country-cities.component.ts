import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { City } from './city';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';

@Component({
  selector: 'app-country-cities',
  standalone: true,
  imports: [],
  templateUrl: './country-cities.component.html',
  styleUrl: './country-cities.component.scss'
})
export class CountryCitiesComponent {
  public cities: City[] = [];
  public id: number;

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute
    ) {
      let 
      idParameter = this.route.snapshot.paramMap.get('id')!;
      this.id = idParameter ? +idParameter : 0;  // Converts into number if returned a string properly.
    }

  ngOnInit(): void {
    this.getCities(this.id);
  }

  getCities(id: number) {
    this.http.get<City[]>(environment.baseUrl + `/api/Countries/CountryCities/${id}`).subscribe(
      {
        next: result => this.cities = result,
        error: error => console.error(error)
      }
    );
  }
}
