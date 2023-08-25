import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { Observable, throwError } from 'rxjs';
import { tap } from 'rxjs/internal/operators/tap';
import { catchError, map } from 'rxjs/operators';
@Injectable({providedIn: 'root'})
export class ErrorToastrInterceptor implements HttpInterceptor{
  constructor(private router:Router,private toastr: ToastrService){

  }
  intercept(req: HttpRequest<any>,next: HttpHandler): Observable<HttpEvent<any>>{

      return next.handle(req).pipe(tap(
        succ => {


        },
        err =>{
          if(err.status == 400){
            err.error.forEach((element: string) => {
              this.toastr.error(element);
            });
          }else{
            this.toastr.error(err.error)
          }
        }
      ));
  }
}
