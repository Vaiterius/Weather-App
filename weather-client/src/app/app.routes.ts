import { Routes } from '@angular/router';
import { HelloComponent } from './hello/hello.component';
import { CountriesComponent } from './countries/countries.component';

export const routes: Routes = [
    {path: "", component: HelloComponent, pathMatch: "full"},  // pathMatch for matching only the empty string.
    {path: "countries", component: CountriesComponent}
];
