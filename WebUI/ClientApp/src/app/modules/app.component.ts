import { Component, OnInit } from '@angular/core';
import { TransactionService } from '../core/http/Transaction/transaction.service';
import { HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SearchTransaction } from '../shared/models/Transaction/search-transaction';
import { DropdownService } from '../core/http/dropdown/dropdown.service';
import { NgbDateAdapter, NgbDateStruct, NgbDateNativeAdapter } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    providers: [{ provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }]
})


export class AppComponent implements OnInit {
    title = '2C2P Exam';
    public progress: number;
    public message: string;
    public isError: boolean;
    appLoader = true;
    public list: any[];
    public searchTransaction: SearchTransaction = new SearchTransaction();
    public transactions: Observable<any[]>
    public currencyCodes: Observable<any[]>


    constructor(private tranService: TransactionService, private ddService : DropdownService) {
        this.isError = false;
        this.getAll();
    }

    ngOnInit() {

        this.transactions = this.tranService.GetAll();
        this.currencyCodes = this.ddService.getCurrencyCodes();
        this.searchTransaction.status = "";
        this.searchTransaction.currencyCode = "";
    }

    public getAll() {
        this.tranService.GetAll().subscribe(result => {
            this.list = result;
        }, error => console.error(error));
    }
    public search() {
        this.tranService.getTransactions(this.searchTransaction).subscribe(result => {
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
            formData.append('file', file);
        }
        if (f.size > (1 * 1024 * 1024)) {
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
                this.getAll();
            }

        }, (error: HttpErrorResponse) => {
                this.message = error.error;
                this.isError = true;
        });
    }
}







