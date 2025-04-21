import { transition, trigger, useAnimation } from '@angular/animations';
import { Component } from '@angular/core';
import { bounce, shake, shakeX, tada } from 'ng-animate';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  animations: [
    trigger('shake', [transition('* => *', useAnimation(shakeX, { params: { timing: 2, delay: 0} }))]),
    trigger('bounce', [transition('* => *', useAnimation(bounce, { params: { timing: 2, delay: 0 } }))]),
  ],

})
export class AppComponent {
  title = 'ngAnimations';
  shakeState = false;


  constructor() {
  }
  triggerShake() {
    this.shakeState = !this.shakeState; // Alterne l'état pour relancer l'animation
  }
}
