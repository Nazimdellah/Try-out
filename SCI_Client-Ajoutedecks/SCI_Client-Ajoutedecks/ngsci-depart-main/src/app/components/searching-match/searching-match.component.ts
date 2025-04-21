import { Component, EventEmitter, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { HubService } from 'src/app/services/hub.service';
@Component({
  selector: 'app-searching-match',
  standalone: true,
  imports: [MatProgressSpinnerModule, MatButtonModule],
  templateUrl: './searching-match.component.html',
  styleUrl: './searching-match.component.css'
})
export class SearchingMatchComponent {
  @Output() closeOverlay = new EventEmitter<void>();

  constructor(private hub : HubService){}

  annuler(){
    this.hub.closeConnection();
    this.closeOverlay.emit()
  }
}
