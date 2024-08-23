import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Member } from '../_models/member';


@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http=inject(HttpClient);
 
  baseUrl=environment;
  getMembers()
  {
    return this.http.get<Member[]>(this.baseUrl.apiUrl+'users');
  }
  getMember(username: string)
  {
    return this.http.get<Member>(this.baseUrl.apiUrl+'users/'+username);
  }

 
}
