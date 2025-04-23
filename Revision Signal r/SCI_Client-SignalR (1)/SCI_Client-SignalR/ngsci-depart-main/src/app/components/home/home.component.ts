import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { MatchService } from '../../services/match.service';
import { MatButtonModule } from '@angular/material/button';
import * as signalR from "@microsoft/signalr";
import { JoinMatchData } from 'src/app/models/models';
import { HubService } from 'src/app/services/hub.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    standalone: true,
    imports: [MatButtonModule, RouterOutlet]
})
export class HomeComponent implements OnInit {

  constructor(public router: Router, public match: MatchService, public hub:HubService) { }
  private hubConnection? : signalR.HubConnection;
  matchData? : JoinMatchData;

  async ngOnInit() {
    await this.hub.startHub();

    this.hubConnection = await this.hub.getConnection();
    if(!this.hubConnection){
      console.error("Connection not real");
      return;
    }
  }

  async joinMatch(user1:boolean) {
    // TODO: Angular: Afficher un dialogue qui montre que l'on attend de joindre un match
    // TODO: Hub: Se connecter au Hub et joindre un match
    if(!this.hubConnection){
      console.error("Connection not real");
      return;
    }


    let userId:string = user1 ? "User1Id" : "User2Id";
    if(user1){
      sessionStorage.setItem("playerId", "1");
    }else{
      sessionStorage.setItem("playerId", "2");
    }

    await this.hubConnection!.invoke("JoinMatch", userId, this.hubConnection!.connectionId, null)
      .catch((err) => {
        console.error(err);
      }
    );
  }

}


