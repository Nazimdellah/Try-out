import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControlOptions, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { MatCard } from '@angular/material/card';
import { MatError, MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';
import {MatSelectModule} from '@angular/material/select';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CardComponent } from '../card/card.component';
import { Card } from 'src/app/models/models';
import { ApiService } from 'src/app/services/api.service';

import { UserService } from 'src/app/services/user.service';


@Component({
  selector: 'app-tri',
  standalone: true,
  imports: [ReactiveFormsModule, MatTabsModule, CommonModule, MatError, MatFormField, MatCard, MatInput,MatLabel,MatSelectModule, FormsModule,RouterModule,CardComponent],
  templateUrl: './tri.component.html',
  styleUrl: './tri.component.css'
})

export class TriComponent implements OnInit{
  selectedValue: number = 0;
  selectedOrdre: number = 0;
  cardList : Array<Card> = [];
  isMagasin : boolean = true
  constructor(public service : ApiService, private route: ActivatedRoute){

  }
  async ngOnInit() {
    this.route.data.subscribe(data => {
      if(data['comp'] == "cartes"){
        this.isMagasin = false
      }
    })
    if(this.isMagasin){
      this.cardList = await this.service.getAllCards(this.selectedValue,this.selectedOrdre)
    }
    else{
      this.cardList = await this.service.getPlayersCards(this.selectedValue,this.selectedOrdre)
    }
    console.log(this.cardList)
  }

  async trier(){
    if(this.isMagasin){
      this.cardList = await this.service.getAllCards(this.selectedValue,this.selectedOrdre)
    }
    else{
      this.cardList = await this.service.getPlayersCards(this.selectedValue,this.selectedOrdre)
    }

  }


}
