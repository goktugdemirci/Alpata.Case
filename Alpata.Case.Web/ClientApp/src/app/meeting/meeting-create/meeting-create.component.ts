import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Meeting } from '../model/meeting';
import { Guid } from 'guid-typescript';
import { MeetingService } from '../services/meeting.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { FileUpload } from 'primeng/fileupload';
import { MeetingAttachmentService } from '../services/meeting-attachment.service';

@Component({
  selector: 'app-meeting-create',
  templateUrl: './meeting-create.component.html',
  styleUrls: ['./meeting-create.component.scss']
})
export class MeetingCreateComponent implements OnInit {
  @Input() newMeetingDialogVisible: boolean = false;
  @Input() model: Meeting = new Meeting(Guid.createEmpty().toString());
  @Output() onDialogClose = new EventEmitter();
  @Output() onSave = new EventEmitter();
  @ViewChild("fileUpload") fileUploader!: FileUpload;
  constructor(private meetingService: MeetingService, private messageService: MessageService, private meetingAttachmentService: MeetingAttachmentService, private confirmationService: ConfirmationService) {

  }
  ngOnInit(): void {
    this.getAttachments();
  }
  upload() {
    var formData = new FormData();
    formData.append("meetingId", this.model.id);

    for (let index = 0; index < this.fileUploader._files.length; index++) {

      formData.append("file", this.fileUploader._files[index]);
    }
    this.meetingAttachmentService.upload(formData).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Attachments were uploaded successfuly' });
      },
      error: (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'An Error Occured' });
      }
    });
  }
  save() {
    if (this.model.id == Guid.createEmpty().toString()) {
      this.meetingService.create(this.model).subscribe({
        next: (data) => {
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Meeting was created successfuly' });
          this.model = data;
          if(this.fileUploader._files.length>0){
            this.upload();
          }
          this.onSave.emit(data);
          this.newMeetingDialogVisible = false;
        },
        error: (e) => {
          console.log(e);
          this.messageService.add({ severity: 'error', summary: 'Error', sticky: true, detail: `${e.error.error.message} An Error Occured.` });
        }
      })
    }
    else {
      this.meetingService.update(this.model).subscribe({
        next: (data) => {
          if(this.fileUploader._files.length>0){
            this.upload();
          }
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Meeting was updated successfuly' });
          this.newMeetingDialogVisible = false;
          this.onSave.emit(data);
        },
        error: (err) => {
          console.log(err);
          this.messageService.add({ severity: 'error', summary: 'Error', sticky: true, detail: `${err.error.error.message} An Error Occured.` });
        }
      })
    }

  }
  close() {
    this.onDialogClose.emit();
  }
  getAttachments(){
    this.meetingAttachmentService.getAttachmentsByMeetingId(this.model.id).subscribe({
      next: (data) => {
        this.model.attachments=data;

      },
      error: (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'An Error Occured' });
      }
    });
  }
  deleteAttachment(id: string) {
    this.meetingAttachmentService.deleteAttachment(id).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Attachment was deleted successfuly' });
        this.getAttachments();
      },
      error: (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'An Error Occured' });
      }
    });
  }
  confirm(event: Event, item: Meeting) {
    this.confirmationService.confirm({
      target: event.target ?? new EventTarget(),
      message: 'Are you sure that you want to delete meeting?',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.deleteAttachment(item.id);
      },
      reject: () => {
        //reject action
      }
    });
  }

}
