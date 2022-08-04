import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(), // for root means it needs to intialise some services
    ToastrModule.forRoot({ positionClass: 'toast-bottom-right' }),
  ],
  exports: [BsDropdownModule, ToastrModule],
})
export class SharedModule {}
