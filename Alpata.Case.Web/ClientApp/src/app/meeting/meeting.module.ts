import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MeetingListComponent } from './meeting-list/meeting-list.component';
import { MeetingRoutingModule } from './meeting-routing.module';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { CalendarModule } from 'primeng/calendar';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import { ToastModule } from 'primeng/toast';
import { FileUploadModule } from 'primeng/fileupload';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ToolbarModule } from 'primeng/toolbar';
import { MeetingCreateComponent } from './meeting-create/meeting-create.component';
import { StepsModule } from 'primeng/steps';
import { ConfirmDialogModule } from 'primeng/confirmdialog';



@NgModule({
  declarations: [
    MeetingListComponent,
    MeetingCreateComponent
  ],
  imports: [
    CommonModule,
    MeetingRoutingModule,
    FormsModule,
    TableModule,
    CalendarModule,
    InputTextModule,
    DialogModule,
    ToastModule,
    FileUploadModule,
    ToolbarModule,
    StepsModule,
    ConfirmDialogModule
  ],
  providers: [
    MessageService,
    ConfirmationService
  ]
})
export class MeetingModule { }
