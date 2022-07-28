import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'The Dating App';
  users: any;
  constructor(private http: HttpClient) {
    //requesting data from API
  }

  //like another constructor
  ngOnInit() {
    this.getUsers();
  }

  // the this prop holds all props to this AppComponent
  //class e.g. this.title, this.users, this.onginit
  getUsers() {
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: (response) => (this.users = response),
      error: (error) => console.log(error),
    });
  }
}
