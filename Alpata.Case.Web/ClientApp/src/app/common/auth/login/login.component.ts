import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { UserLogin } from '../model/userLogin';
import { LoginService } from '../services/login.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styles: [`
        i {
            opacity: 0.6;
            transition-duration: .12s;
            color: #2196F3;
            
            &:hover {
                opacity: 1;
            }
        }
    `]
})

export class LoginComponent {
    
    model: UserLogin = new UserLogin();
    buttonDisabled : boolean = false;
    constructor(private loginService: LoginService, private router:Router, private messageService:MessageService) {

    }
    authenticate() {
        this.buttonDisabled = true;
        this.loginService.authenticate(this.model).subscribe({
            next: (data) => {
                    localStorage.setItem("accessToken",data.accessToken);
                    this.router.navigate(["/"])
            },
            error: (err) => {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error.error.message });
                this.buttonDisabled=false;
            }
        });
    }
}
