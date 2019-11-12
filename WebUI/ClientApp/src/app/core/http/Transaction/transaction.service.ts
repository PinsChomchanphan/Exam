import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';



@Injectable({
    providedIn: 'root'
})
export class TransactionService {
    constructor(private BaseService: BaseService) { }
    uploadFile(formData: any) {
        const uploadReq = new HttpRequest('POST', 'api/upload', formData, {
            reportProgress: true,
        });
        // return this.http.get(this.url);  
        return this.BaseService.http.request(uploadReq);
    }


    GetAll(): Observable<any> {
       
        // return this.http.get(this.url);  
        return this.BaseService.http.get('api/transaction');
    }

}
