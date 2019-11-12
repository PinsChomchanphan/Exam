import { Data } from "@angular/router";

export interface TransactionModel {
    id: number;
    transactionId: string;
    payment: string;
    status: string;
    transactionDate: Data;
}
