import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Person } from '../models/person.models';

import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { User } from '../models/user.models';
import { Token } from '../models/token.models';
import { AcceptOrder } from '../models/acceptOrder.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  private persons: Observable<Person[]>;

  constructor(private http: HttpClient, router: Router) { 
    this.refreshPersons();
  }

  private refreshPersons(): Observable<Person[]> {
    this.persons = this.http
      .get<Person[]>(environment.personServiceUrl + '/api/person/');

    return this.persons;
  }

  public getPersons() {
    return this.persons;
  }

  login(login:User) :Observable<Token> {
    return this.http.post<Token>(environment.personServiceUrl + '/api/account/login', login);
  }

  registerCustomer(data): Observable<any> {
    return this.http
      .post<any>(environment.personServiceUrl + '/api/account/register-customer', data);
  }

  registerDeliverer(data): Observable<any> {
    return this.http
      .post<any>(environment.personServiceUrl + '/api/account/register-deliverer', data);
  }

  public getPerson(id: number): Observable<Person> {
    return this.http
      .get<Person>(environment.personServiceUrl + '/api/person/' + id);
  }

  public getDeliverers(): Observable<Person[]> {
    return this.http
      .get<Person[]>(environment.personServiceUrl + '/api/person/deliverers');
  }

  public updatePerson(idPerson: string | null, data) {
    return this.http
      .put<Person>(environment.personServiceUrl + '/api/person/' + idPerson, data)
  }

  public verification(data) {
    if(data.isAccep) {
      return this.http
      .post<AcceptOrder>(environment.personServiceUrl + '/api/admin/accept', data);
    } else {
      return this.http
      .post<AcceptOrder>(environment.personServiceUrl + '/api/admin/denied', data);
    }
  }

  getImage(idPerson: number): Observable<Blob> {
    return this.http
      .get(environment.personServiceUrl + '/api/person/' + idPerson + '/image', { responseType: 'blob' });
  }

  uploadImage(idPerson: number, image: any): Observable<any>{
    return this.http
      .post(environment.personServiceUrl + '/api/person/' + idPerson + '/image', image);
  }

  loginGoogle(person: Person): Observable<Token> {
    return this.http
      .post<Token>(environment.personServiceUrl + '/api/account/login-google', person);
  }

}
