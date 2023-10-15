import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import ValidationHelper from 'src/app/shared/helpers/validation.helper';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit {
  formSignIn!: FormGroup;
  invalidMessages: string[] = [];
  formErrors = {
    username: '',
    password: '',
  };
  isSubmit: boolean | undefined;
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.createdForm();
  }

  createdForm(): void {
    this.formSignIn = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  validateForm(): boolean {
    this.invalidMessages = ValidationHelper.getInvalidMessages(
      this.formSignIn,
      this.formErrors
    );
    return this.invalidMessages.length === 0;
  }

  onSubmit(): void {
    this.isSubmit = true;
    if (this.validateForm()) {
      const data = {
        username: this.formSignIn.value.username,
        password: this.formSignIn.value.password,
      };
      this.authService
        .login(data)
        .subscribe((result: any) => {
          this.router.navigate(['/dashboard']);
        });
    } else {
      this.router.navigate(['/']);
    }
  }

}
