
export interface Player {
    id: number;
    name: string;
    userId: string;
}
export interface Deck {
    id: number;
    name: string;
    carteJoueurs: Array<OwnedCard>; // Liste des cartes possédées
    playerId: number;
    courant: boolean;
    carteSuprime: Array<OwnedCard>;

}
export interface OwnedCard {
    id: number;
    card: Card;
}


export interface Card {
    id: number;
    name: string;
    attack: number;
    health: number;
    cost: number;
    imageUrl: string;
}

export interface MatchData {
    match: Match;
    playerA: Player;
    playerB: Player;
    winningPlayerId: number;
    isStarted: boolean;
}

export interface Match {
    id: number;
    isMatchCompleted: boolean;
    isPlayerATurn: boolean;
    playerDataA: PlayerData;
    playerDataB: PlayerData;
    winnerUserId: string;
}

export interface PlayableCard {
    id: number;
    card: Card;
    health: number;
}

export interface PlayerData {
    id: number;
    health: number;
    maxhealth: number;
    mana: number;
    playerId: number;
    playerName: string;
    cardsPile: PlayableCard[];
    hand: PlayableCard[];
    battleField: PlayableCard[];
    graveyard: PlayableCard[];
}

//JoinMatchData depuis la fonction JoinMatch SignalR
export interface JoinMatchData {
    match: Match;
    playerA: Player;
    playerB: Player;
    isStarted: boolean;
}
