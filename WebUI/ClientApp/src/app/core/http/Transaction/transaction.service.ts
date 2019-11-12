import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class TransactionService {
    constructor(private baseService: BaseService) { }
    uploadFile(formData: any) {
        const uploadReq = new HttpRequest('POST', 'api/upload', formData, {
            reportProgress: true,
        });
        // return this.http.get(this.url);
        return this.baseService.http.request(uploadReq);
    }


    GetAll(): Observable<any> {
        return this.baseService.http.get('api/transaction');
    }

    getTransactions(filter: any): Observable<any> {
        return this.baseService.http.get('/api/transaction/search' + '?' + this.queryString(filter));
    }

    queryString(obj: any) {
        let parts = [];
        for (let property in obj) {
            let value = obj[property];
            if (value != null && value != undefined) {
                parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
            }
        }
        return parts.join('&');
    }
}
