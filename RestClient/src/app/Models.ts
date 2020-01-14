import { Optional } from '@angular/core';

export class Fatura{

    constructor(@Optional() public FaturaID , public Musteri:string,@Optional() public Tutar: number,public  FaturaDetays: FaturaDetay[]){
    }
}

export class FaturaDetay{

    constructor(@Optional() public FaturaDetayID, public  FaturaID, public Aciklama:string, public BirimFiyat:number,public Miktar:number, @Optional() public ToplamTutar: number){
        ToplamTutar = BirimFiyat * Miktar;
    }
}