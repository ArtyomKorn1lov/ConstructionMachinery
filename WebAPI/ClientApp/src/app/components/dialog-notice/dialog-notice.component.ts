import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-dialog-notice',
  templateUrl: './dialog-notice.component.html',
  styleUrls: ['./dialog-notice.component.scss']
})
export class DialogNoticeComponent implements OnInit {

  public message: string = "";

  constructor(private dialogRef: MatDialogRef<DialogNoticeComponent>, @Inject(MAT_DIALOG_DATA) public data: any) { }

  public close(): void {
    this.dialogRef.close({flag: false});
  }

  public ngOnInit(): void {
    this.message = this.data.message;
  }

}
