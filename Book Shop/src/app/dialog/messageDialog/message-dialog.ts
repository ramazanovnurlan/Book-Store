import { Component, Inject } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface DialogData {
    Text: string,
    obj_message: any,
    isRefresh: boolean
}

@Component({
    templateUrl: './message-dialog.html'
})
export class MessageDialog {
    constructor(@Inject(MAT_DIALOG_DATA) public data: DialogData, private dialog: MatDialog) {
     }

     getData(data: string){
        return data.split('&');
     }

     reload(){
         window.location.reload();
     }
     
     isRefresh(value: boolean)
     {
         if(value)
         {
             window.location.reload();
         }
     }
}