import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonContent, IonHeader, IonTitle, IonToolbar, MenuController, IonFooter, IonRow, IonCol, IonButton } from '@ionic/angular/standalone';
import { Router } from '@angular/router';
import { applicationRoutes } from 'src/app/app.routes';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.page.html',
  styleUrls: ['./landing.page.scss'],
  standalone: true,
  imports: [IonContent, IonHeader, IonRow, IonCol, IonFooter,IonButton, IonTitle, IonToolbar, CommonModule, FormsModule]
})
export class LandingPage implements OnInit {

  constructor(private menuCtrl: MenuController, private router: Router) { }

  ngOnInit() {
    this.menuCtrl.enable(false);
  }

  register() {
    this.router.navigate([applicationRoutes.auth.register]);
  }

  async login() {
    this.router.navigate([applicationRoutes.auth.login]);
  }
}
