import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BaseService<T> {

  protected requestUrl:string="";
  constructor(public requestPath: String, public http: HttpClient) {
    this.requestUrl = `${environment.apiEndPoint}${requestPath}`;
  }

  create(model: T) {
    return this.http.post<T>(this.requestUrl, model);
  }

  update(model: T) {
    return this.http.put<T>(this.requestUrl, model);
  }

  getAll() {
    return this.http.get<Array<T>>(this.requestUrl);
  }

  get(id: string) {
    return this.http.get<T>(`${this.requestUrl}/${id}`);
  }

  delete(id: string) {
    return this.http.delete(`${this.requestUrl}/${id}`);
  }
}
