import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { IonButton, IonContent, IonHeader, IonInput, IonItem, IonTitle, IonToolbar, MenuController } from '@ionic/angular/standalone';
import { AuthService, LoginRequestModel, RegisterRequestModel } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.scss'],
  standalone: true,
  imports: [IonContent, IonHeader, IonTitle, IonToolbar, CommonModule, IonButton, IonItem, ReactiveFormsModule, IonInput]
})
export class RegisterPage implements OnInit {

  credentials!: FormGroup;

  constructor(
    private fb: FormBuilder, private menuCtrl: MenuController, private authService: AuthService) {
    this.credentials = this.fb.group({
      email: [null, [Validators.email]],
      password: [null, [Validators.required, Validators.minLength(6)]]
    });
  }

  ngOnInit() {
    this.menuCtrl.enable(false);
  }

  async login() {
    if (this.credentials.valid) {
      this.authService.register({ password: this.getPassword?.value, email: this.getEmail?.value } as RegisterRequestModel).subscribe()
    }
  }

  // Easy access for form fields
  get getEmail() {
    return this.credentials.get('email')!;
  }

  get getPassword() {
    return this.credentials.get('password')!;
  }
}
