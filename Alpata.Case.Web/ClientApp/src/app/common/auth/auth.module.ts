import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthRoutingModule } from './auth-routing.module';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { AppConfigModule } from 'src/app/layout/config/app.config.module';
import { LoginComponent } from './login/login.component';
import { ToastModule } from 'primeng/toast';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import { InputMaskModule } from 'primeng/inputmask';
import { MessageService } from 'primeng/api';
import { RegisterComponent } from './register/register.component';
import { DialogModule } from 'primeng/dialog';
import { FileUploadModule } from 'primeng/fileupload';


@NgModule({
    declarations: [
        LoginComponent,
        RegisterComponent
    ],
    imports: [
        CommonModule,
        AuthRoutingModule,
        InputTextModule,
        PasswordModule,
        ButtonModule,
        AppConfigModule,
        FormsModule,
        ToastModule,
        ProgressSpinnerModule,
        InputMaskModule,
        DialogModule,
        FileUploadModule
    ],
    providers:[
        MessageService
    ]
})
export class AuthModule { }
