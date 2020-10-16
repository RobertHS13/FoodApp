import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../services/login.service';
import { User } from '../../interfaces';
import { AlertController } from '@ionic/angular';
import { NavController } from '@ionic/angular';
import { CacheUser } from '../../cache/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  user: User = {
    firstName: undefined,
    lastName: undefined,
    type: "Default",
    phone: undefined,
    email: undefined, 
    password: undefined,
  };

  confirm: string;

  segmentLogin = 'login';
  segmentRegistration= 'registration';
  segmentId = this.segmentLogin;

  subtitles = new Map<string, string>([
    [this.segmentLogin, 'Log in'],
    [this.segmentRegistration, 'Register']
  ]);

  btnText = new Map<string, string>([
    [this.segmentLogin, 'Sing in'],
    [this.segmentRegistration, 'Sign up'],
  ]);

  constructor(protected loginService: LoginService, public alertController: AlertController, private navCtrl: NavController) { }

  ngOnInit() {
  }

  signUp(){
    this.loginService.registerUser(this.user);
    CacheUser.user = this.user;
    this.navCtrl.navigateForward('/home');
  }

  signIn(){
    this.loginService.loginUser(this.user.email, this.user.password);
    CacheUser.user = this.user;
    this.navCtrl.navigateForward('/home');
  }

  onLinkClickLoginRegistration(segmentId: string) {
    this.segmentId = segmentId;
  }

  onBtnClick() {
    switch (this.segmentId) {
      case this.segmentLogin:
        this.signIn(); 
        break;

      case this.segmentRegistration: {
        if(this.confirm != this.user.password)
          this.presentAlert();
        else
          this.signUp();
        break;
      }
    }
  }

  async presentAlert() {
    const alert = await this.alertController.create({
      cssClass: 'my-custom-class',
      header: 'Alert',
      subHeader: 'Please, check out',
      message: 'Passwords do not match.',
      buttons: ['OK']
    });

    await alert.present();
  }

}
