import { HttpClient } from '@angular/common/http';
import { ThrowStmt } from '@angular/compiler';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter(); // we want to chnage the registerMode property to chnage the display when the cancel button is clicked therefore we emit a value of false in the cancel() function
  model: any = {};

  constructor(
    private accountservices: AccountService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  register() {
    this.accountservices.register(this.model).subscribe(
      (res) => {
        console.log(res);
        this.cancel();
      },
      (error) => {
        console.log(error);
        this.toastr.error(error.error);
      }
    );
  }

  //manipulate the prop from an action in our html so we can
  // send it to parent component which is home component
  //essentially closes the register form once we have registered
  cancel() {
    this.cancelRegister.emit(false);
  }
}
