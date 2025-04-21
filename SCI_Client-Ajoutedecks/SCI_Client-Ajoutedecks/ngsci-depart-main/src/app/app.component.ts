import { lastValueFrom, Observable } from 'rxjs';
import { UserService } from './services/user.service';
import { Component, OnInit } from '@angular/core';
import { MatchService } from './services/match.service';
import { Router, RouterLink, RouterOutlet } from '@angular/router';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatButtonModule } from '@angular/material/button';
import { SearchingMatchComponent } from './components/searching-match/searching-match.component';
import { CommonModule } from '@angular/common';
import { Card } from 'src/app/models/models';
import { ApiService } from './services/api.service';
import { HubService } from './services/hub.service';
import * as signalR from '@microsoft/signalr';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  imports: [
    MatToolbarModule,
    MatIconModule,
    MatChipsModule,
    RouterOutlet,
    MatButtonModule,
    RouterLink,
    SearchingMatchComponent,
    CommonModule
  ],
})
export class AppComponent implements OnInit {
  title = 'supercartesinfinies';
  cards : Card[] = []
  private connectionHub?: signalR.HubConnection;

  async ngOnInit() {

  }

  constructor(public router: Router, public matchService: MatchService, public userService: UserService, public service :ApiService, public hub : HubService) { }

  isLogged() {
    // TODO: Gérer l'affichage du joueur lorsqu'il est connecté
    return !!sessionStorage.getItem('token');
  }

  getUsername() {
    return sessionStorage.getItem('username');
  }

  async logout() {
    await this.userService.logout();
    this.router.navigate(['/login']);

  }

  async joindre(){
    //We start a search (loading modal)
    this.hub.enRecherche = true;
    setTimeout(async ()=>{
      if(this.hub.enRecherche) {
        await this.hub.connectHub();
        this.connectionHub = await this.hub.getConnection();
        await this.joinMatch()
      }
    },1000)
  };

  async joinMatch(){
    await this.connectionHub!.invoke('JoinMatch');
  }
}
