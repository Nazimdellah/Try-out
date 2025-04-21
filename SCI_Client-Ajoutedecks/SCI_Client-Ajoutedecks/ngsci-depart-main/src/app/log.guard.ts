import { inject } from '@angular/core';
import { CanActivateFn, createUrlTreeFromSnapshot } from '@angular/router';
import { UserService } from './services/user.service';

export const logGuard: CanActivateFn = (route, state) => {
  if(!inject(UserService).isLoggedIn()){
    return createUrlTreeFromSnapshot(route,["/login"])
  }
  return true;
};
