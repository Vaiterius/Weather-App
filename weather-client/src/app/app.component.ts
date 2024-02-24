import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HelloComponent } from "./hello/hello.component";
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from "./navbar/navbar.component";

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    imports: [RouterOutlet, HelloComponent, HttpClientModule, NavbarComponent]
})
export class AppComponent  {
  title = 'weather-client';
}
