import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { apiUrls } from '../http/urls';


@Injectable({
  providedIn: 'root'
})
export class WeatherHttpService {

  constructor(private http: HttpClient) { }

  get(): Observable<WeatherResponse[]>{
        return this.http.get<WeatherResponse[]>(apiUrls.weather.get)
  }
}

export interface WeatherResponse {
     date: Date;
     temperatureC:Number;
     temperatureF:Number;
     summary: String;
}