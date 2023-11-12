import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Meeting } from '../model/meeting';

@Injectable({
  providedIn: 'root'
})
export class MeetingService  {

  apiEndpoint: string;
  constructor(protected http: HttpClient) {
    this.apiEndpoint = environment.apiEndPoint;

  }
  getAll() {
    return this.http.get<Array<Meeting>>(`${this.apiEndpoint}/api/Meetings`);
  }
  create(model:Meeting){
    return this.http.post<Meeting>(`${this.apiEndpoint}/api/Meetings`, model);
  }
  update(model:Meeting){
    return this.http.put(`${this.apiEndpoint}/api/Meetings`, model);
  }
  delete(id: string) {
    return this.http.delete(`${this.apiEndpoint}/api/Meetings/${id}`);
  }
  get(id: string) {
    return this.http.get<Meeting>(`${this.apiEndpoint}/api/Meetings/${id}`);
  }
}
