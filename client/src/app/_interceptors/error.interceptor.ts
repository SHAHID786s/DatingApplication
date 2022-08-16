import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private toastr: ToastrService) {}
  // to work with the observabl we must use pipe method
  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error) => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                const modalStateErrors = []; // we are going to store errors in here it is the big json payload  from the network all the modal validation is in error.error.errors property which means it will be concise
                for (const x in error.error.errors) {
                  if (error.error.errors[x]) {
                    modalStateErrors.push(error.error.errors[x]); // loop through errors and save them to the array.
                  }
                }

                throw modalStateErrors.flat(); // throw error back to component flat is part of js2019 that falttens json array to the level we need so we can see modal validation error
              } else {
                this.toastr.error(
                  error.statusText === 'OK' ? 'Bad request' : error.statusText,
                  error.status
                );
              }
              break;

            case 401:
              this.toastr.error(
                error.statusText === 'OK' ? 'Unauthorized' : error.statusText,
                error.status
              ); // the code was giving an OK error due to chrome browser settings therefore we have overriden this
              //this.toastr.error(error.statusText, error.status);
              break;
            case 404:
              this.router.navigateByUrl('/not-found'); // we will redirect to notfound page
              break;

            case 500:
              const navExtras: NavigationExtras = {
                state: { error: error.error }, //get the error info from api and pass it with the url and is consumed via router.getCurrentNaviagation() which will give us the error
              };
              this.router.navigateByUrl('/server-error', navExtras);
              break;

            default:
              this.toastr.error('something unexpected ');
              console.log(error);

              break;
          }
        }
        return throwError(error);
      })
    );
  }
}
