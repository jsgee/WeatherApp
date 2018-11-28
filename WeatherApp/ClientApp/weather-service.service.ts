
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

export interface Day {
    value: string;
}

@Injectable()
export class WeatherService {
    private headers: HttpHeaders;
    private accessPointUrl: string = 'http://localhost:8841/api/data';
    constructor(private http: HttpClient) {
        this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    }

    public get() {
        // Get all jogging data
        return this.http.get(this.accessPointUrl, { headers: this.headers });
    }
}
