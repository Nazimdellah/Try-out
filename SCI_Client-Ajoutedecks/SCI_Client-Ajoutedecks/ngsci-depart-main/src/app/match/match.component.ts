import { FakerService } from './../services/faker.service';
import { Component, OnInit } from '@angular/core';
import { JoinMatchData, Match, MatchData, PlayerData } from '../models/models';
import { MatchService } from './../services/match.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../services/api.service';
import { HealthComponent } from './health/health.component';
import { MatButtonModule } from '@angular/material/button';
import { PlayerhandComponent } from './playerhand/playerhand.component';
import { EnemyhandComponent } from './enemyhand/enemyhand.component';
import { BattlefieldComponent } from './battlefield/battlefield.component';
import { HubConnection, JsonHubProtocol } from '@microsoft/signalr';
import { CommonModule } from '@angular/common';
import { HubService } from '../services/hub.service';


@Component({
    selector: 'app-match',
    templateUrl: './match.component.html',
    styleUrls: ['./match.component.css'],
    standalone: true,
    imports: [BattlefieldComponent, EnemyhandComponent, PlayerhandComponent, MatButtonModule, HealthComponent,CommonModule]
})
export class MatchComponent implements OnInit {
  private connectionHub? : signalR.HubConnection;

  constructor(private route: ActivatedRoute, public router: Router, public matchService:MatchService, public apiService:ApiService, public hub : HubService) { }
  visible = false;
  victoryVisible = false;
  matchData? : MatchData;
  currentPlayerId : number = JSON.parse(sessionStorage.getItem('playerNumId')!);

  async ngOnInit() {
    let matchId:number  = parseInt(this.route.snapshot.params["id"]);
    // TODO Tâche Hub: Se connecter au Hub et obtenir le matchData

    await this.hub.startHub();
    this.connectionHub = await this.hub.getConnection();
    
    this.matchData = await this.hub.getMatchData();
    this.hub.enRecherche = false;

        //   //Manually do it in case you just reload the page
      //this.matchData!.isStarted = true;
      sessionStorage.setItem('matchData', JSON.stringify(this.matchData));

      this.matchService.playMatch(this.matchData!, this.currentPlayerId);
      this.connectionHub!.invoke('StartMatchEvent', this.matchData?.match);
    // if(!this.matchData?.isStarted){
    //   //Manually do it in case you just reload the page
    //   this.matchData!.isStarted = true;
    //   sessionStorage.setItem('matchData', JSON.stringify(this.matchData));

    //   this.matchService.playMatch(this.matchData!, this.currentPlayerId);
    //   this.connectionHub!.invoke('StartMatchEvent', this.matchData?.match);

    // }else{
    //   console.log(this.currentPlayerId);
    //   this.matchService.playMatch(this.matchData, this.currentPlayerId);
    // }
  }

  endMatch() {
    this.matchService.clearMatch();
    sessionStorage.removeItem('matchData');
    this.router.navigate(['/home'])
  }

  surrender(){
    this.connectionHub!.invoke('Surrender', this.matchData?.match.id);
  }

  async endTurn(){
    console.log('Player ended turn');
    await this.connectionHub!.invoke('EndTurn', this.matchData?.match.id);
  }

  isMatchCompleted() {
    return this.matchService.matchData?.match.isMatchCompleted;
  }
}
