import { Component, OnInit } from '@angular/core';
import { isEmpty } from 'rxjs/operators';
import { AccountService } from 'src/app/_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
  styleUrls: ['./not-found.component.css'],
})
export class NotFoundComponent implements OnInit {
  loggedIn: boolean;
  constructor(private service: AccountService, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.getValidUser(); // need to put inside so it loads with the component
    this.loggedIn;
  }

  getValidUser() {
    //check to see if there is a user if so return to members using the button
    // otherwise return to notfound

    this.service.currentUser$.subscribe((response) => {
      response === null ? (this.loggedIn = false) : (this.loggedIn = true);
    });
    console.log('is the user logged in t or f:' + this.loggedIn);
  }
}
