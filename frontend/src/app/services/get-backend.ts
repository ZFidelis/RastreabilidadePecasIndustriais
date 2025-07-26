import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GetBackend {

  pegaEvento(info: string){
    console.log(info)
  }

}
