import { Component, OnInit } from '@angular/core';
import { Person } from 'src/app/models/person.models';
import { Verification } from 'src/app/models/verification.model';
import { PersonService } from 'src/app/services/person.service';

@Component({
  selector: 'app-person-verification',
  templateUrl: './person-verification.component.html',
  styleUrls: ['./person-verification.component.css']
})
export class PersonVerificationComponent implements OnInit {
  persons: Person[] = [];

  constructor(private personService: PersonService) { }

  ngOnInit(): void {
    this.loadPersons();
  }

  private loadPersons() {
    this.personService.getDeliverers()
      .subscribe(persons => {
        this.persons = persons;
      });
  }

  onDenied(id: number) {
    let denied = new Verification(id, false);
    this.personService.verification(denied)
    .subscribe(() => {
      this.loadPersons();
    })
  }

  onAccept(id: number) {
    let accept = new Verification(id, true);
    this.personService.verification(accept)
      .subscribe(() => {
        this.loadPersons();
      })
  }

}
