import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Guid } from 'guid-typescript';
import { User } from '../model/user';
import { RegisterService } from '../services/register.service';
import { MessageService } from 'primeng/api';
import { FileUpload } from 'primeng/fileupload';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {
model: User = new User(Guid.createEmpty().toString());
  isEmailValid: boolean = true;
  isPhoneNumberValid: boolean = true;
  @ViewChild("profilePictureUpload") profilePictureUpload!: FileUpload
  constructor(private registerService: RegisterService, private messageService: MessageService, private router:Router) {

  }
  ngOnInit(): void {
  }

  upload() {
    var formData = new FormData();
    formData.append("userId", this.model.id);
    formData.append("file", this.profilePictureUpload._files[0]);
    this.registerService.upload(formData).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Profile Picture was uploaded successfuly' });

      },
      error: (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'An Error Occured' });
      }
    });
  }
  save() {

    this.registerService.create(this.model).subscribe({
      next: (data) => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'User was created successfuly' });
        this.model = data;

      if(this.profilePictureUpload._files.length>0){
        this.upload();
      }
        setTimeout(()=> {
          this.router.navigate(["/auth/login"])
        },3000)
      },
      error: (e) => {
        this.messageService.add({ severity: 'error', summary: 'Error', sticky: true, detail: `${e.error.error?.message} An Error Occured.` });
      }
    })

  }

  isUniqueEmail(email: string) {
    this.registerService.isUniqueEmail(email).subscribe({
      next: () => {
        this.isEmailValid = true;
      },
      error: (error) => {
        this.isEmailValid = false;
      }
    });
  }

  isUniquePhoneNumber(phoneNumber: string) {
    this.registerService.isUniquePhoneNumber(phoneNumber).subscribe({
      next: () => {
        this.isPhoneNumberValid = true;
      },
      error: (error) => {
        this.isPhoneNumberValid = false;
      }
    });
  }
}
