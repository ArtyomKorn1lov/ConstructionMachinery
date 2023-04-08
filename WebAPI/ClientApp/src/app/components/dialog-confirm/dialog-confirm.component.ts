import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-dialog-confirm',
  templateUrl: './dialog-confirm.component.html',
  styleUrls: ['./dialog-confirm.component.scss']
})
export class DialogConfirmComponent implements OnInit {

  public message: string = "";

  constructor(private dialogRef: MatDialogRef<DialogConfirmComponent>, @Inject(MAT_DIALOG_DATA) public data: any) { }

  public close(): void {
    this.dialogRef.close({flag: false});
  }

  public confirm(): void {
    this.dialogRef.close({flag: true});
  }

  public refuse(): void {
    this.dialogRef.close({flag: false});
  }

  public ngOnInit(): void {
    this.message = this.data.message;
  }

}
