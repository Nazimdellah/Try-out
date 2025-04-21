import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { MatCard } from '@angular/material/card';
import { MatError, MatFormField } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';



import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UserService } from '../services/user.service';
;

const domain = "https://localhost:7179/";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, MatTabsModule, CommonModule, MatError, MatFormField, MatCard, MatInput],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
   registerForm: FormGroup;
  registerData: any | null = null;
  registerUsername: string = "";
  registerEmail: string = "";
  registerPassword: string = "";
  registerPasswordConfirm: string = "";


  constructor(private fb: FormBuilder, public userService: UserService, public router: Router) {
     this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(1),
        // this.passwordComplexityValidator
      ]],
      confirmPassword: ['', [Validators.required]]
    }
    // , { validators: this.passwordMatchValidator }
    );

    this.registerForm.valueChanges.subscribe(() => {
      this.registerData = this.registerForm.value;
     });
  }

  ngOnInit(): void { }
  // passwordComplexityValidator(control: AbstractControl): ValidationErrors | null {
  //   const password = control.value;
  //   if (!/[A-Z]/.test(password)) {
  //     return { uppercase: true };
  //   }
  //   if (!/[!@#$%^&*(),.?":{}|<>]/.test(password)) {
  //     return { specialCharacter: true };
  //   }
  //   return null;
  // }
  // passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
  //   const password = control.get('password')?.value;
  //   const confirmPassword = control.get('confirmPassword')?.value;

  //   if (password !== confirmPassword) {
  //     control.get('confirmPassword')?.setErrors({ passwordMismatch: true });
  //     return { passwordMismatch: true };
  //   } else {
  //     control.get('confirmPassword')?.setErrors(null);
  //     return null;
  //   }
  // }

  async register(): Promise<void> {
     if (this.registerForm.invalid) {
      return;
    }

     const { email, password, confirmPassword } = this.registerForm.value;

    try {
      const success = await this.userService.register(email, password, confirmPassword);

      if (success) {
        const successLogin = await this.userService.login(email, password);
        if (successLogin?.token) {

          alert("L'enregistrement a été un succès!");
          this.router.navigate(['home']);
        }
      }
    } catch (e: any) {
      console.log(e);
      alert("Erreur pendant l'enregistrement: " + e.message);
      return;
    }

  }

  goToLogin() {
    this.router.navigate(['/login']);
  }

}
