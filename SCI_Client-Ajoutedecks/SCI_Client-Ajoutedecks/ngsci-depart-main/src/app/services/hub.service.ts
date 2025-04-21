import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { JoinMatchData, MatchData } from '../models/models';
import { Router } from '@angular/router';
import { MatchService } from './match.service';

@Injectable({
  providedIn: 'root'
})
export class HubService {
  private connectionHub?: signalR.HubConnection
  url : string = 'https://localhost:7179/matchHub'

  enRecherche : boolean = false;
  visible = false;
  victoryVisible = false;

  constructor(public route : Router, public match : MatchService) { }
  
  async connectHub(): Promise<void>{
    this.connectionHub = new signalR.HubConnectionBuilder()
                              .withUrl(this.url, { accessTokenFactory: () => sessionStorage.getItem('token')! })
                              .build();

    this.connectionHub!.on('JoiningMatchData', async (data : JoinMatchData | null) => {
      if(data == null){
        console.log('waiting on another player');
        return;
      }
      if(data){
        //We transform JoinMatchData into MatchData
        let matchData = await this.createMatchData(data);
        //Add it to sessionStorage
        sessionStorage.setItem('matchData', JSON.stringify(matchData));
        this.route.navigate(['/match', data.match.id]);
      }
    });

    this.connectionHub!.on('ApplyEvents', async (data) => {
      console.log(data);
      await this.match.applyEvent(data);
    });

    this.connectionHub!.on('PlayerEndedTurn', async (data) =>{
      await this.match.applyEvent(data);
    });

    this.connectionHub!.on('PlayerSurrendered', async (data) => {
      await this.match.applyEvent(data);
      //Get current playerId
      let playernumId = sessionStorage.getItem('playerNumId');

      //Check if same PlayerId to make overlay happen.
      this.victoryVisible = data.surrenderingPlayerId == playernumId ? false : true;
      this.visible = data.surrenderingPlayerId == playernumId ? true : false;
    });

    return this.connectionHub.start()
    .then(() => {
      console.log('Connection started.');
    }).catch((error) => {
      console.log('Could not start connection : ' + error);
    });
  }

  async startHub(){
    if (this.connectionHub && this.connectionHub.state === signalR.HubConnectionState.Connected){
      return Promise.resolve();
    }
    return await this.connectHub();
  }

  async getConnection(): Promise<signalR.HubConnection | undefined>{
    return Promise.resolve(this.connectionHub ?? undefined);
  }

  async closeConnection() {
    this.connectionHub!.stop().then(() => {
      this.enRecherche = false;
    });
  }

  async createMatchData(data : JoinMatchData): Promise<MatchData> {
    let winner : number = -1;

    if(data.match.winnerUserId){
      let userId = sessionStorage.getItem('playerId'); //IdentityUser ID
      let playernumId = sessionStorage.getItem('playerNumId'); //Player ID

      if(userId && playernumId){
        let playerId = JSON.parse(playernumId);

        if(data.match.winnerUserId == userId){
          winner = playerId;
        }else{
          // Find the opponent’s Player ID
          let playerAUserId = data.playerA.userId;
          let playerBUserId = data.playerB.userId;
          let playerAId = data.playerA.id;
          let playerBId = data.playerB.id;

          if (playerAUserId == data.match.winnerUserId) {
              winner = playerAId;
          } else if (playerBUserId == data.match.winnerUserId) {
              winner = playerBId;
          }
        }
      }
    }
    let matchData : MatchData = {
      match: data.match,
      playerA: data.playerA,
      playerB: data.playerB,
      isStarted: data.isStarted,
      winningPlayerId: winner
    }
    return matchData;
  }

  async getMatchData(): Promise<MatchData | undefined> {
    let jsonData = sessionStorage.getItem('matchData')!;
    let data = JSON.parse(jsonData);
    return Promise.resolve(data ?? undefined);
  }

  async isVictory() {
    let matchData = await this.getMatchData();
    //console.log(matchData);
    //console.log(this.match);
    if(matchData?.winningPlayerId)
      return matchData!.winningPlayerId === this.match.playerData!.playerId
    return false;
  }
}