import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { MatchComponent } from './match/match.component';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { MesCartesComponent } from './components/mes-cartes/mes-cartes.component';
import { MagasinComponent } from './components/magasin/magasin.component';
import { TriComponent } from './components/tri/tri.component';
import { logGuard } from './log.guard';
import { DeckComponent } from './deck/deck.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },

  { path: 'home', component: WelcomeComponent, canActivate:[logGuard] },

  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

  { path: 'match/:id', component: MatchComponent, canActivate:[logGuard] },
  { path: 'deck', component: DeckComponent},

  { path: 'mes-cartes', component: MesCartesComponent, canActivate:[logGuard], children: [
    { path: '', component: TriComponent, data: { comp: 'cartes' } }
  ]},

  { path: 'magasin', component: MagasinComponent, canActivate:[logGuard], children: [
    { path: '', component: TriComponent, data: { comp: 'magasin' } }
  ]},
  
  

  { path: '**', redirectTo: '/home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
