import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

//Services
import { BaseService } from '../core/http/base.service'

import { HeaderComponent } from "./header/header.component";
import { FooterComponent } from './footer/footer.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
    declarations: [HeaderComponent, FooterComponent],
    imports: [
        CommonModule,
        NgbModule
    ],
    exports: [CommonModule, HeaderComponent, FooterComponent, NgbModule ],
    providers: [BaseService]
})
export class CoreModule { }
