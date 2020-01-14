import { Component } from '@angular/core';
import { Fatura } from './Models';
import { FaturaDetay } from './Models';
import { Service } from './Service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent { 
  faturas: Fatura[] = [];
  fatura: Fatura;
  showDetail:boolean = false;
  service: Service;

  constructor(private http:HttpClient){
    this.service = new Service(http);
    this.getFaturas();
  }

  getFaturas(){
    this.service.getFaturas().subscribe(
      res => {
        this.faturas = res;
      }
    );
  }

  getFaturaDetails(id){
    this.service.getFaturaDetail(id).subscribe(
      res => {
        this.fatura = res;
        this.showDetail = true;
      }
    );
  }

  onClick(clicked:Fatura){
    this.getFaturaDetails(clicked.FaturaID);
  }

  faturaSil(FaturaID){
    this.showDetail = false;
    this.service.deleteFatura(FaturaID).subscribe( data => this.getFaturas());
  }

  detaySil(fatura:Fatura,faturaDetay:FaturaDetay){
    let index = fatura.FaturaDetays.indexOf(faturaDetay);
    if(index > -1){
      fatura.FaturaDetays.splice(index, 1);
    }
    this.service.updateFatura(fatura).subscribe(res => {this.getFaturas();});
  }

  faturaEkle(musteriAdi:string){
    let fatura = new Fatura(0, musteriAdi, 0, []);
    this.service.saveFatura(fatura).subscribe(res => this.getFaturas());
  }

  faturaDetayEkle(fatura:Fatura, fdAciklama,fdMiktar,fdBf){
    let fd = new FaturaDetay(0,fatura.FaturaID,fdAciklama,fdBf,fdMiktar,0);
    fatura.FaturaDetays.push(fd);
    this.service.updateFatura(fatura).subscribe(res => {this.getFaturas();this.getFaturaDetails(fatura.FaturaID); });
  }
}
