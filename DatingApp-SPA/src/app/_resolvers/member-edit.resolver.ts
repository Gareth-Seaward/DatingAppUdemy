import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';

@Injectable({providedIn: 'root'})
export class MemberEditResolver implements Resolve<User> {

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private router: Router,
    private alertifyService: AlertifyService
  ) {}

  resolve(router: ActivatedRouteSnapshot): Observable<User> {
    // tslint:disable-next-line: no-string-literal
    return this.userService.getUser(this.authService.decodedToken.nameid)
    .pipe(catchError(error => {
      this.alertifyService.error('Problem retrieving your data');
      this.router.navigate(['/members']);
      return of(null); // return to get out.
    }));
  }
}
