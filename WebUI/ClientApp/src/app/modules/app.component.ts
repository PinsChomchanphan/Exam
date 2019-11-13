import { Component, OnInit } from '@angular/core';
import { TransactionService } from '../core/http/Transaction/transaction.service';
import { HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SearchTransaction } from '../shared/models/Transaction/search-transaction';
import { DropdownService } from '../core/http/dropdown/dropdown.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})


export class AppComponent implements OnInit {
    title = '2C2P Exam'
    public progress: number;
    public message: string;
    public isError: boolean;
    appLoader = true;
    public list: any[];
    public searchTransaction: SearchTransaction = new SearchTransaction();
    public transactions: Observable<any[]>
    public currencyCodes: Observable<any[]>
    

    public datepickerOpts = {
        startDate: new Date(2016, 5, 10),
        autoclose: true,
        todayBtn: 'linked',
        todayHighlight: true,
        assumeNearbyYear: true,
        format: 'D, d MM yyyy'
    };
    

    constructor(private tranService: TransactionService, private ddService : DropdownService) {
        this.isError = false;
       //  this.getAll();
        this.searchTransaction.currencyCode = "USD";
        this.searchTransaction.status = "D";
        this.tranService.getTransactions(this.searchTransaction).subscribe(result => {
        }, error => console.error(error));
    }

    ngOnInit() {

        this.transactions = this.tranService.GetAll();
        this.currencyCodes = this.ddService.getCurrencyCodes();
    }

    public getAll() {
        this.tranService.GetAll().subscribe(result => {
            this.list = result;
        }, error => console.error(error));
    }


    upload(files) {

        if (files.length === 0)
            return;

        const formData = new FormData();
        let f;
        for (let file of files) {
            f = file;
            formData.append(file.name, file);
        }
        if (f.size > 1048576) {
            this.isError = true;
            this.message = 'File size Should Be UpTo 1MB';
            return;
        }
        if (f.type != 'text/xml' && f.type != 'application/vnd.ms-excel') {
            this.isError = true;
            this.message = 'Unknown format';
            return;
        }
        
        this.tranService.uploadFile(formData).subscribe(event => {
            if (event.type === HttpEventType.UploadProgress)
                this.progress = Math.round(100 * event.loaded / event.total);
            else if (event.type === HttpEventType.Response) {
                this.message = 'Success';
                this.isError = false;
            }
                
        }, (error: HttpErrorResponse) => {
                this.message = error.error;
                this.isError = true;
        });
    }
}







