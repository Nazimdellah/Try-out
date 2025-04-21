import { transition, trigger, useAnimation } from '@angular/animations';
import { Component } from '@angular/core';
import { bounce, shake, shakeX, tada } from 'ng-animate';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  animations: [
    trigger('shake', [transition('* => *', useAnimation(shakeX, { params: { timing: 2, delay: 0 } }))]),
    trigger('bounce', [transition('* => *', useAnimation(bounce, { params: { timing: 2, delay: 0 } }))]),
  ],
})
export class AppComponent {
  title = 'ngAnimations';
  shakeState = false;
  bounceState = false;
  bouble = false;

  constructor() {}

  triggerShake() {
    this.shakeState = !this.shakeState; // Toggle shake state to trigger animation
  }

  onShakeDone(event: any) {
    if (event.triggerName === 'shake') {
      this.bounceState = !this.bounceState; // Start bounce animation after shake ends
    }
  }

}
