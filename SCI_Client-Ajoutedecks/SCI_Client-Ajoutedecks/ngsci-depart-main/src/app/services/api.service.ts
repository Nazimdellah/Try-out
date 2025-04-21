import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Card, Deck, Player } from '../models/models';
import { Token } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})

export class ApiService {

  serverUrl = "https://localhost:7179/";
  //serverUrl = "http://localhost:5276/";


  constructor(public http: HttpClient) { }

  async getAllCards(champ: Number | null, ordre: Number | null): Promise<Card[]> {
    let params = new URLSearchParams();
    if (champ !== null) params.append("champ", champ.toString());
    if (ordre !== null) params.append("ordre", ordre.toString());
    let result = await lastValueFrom(this.http.get<Card[]>(this.serverUrl + 'api/card/GetAllCards?' + params.toString()));
    return result;
  }
  async getdeck(): Promise<Deck[]> {
    try {
      const playerId = sessionStorage.getItem("playerNumId")!;
      let temp: number = +playerId;

      let result = await lastValueFrom(this.http.get<Deck[]>(this.serverUrl + 'api/Deck/GetDeck/' + temp));
      console.log(result);
      return result;
    } catch (error) {
      console.error('Error fetching deck:', error);
      throw error;  // rethrow or handle as needed
    }
  }

  async CreateDeck(Deckname: string): Promise<any> {
    const token = sessionStorage.getItem("playerNumId")!;

    console.log(token);

    let bodyRequest = {
      name: Deckname,
      PlayerId: token!
    }
    let result = await lastValueFrom(this.http.post<any>(`${this.serverUrl}api/Deck/CreateDeck`, bodyRequest));
    console.log(result);
    return result;

  }


  async getPlayersCards(champ: Number | null, ordre: Number | null): Promise<Card[]> {
    let params = new URLSearchParams();
    if (champ !== null) params.append("champ", champ.toString());
    if (ordre !== null) params.append("ordre", ordre.toString());
    let result = await lastValueFrom(this.http.get<any>(this.serverUrl + 'api/card/GetPlayersCards?' + params.toString()));
    return result;
  }

  async privateCall(): Promise<string[]> {
    return await lastValueFrom(this.http.get<any>(this.serverUrl + 'api/Users/PrivateData'));
  }
  async deleteCarte(deckId: number, OwnedCardId: number): Promise<any> {


    let result = await lastValueFrom(this.http.delete<any>(`${this.serverUrl}api/Deck/DeleteCarte?DeckId=${deckId}&OwnedCardId=${OwnedCardId}`));
    console.log(result + "Carte supprimée");
    return result;

  }

  //   async addCarte(carteID: number, deck: number): Promise<any> {
  //     const token = sessionStorage.getItem("playerNumId")!;
  //     let AjoutCarte = {

  //       PlayerId: token!,
  //       CarteID: token!,
  //       DeckID: deck,
  //     }
  //     let result = await lastValueFrom(this.http.post<any>(`${this.serverUrl}api/Deck/AjoutDcarte`, AjoutCarte));
  //     console.log(result + "Carte ajoutée");
  //     return result;
  //   }

  //
  async addCarte(DeckID: number, cardID: number): Promise<any> {
    const token = sessionStorage.getItem("playerNumId")!;


    let result = await lastValueFrom(this.http.post<any>(`${this.serverUrl}api/Deck/AjoutDcarte?DeckID=${DeckID}&cardID=${cardID}`, null));
    console.log(result + "Carte ajoutée");
    return result;
  }
  async deleteDeck(deckId: number): Promise<any> {
    let result = await lastValueFrom(this.http.delete<any>(`${this.serverUrl}api/Deck/Deletedeck?DeckId=${deckId}`));
    console.log(result);
    return result;
  }
  async setdeckCourant(deckId: number): Promise<any> {
    const token = sessionStorage.getItem("playerNumId")!;

    let result = await lastValueFrom(this.http.post<any>(`${this.serverUrl}api/Deck/SetCourantDeck?DeckId=${deckId}&PlayerID=${token}`, null));
    console.log(result + "Deck courant");
    return result;

  }
  async getallCards(): Promise<Card[]> {
    let result = await lastValueFrom(this.http.get<Card[]>(this.serverUrl + 'api/Deck/GetAllCard'));
    return result;
  }
}
