import { Deck, Card, OwnedCard } from './../models/models';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

import { ApiService } from '../services/api.service';
import { CardComponent } from '../components/card/card.component';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-deck',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    RouterOutlet,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    CardComponent,
    MatIconModule
  ],
  templateUrl: './deck.component.html',
  styleUrls: ['./deck.component.css']
})
export class DeckComponent {
  decks: Deck[] = [];
  selectedDeck: Deck | null = null;
  cartedebut: Card[] = [];

  deckName: string = '';
  showForm: boolean = false;
  showCardsToAdd: boolean = false;
  currentDeckId: number | null = null;
  cartedepartie: any[] = [];
  /**
   * Represents the collection of cards used in the game.
   * This array holds instances of the `Card` type, which are part of the deck.
   */

  constructor(private apiService: ApiService) { }

  async ngOnInit() {
    try {
      this.decks = await this.apiService.getdeck();
      console.log('Decks chargés :', this.decks);
      if (this.decks.length > 0) {
        console.log('Les decks :', this.decks);
      }
    } catch (error) {
      console.error('Erreur lors du chargement des decks :', error);
    }

    this.getallcard()

  }
  exist(cards: Card, cardsJouer: OwnedCard[]) {
    for (let i = 0; i < cardsJouer.length; i++) {
      if (cards.id == cardsJouer[i].card.id) {
        return true;

      }

    }
    return false;

  }


  toggleForm() {
    this.showForm = !this.showForm;
  }

  async createDeck() {
    try {
      console.log('Nom du deck :', this.deckName);
      const result = await this.apiService.CreateDeck(this.deckName);
      console.log('Deck créé :', result);
      this.decks.push(result);
      this.showForm = false
    } catch (error) {
      console.error('Erreur lors de la création du deck :', error);
    }
    this.ngOnInit(); // Recharge les decks après la création
  }

  async CarteDelete(deckId: number, carte: number) {

    let x = await this.apiService.deleteCarte(deckId, carte)
    this.ngOnInit()
    console.log('Carte supprimée :', carte);
    return x;
  }

  onShowCardsToAdd(deckId: number): void {
    this.showCardsToAdd = true;
    this.currentDeckId = deckId;
  }

  onCloseCardsToAdd(): void {
    this.showCardsToAdd = false;
    this.currentDeckId = null;
  }

  async addCardToDeck(deckId: number, cardID: number) {
    try {
      const result = await this.apiService.addCarte(deckId, cardID);
      console.log('Carte ajoutée :', result);
      this.ngOnInit(); // Recharge les decks après l'ajout
    } catch (error) {
      console.error('Erreur lors de l\'ajout de la carte :', error);
    }
  }
  async deleteDeck(deckId: number) {
    let x = await this.apiService.deleteDeck(deckId);
    this.ngOnInit(); // Recharge les decks après la suppression
    console.log(this.decks);
    console.log("Deck supprimé");
  }

  async rendreDeckCourant(deckId: number) {
    let x = await this.apiService.setdeckCourant(deckId);
    this.ngOnInit(); // Recharge les decks après la mise à jour
    return x;
  }

  async getallcard() {
    this.cartedebut = await this.apiService.getallCards();
    console.log('Cartes de début chargées :', this.cartedebut);

  }
  displayForm() {
    this.showForm = true;
  }
  cancel() {
    this.deckName = '';
    this.showForm = false;
  }
}



