import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { FormControl, FormGroup, ReactiveFormsModule, UntypedFormGroup, Validators } from '@angular/forms';

import { AuthService } from './auth.service';
import { LoginRequest } from './login-request';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink, MatFormFieldModule, MatInputModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  form!: UntypedFormGroup;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.form = new FormGroup({
      username: new FormControl("", Validators.required),
      password: new FormControl("", Validators.required),
    });
  }

  onSubmit() {
    let loginRequest: LoginRequest = <LoginRequest> {
      username: this.form.controls['username'].value,
      password: this.form.controls['password'].value
    }
    this.authService.login(loginRequest).subscribe(
      {
        next: (result) => {
          console.log("Mom loves us?: ", result.message);
          this.router.navigate(['/']);
        },
        error: (error) => console.error(error)
      }
    );
  }
}
