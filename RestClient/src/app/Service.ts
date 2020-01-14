import { Component } from '@angular/core';
import { Fatura } from './Models';
import { FaturaDetay } from './Models';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export class Service{
    private baseUrl: string = "https://localhost:44307/api/fatura/";
    constructor(private http: HttpClient){    }
    faturas: Fatura[] = [];

    getFaturas(): Observable<Fatura[]>{
        return this.http.get<Fatura[]>(this.baseUrl);
    }

    getFaturaDetail(FaturaID): Observable<Fatura>{
        return this.http.get<Fatura>(this.baseUrl + FaturaID);
    }

    saveFatura(fatura:Fatura): Observable<Fatura>{
        return this.http.post<Fatura>(this.baseUrl,fatura);
    }

    updateFatura(fatura:Fatura): Observable<Fatura>{
        return this.http.post<Fatura>(this.baseUrl+ fatura.FaturaID,fatura);
    }

    deleteFatura(FaturaID){
        return this.http.delete(this.baseUrl + FaturaID);
    }

    
}