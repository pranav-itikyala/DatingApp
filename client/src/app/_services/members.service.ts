import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Member } from '../_models/member';
import { of, tap } from 'rxjs';
import { Photo } from '../_models/photo';


@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http=inject(HttpClient);
  baseUrl=environment;
  members=signal<Member[]>([]);
  getMembers()
  {
    return this.http.get<Member[]>(this.baseUrl.apiUrl+'users').subscribe(
      {
        next:members=>this.members.set(members)
      }
    );

  }
  getMember(username: string)
  {

    const member =this.members().find(x=> x.userName=username);
    if(member!== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl.apiUrl+'users/'+username);
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl.apiUrl + 'users', member).pipe(
      tap(()=>
      {
        this.members.update(members=> members.map(m => m.userName=== member.userName? member: m))
      })
    );
    
  }

  setMainPhoto(photo: Photo) {
    return this.http.put(this.baseUrl.apiUrl + 'users/set-main-photo/' + photo.id, {}).pipe(
      tap(()=>{
        this.members.update(members=> members.map(m=>{
          if(m.photos.includes(photo)){
            m.photoUrl=photo.url
          }
          return m;
        }))
      }

      )
    );
  }

  deletePhoto(photo: Photo) {
    return this.http.delete(this.baseUrl.apiUrl + 'users/delete-photo/' + photo.id).pipe(
      tap(()=>{
        this.members.update(members=> members.map(m=>{
          if(m.photos.includes(photo)){
            m.photos=m.photos.filter(x=>x.id!==photo.id)
          }
          return m;
        }))
      })
    );
  }


}
