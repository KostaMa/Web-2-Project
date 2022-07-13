import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { NgForm, ReactiveFormsModule, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.models';
import { PersonService } from 'src/app/services/person.service';
import { Token } from 'src/app/models/token.models';
import { Person } from 'src/app/models/person.models';
import { GoogleLoginProvider, SocialUser } from "angularx-social-login";
import { SocialAuthService } from "angularx-social-login"; 

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({
    username: new FormControl('****', [Validators.required, Validators.minLength(4)]),
    password: new FormControl('****', [Validators.required, Validators.minLength(4)])
  });
  constructor(private personService: PersonService, 
              private router: Router, 
              private toastr: ToastrService,
              private socialAuthService: SocialAuthService) { }

  ngOnInit(): void {
    this.person.firstName = '';
    if(localStorage.getItem('token') != null)
      this.router.navigateByUrl('');
  }

  onSubmit() {
    let user: User = new User();
    user.username = this.loginForm.controls['username'].value;
    user.password = this.loginForm.controls['password'].value;
    this.personService.login(user).subscribe(
      (data: Token) => {
        localStorage.setItem('token', data.token);
        localStorage.setItem('personId', data.idPerson.toString());
        localStorage.setItem('role', data.role);
        localStorage.setItem('activate', data.activate);
        this.router.navigateByUrl('/home');
      },
      error => {
        this.toastr.error(error.error.message, 'Authentication failed.');
    }
    )
  }

  person: Person = new Person();
  loginWithGoogle(): void {
    this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID)
      .then(() => {this.socialAuthService.authState.subscribe((user : SocialUser) => {
        this.person.firstName = user.firstName.toString(); 
        this.person.lastName = user.lastName; 
        this.person.email = user.email; 
        this.person.imageUrl = user.photoUrl; 
        this.person.userName = user.name; 
        this.person.birth = new Date().toISOString();
        this.personService.loginGoogle(this.person).subscribe(
          (data : Token) => { 
            if(data == null)
            {
              alert('Greska u google autentifikaciji');
            }
            else
            {
              localStorage.setItem('token', data.token);
              localStorage.setItem('personId', data.idPerson.toString());
              localStorage.setItem('role', data.role);
              localStorage.setItem('activate', data.activate);
              this.router.navigateByUrl('/home');
            }
          },
          error => {
            alert('Server error');
          }
        );
      });
    });
  }

  get username() {
    return this.loginForm.get('username');
  }
  
  get password() {
    return this.loginForm.get('password');
  }

}
