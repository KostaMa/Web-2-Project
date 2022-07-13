import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Person } from '../models/person.models';
import { PersonService } from '../services/person.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  person: Person;

  constructor(private router: Router, private personService:PersonService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadPerson();
  }

  loadPerson() {
    this.personService.getPerson(Number(localStorage.getItem('personId')))
      .subscribe((result) => {
        this.person = result;
        if(this.person.verification === 'Denied' || this.person.verification === 'OnHold') {
          this.router.navigateByUrl('');
          this.toastr.error('Your account is not verified.', 'Login failed.');
        }
    });
  }

}
