import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/User';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'The Dating App';
  users: any;
  constructor(private accountService: AccountService) {
    //requesting data from API
  }

  //like another constructor
  ngOnInit() {
    this.setCurrentUser();
  }
  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user')); // because we stringified we have to parse to read the user obj
    this.accountService.setCurrentUser(user); // pass the user which is now readable
  }
}
