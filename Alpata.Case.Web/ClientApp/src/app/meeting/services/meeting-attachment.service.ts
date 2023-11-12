import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Meeting } from '../model/meeting';
import { Guid } from 'guid-typescript';
import { MeetingAttachment } from '../model/meetingAttachment';

@Injectable({
  providedIn: 'root'
})
export class MeetingAttachmentService  {

  apiEndpoint: string;
  constructor(protected http: HttpClient) {
    this.apiEndpoint = environment.apiEndPoint;

  }
  upload(formData:FormData){
    return this.http.post(`${this.apiEndpoint}/api/MeetingAttachments/Upload`,formData);
  }

  deleteAttachment(id:string){
    return this.http.delete(`${this.apiEndpoint}/api/MeetingAttachments/${id}`);

  }

  getAttachmentsByMeetingId(meetingId:string){
    return this.http.get<MeetingAttachment[]>(`${this.apiEndpoint}/api/MeetingAttachments/ByMeetingId/${meetingId}`);

  }

}
