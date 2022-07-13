import { Component, OnInit } from '@angular/core';
import { Person } from 'src/app/models/person.models';
import { PersonService } from 'src/app/services/person.service';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.css']
})
export class PersonListComponent implements OnInit {
  public persons: Person[] = [];

  constructor(private personService: PersonService) { 
    this.personService.getPersons()
      .subscribe(persons => {
        this.persons = persons;
      })
  }

  ngOnInit(): void {
  }

}
