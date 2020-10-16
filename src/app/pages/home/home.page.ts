import { Component, OnInit } from '@angular/core';
import { CacheUser } from '../../cache/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
})
export class HomePage implements OnInit {

  user = CacheUser.user;

  constructor() { }

  ngOnInit() {
    console.log("in home: ", this.user.email);
  }

}
