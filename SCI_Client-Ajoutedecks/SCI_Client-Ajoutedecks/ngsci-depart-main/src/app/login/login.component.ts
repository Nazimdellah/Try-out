import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RouterOutlet, Router } from '@angular/router';
import { MatCard } from '@angular/material/card';
import { MatFormField, MatError } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { UserService } from '../services/user.service';


interface LoginDTO {
  LoginEmail: string | null;
  LoginPassword: string | null;
}

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterOutlet, CommonModule, ReactiveFormsModule, FormsModule, MatCard, MatFormField, MatError, MatInput],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  serverError: string | null = null;

  constructor(
    public authService: UserService,
    private fb: FormBuilder,
    public router: Router,
    private cd: ChangeDetectorRef
  ) {
    this.form = this.fb.group({
      LoginEmail: ["", [Validators.required, Validators.email]],
      LoginPassword: ["", [Validators.required, Validators.minLength(1)]]
    });
  }

  ngOnInit(): void { }

  async login(): Promise<void> {
    if (this.form.invalid) return;

    this.serverError = null; // Réinitialiser les erreurs du serveur

    try {
      const { LoginEmail, LoginPassword } = this.form.value;
      await this.authService.login(LoginEmail, LoginPassword);
      this.router.navigate(['home']);
    } catch (error: any) {
      console.error("Erreur complète du serveur :", error);

      let errorMessage = "Une erreur est survenue.";

      if (error) {
        if (typeof error === 'string') {
          errorMessage = error; // Si l'erreur est une chaîne brute
        } else if (error.Message) {
          errorMessage = error.Message; // API retourne "Message"
        } else if (error.message) {
          errorMessage = error.message; // API retourne "message"
        } else if (error.errors) {
          errorMessage = Object.values(error.errors).flat().join(' '); // API retourne "errors"
        } else if (error.status === 400) {
          errorMessage = "Requête invalide. Vérifiez vos informations.";
        } else if (error.status === 401) {
          errorMessage = "Identifiants incorrects.";
        } else if (error.status === 500) {
          errorMessage = "Erreur serveur. Réessayez plus tard.";
        }
      }

      this.serverError = errorMessage;
      this.cd.detectChanges();
    }
  }

  goToRegister() {
    this.router.navigate(['/register']);
  }
}