import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(private router: Router, private toastr: ToastrService) {
        
    }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // Using pipe to catch the error outside of the obs
        return next.handle(req).pipe(
            catchError(error => {
                if (error) {
                    // Maybe in the future there`s a need to navigate to other error component
                    switch (error.status) {
                        case 400:
                            // Second error is our error from the API returned
                            if (error.error.errors) {
                                // This is because we want the validation error to be displayed
                                throw error.error;
                            } else {
                                this.toastr.error(error.error.message, error.error.statusCode);
                            }
                            break;
                        case 401:
                            this.toastr.error(error.error.message, error.error.statusCode);
                            break;
                        case 404:
                            this.router.navigateByUrl('/error');
                            break;
                        case 500:
                            // passing state to component via router
                            // Creating the object error which will have the API error exception
                            const navigationExtras: NavigationExtras = {state: {error: error.error}};
                            this.router.navigateByUrl('/error', navigationExtras);
                            break;
                        default:
                            break;
                    }
                }

                // Handle case out of the status code. ( needs another approach )
                return throwError(error);
            })
        );
    }
}
