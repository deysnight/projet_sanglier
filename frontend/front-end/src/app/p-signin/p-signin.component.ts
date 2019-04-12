import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-p-signin',
  templateUrl: './p-signin.component.html',
  styleUrls: ['./p-signin.component.css']
})
export class PSigninComponent implements OnInit {

  form;
  constructor(private fb: FormBuilder,
    private myRoute: Router,
    private auth: AuthService) {
    this.form = fb.group({
      username: ['', [Validators.required]],
      password: ['', Validators.required]
    })
  }

 
  ngOnInit() {
  }


  login() {
    if (this.form.valid) {
      //.auth.sendToken(this.form.value.username)
      //this.myRoute.navigate(["dashboard"]);
    }

  }
  /*login(username, userpwd, acca) {
    console.log(acca);
  }*/


}
