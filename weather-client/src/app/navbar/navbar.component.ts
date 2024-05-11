import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";
import {MatToolbarModule} from "@angular/material/toolbar";
import { Subject, takeUntil } from 'rxjs';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, MatIconModule, MatToolbarModule, MatButtonModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit, OnDestroy {
  public isLoggedIn = false;
  private _destroySubject = new Subject();

  public constructor(private authService: AuthService, private router: Router) {
    this.authService.authStatus.pipe(takeUntil(this._destroySubject))
      .subscribe(result => {
        this.isLoggedIn = result;
      })
  }

  public ngOnInit(): void {
    this.isLoggedIn = this.authService.isAuthenticated();
  }

  public ngOnDestroy(): void {
    this._destroySubject.next(true);
    this._destroySubject.complete();
  }

  public onLogout(): void {
    this.authService.logout();
    this.router.navigate(['login'])
  }
}
