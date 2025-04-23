import { Injectable } from '@angular/core';
import { JoinMatchData, MatchData } from '../models/models';
import * as signalR from "@microsoft/signalr";
import { Router } from '@angular/router';
import { MatchService } from './match.service';

@Injectable({
  providedIn: 'root'
})
export class HubService {

  constructor(public router: Router,  public matchService:MatchService) { }
  private hubConnection? : signalR.HubConnection;
  joinMatchData? : JoinMatchData;
  currentPlayerId? : string = sessionStorage.getItem("playerId")!;
  url : string = "https://localhost:7179/matchHub";
  //url : string = "https://localhost:7219/matchHub";
  
  connectHub(): Promise<void> {

    this.hubConnection = new signalR.HubConnectionBuilder().withUrl(this.url).build();

    //Returns message if waiting on a player.
    this.hubConnection!.on("LookingForOtherPlayer", (data) => {
      console.log(data);
    });

    //Returns JoiningMatchData info and navigates to match.
    this.hubConnection!.on("JoiningMatchData", (data:JoinMatchData) => {
      console.log(data);
      this.joinMatchData = data;
      if(this.joinMatchData){
        this.router.navigate(["/match", this.joinMatchData.match.id]);
        sessionStorage.setItem("joinMatchData", JSON.stringify(data));
      }
    });

    this.hubConnection!.on("startMatchInfo", async (data) => {
      if(this.joinMatchData == undefined) {
        this.joinMatchData = await this.getMatch();
      }
      
      let matchData : MatchData = {
        match:this.joinMatchData!.match,
        playerA:this.joinMatchData!.playerA,
        playerB:this.joinMatchData!.playerB,
        //Possibly have to change this
        winningPlayerId:-1
      }
      this.matchService.playMatch(matchData, +this.currentPlayerId!);
      console.log("All the events");
      console.log(data);
      this.matchService.applyEvent(data);
    });
    
    this.hubConnection!.on("PlayerEndTurn", (data) =>{
      console.log(data)
      this.matchService.applyEvent(data);
    });

    this.hubConnection!.on("SurrenderReturn", (data) =>{
      console.log(data)
      this.matchService.applyEvent(data);
      sessionStorage.removeItem("matchData");
      this.router.navigate(['/']);
    });

    return this.hubConnection.start()
        .then(() => {
            console.log("Connection started");
        })
        .catch(err => {
            console.error('Error while starting connection:', err);
            throw err;
        });

    
  }

  startHub(): Promise<void> {
    //Checks if connection already exists
    if (this.hubConnection && this.hubConnection.state === signalR.HubConnectionState.Connected) {
      return Promise.resolve();
    }
    return this.connectHub();
  }

  getConnection(): Promise<signalR.HubConnection | undefined> {
    return Promise.resolve(this.hubConnection ?? undefined);
  }

  getMatch(): Promise<JoinMatchData | undefined> {
    let storedMatchData = sessionStorage.getItem("joinMatchData");
    if(storedMatchData){
      this.joinMatchData = JSON.parse(storedMatchData);
      return Promise.resolve(this.joinMatchData);
    }
    return Promise.resolve(undefined);
  }

  getPlayerId() {
    return sessionStorage.getItem("playerId");     
  }
}
