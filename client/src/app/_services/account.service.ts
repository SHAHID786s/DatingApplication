import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';

  private currentUserSource = new ReplaySubject<User>(1); // stores user value and when someone subscribes it gives last value to  in this case as we used 1
  currentUser$ = this.currentUserSource.asObservable(); // watches this object so components will be notified of any changes need to be calle in the html

  constructor(private http: HttpClient) {}
  //a method on the Observable interface that can be used to
  // combine multiple RxJS operators to compose asynchronous operations.
  // The pipe() function takes one or more operators (map(),tap,take)and
  // returns an RxJS Observable.
  login(model: User) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response; // the value we get from api store in user
        if (user) {
          localStorage.setItem('user', JSON.stringify(user)); // adding user to local storage
          this.currentUserSource.next(user); //setting last user value
        }
        return user;
      })
    );
  }
  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }
  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user); //setting last user value
        }
      })
    );
  }
}
