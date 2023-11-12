import { Component, OnInit } from '@angular/core';
import { MeetingService } from '../services/meeting.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Meeting } from '../model/meeting';
import { Table } from 'primeng/table';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-meeting-list',
  templateUrl: './meeting-list.component.html',
  styleUrls: ['./meeting-list.component.scss']
})
export class MeetingListComponent implements OnInit {
  progressVisible: boolean = true;
  newMeetingDialogVisible: boolean = false;
  meetings: Meeting[] = [];
  meeting: Meeting =new Meeting(Guid.createEmpty().toString());
  searchText: string = "";
  ngOnInit(): void {
    this.getAll();
  }
  clearFilter(table: Table) {
    table.clear();
  }
  constructor(private meetingService: MeetingService, private messageService: MessageService, private confirmationService:ConfirmationService) {
  }
  getAll() {
    this.progressVisible = true;
    this.meetingService.getAll().subscribe({
      next: (data) => {
        this.meetings = data;
        this.progressVisible = false;
      },
      error: (error) => {
        this.progressVisible = false;
      }
    })
  }
  editMeeting(item: Meeting) {
    this.meetingService.get(item.id).subscribe({
      next: (data) => {
        data.startTime = new Date(data.startTime);
        data.endTime = new Date(data.endTime);
        this.meeting = data;
        this.newMeetingDialogVisible = true;
      },
      error: (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'An Error Occured' });
      }
    })
  }
  deleteMeeting(id: string) {
    this.meetingService.delete(id).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Meeting was deleted successfuly' });
        this.getAll();
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
        this.deleteMeeting(item.id);
      },
      reject: () => {
        //reject action
      }
    });
  }
  openNew() {
    this.meeting = new Meeting(Guid.createEmpty().toString());
    this.newMeetingDialogVisible = true;
  }
  refresh(data: Meeting) {
    this.getAll();
  }
}

