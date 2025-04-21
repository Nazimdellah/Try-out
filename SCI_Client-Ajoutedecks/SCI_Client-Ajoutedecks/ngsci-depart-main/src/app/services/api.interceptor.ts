import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
    // Authentification avec token
    intercept(req: HttpRequest<any>, handler: HttpHandler): Observable<HttpEvent<any>> {
        let token = sessionStorage.getItem('token');

        if (token) {
            console.log("Token ajouté");
            req = req.clone({
                setHeaders: {
                    Authorization: `Bearer ${token}` 
                }
            });
        } else {
            console.warn("Aucun token trouvé dans le sessionStorage");
        }

        return handler.handle(req);
    }
}