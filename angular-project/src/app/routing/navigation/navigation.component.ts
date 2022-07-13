import { Component, OnInit } from '@angular/core';
import { PersonService } from 'src/app/services/person.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  public isLogged: boolean = false;
  public personId: string | null;
  role: string | null;

  constructor(private personService: PersonService) { 
    this.checkToken();
  }

  ngOnInit(): void {
    if(localStorage.getItem('token') !== null)
    {
      this.isLogged = true;
      this.role = localStorage.getItem('role');
    }
  }
  
  logOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('personId');
    localStorage.removeItem('role');
  }

  checkToken() {
    let data = localStorage.getItem('personId');
    this.personId = data;
  }

}
