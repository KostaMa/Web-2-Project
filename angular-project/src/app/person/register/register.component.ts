import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PersonService } from 'src/app/services/person.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public personForm: FormGroup;

  constructor(private personService: PersonService,
              private formBuilder: FormBuilder,
              private toastr: ToastrService,
              private router: Router
    ) { 
    this.personForm = this.formBuilder.group({
      username: ['Enter username', [Validators.required, Validators.minLength(4)]],
      firstname: ['Enter first name', [Validators.required]],
      lastname: ['Enter last name', Validators.required],
      password: ['********', [Validators.required, Validators.minLength(4)]],
      passwordCheck: ['********', [Validators.required, Validators.minLength(4)]],
      address: ['Enter address', Validators.required],
      birth: ['01-01-0001', Validators.required],
      email: ['email@gmail.com', [Validators.required, Validators.email]],
      option: ['customer'],
    });
  }

  ngOnInit(): void {
  }

  onSubmit(data) {
    if(data.password !== data.passwordCheck) {
      this.personForm.controls['password'].reset();
      this.personForm.controls['passwordCheck'].reset();
      this.toastr.error('Password are not the same.', 'Password check faild.');
    }
    
    if(data.option === 'deliverer') {
      this.personService.registerDeliverer(data)
        .subscribe(() => {
          this.personForm.reset();
          this.router.navigateByUrl('');
          this.toastr.success('You are successfully registred. ', 'Registration success.');
        });
    } else if(data.option === 'customer') {
      this.personService.registerCustomer(data)
        .subscribe(() => {
          this.personForm.reset();
          this.router.navigateByUrl('');
          this.toastr.success('You are successfully registred.', 'Registration success.');
        });
    } else {
      this.toastr.error('Something went wrong.', 'Registration failed.');
    }

  }

  get firstname() {
    return this.personForm.get('firstname');
  }

  get lastname() {
    return this.personForm.get('lastname');
  }

  get username() {
    return this.personForm.get('username');
  }

  get email() {
    return this.personForm.get('email');
  }

  get birth() {
    return this.personForm.get('birth');
  }

  get address() {
    return this.personForm.get('address');
  }

  get passwordCheck() {
    return this.personForm.get('passwordCheck');
  }

  get password() {
    return this.personForm.get('password');
  }


}
