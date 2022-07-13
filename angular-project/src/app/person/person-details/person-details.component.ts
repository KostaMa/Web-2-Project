import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ActivatedRoute, Router, TitleStrategy } from '@angular/router';
import { map, never, Subscription, switchMap, take } from 'rxjs';
import { Person } from 'src/app/models/person.models';
import { PersonService } from 'src/app/services/person.service';

@Component({
  selector: 'app-person-details',
  templateUrl: './person-details.component.html',
  styleUrls: ['./person-details.component.css']
})
export class PersonDetailsComponent implements OnInit, OnDestroy {
  isLogged: boolean = false;
  person: Person;
  activeSubscriptions: Subscription[];
  file: File | null;

  personForm = new FormGroup({
    username: new FormControl('', Validators.required),
    firstname: new FormControl('', Validators.required),
    lastname: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.email, Validators.required]),
    address: new FormControl('', Validators.required)
  });

  constructor(
    private route: ActivatedRoute,
    private personService: PersonService,
    private sanitizer: DomSanitizer
    ) { 
    this.activeSubscriptions = [];
  }

  ngOnDestroy(): void {
    this.activeSubscriptions.forEach((sub: Subscription) => {
      sub.unsubscribe();
    });
  }

  ngOnInit(): void {
    this.loadPerson();
  }

  private loadPerson() {
    const id = this.route.snapshot.paramMap.get('id');
    const getPersonSub = this.personService.getPerson(Number(id)).subscribe(result => {
      this.person = result;
      this.isLogged = true;
      this.personForm.controls['firstname'].setValue(this.person.firstName);
      this.personForm.controls['lastname'].setValue(this.person.lastName);
      this.personForm.controls['username'].setValue(this.person.userName);
      this.personForm.controls['address'].setValue(this.person.address);
      this.personForm.controls['email'].setValue(this.person.email);
      this.AddImage();
    });
    this.activeSubscriptions.push(getPersonSub);
  }

  onSaveChange(data) {
    if (data.valid) {
      window.alert('Not valid!');
      return;
    }
    this.personService
      .updatePerson(localStorage.getItem('personId'), data)
      .subscribe((person: Person) => {
        window.alert('Successfully updated a person!');
        this.loadPerson();
      });
  }

  onFileChange(event:Event):void
  {  
    const input = event.target as HTMLInputElement;

    if (!input.files?.length) {
        return;
    }

    const file = input.files[0]; 
    const formData = new FormData();
    formData.append('file', file, file.name);

    this.personService.uploadImage(this.person.id, formData).subscribe(
      (data : Object) => { 
          //this.toastr.success("Profile image uploaded!");
          this.AddImage();
    },
    (error: any) => { } 
    ); 
  }
  
  image:Blob=new Blob();
  imageUrl: SafeUrl ="https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460__340.png";
  AddImage() {
    this.personService.getImage(Number(localStorage.getItem('personId'))).subscribe(
      (response: any)=>
      {
        this.image = response;
        this.imageUrl = this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(this.image));
      });
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

  get address() {
    return this.personForm.get('address');
  }

  get email() {
    return this.personForm.get('email');
  }

}
