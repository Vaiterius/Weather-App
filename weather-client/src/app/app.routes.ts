import { Routes } from '@angular/router';
import { HelloComponent } from './hello/hello.component';
import { CountriesComponent } from './countries/countries.component';
import { CountryCitiesComponent } from './countries/country-cities.component';
import { LoginComponent } from './auth/login.component';

export const routes: Routes = [
    {path: "", component: HelloComponent, pathMatch: "full"},  // pathMatch for matching only the empty string.
    {path: "countries", component: CountriesComponent},
    {path: "country-cities/:id", component: CountryCitiesComponent},
    {path: "login", component: LoginComponent}
];
