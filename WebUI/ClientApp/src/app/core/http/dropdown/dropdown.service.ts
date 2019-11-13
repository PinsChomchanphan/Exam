import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class DropdownService {
    constructor(private baseService: BaseService) { }

    getCurrencyCodes(): Observable<any> {
        return this.baseService.http.get('api/dropdown/currencycode');
    }

}
