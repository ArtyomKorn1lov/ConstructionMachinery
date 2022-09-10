import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { RequestService } from 'src/app/services/request.service';
import { AvailabilityRequestModelForCustomer } from 'src/app/models/AvailabilityRequestModelForCustomer';
import { Router } from '@angular/router';
import { ImageModel } from 'src/app/models/ImageModel';

@Component({
  selector: 'app-my-request-info',
  templateUrl: './my-request-info.component.html',
  styleUrls: ['./my-request-info.component.scss']
})
export class MyRequestInfoComponent implements OnInit {

  public request: AvailabilityRequestModelForCustomer = new AvailabilityRequestModelForCustomer(0, "", "", "", "", 0, 0, [ new ImageModel(0, "", "", 0) ], []);
  public date: Date = new Date();
  private targetRoute: string = "/my-requests"

  constructor(private accountService: AccountService, private requestService: RequestService, private router: Router) { }

  public cancel(): void {
    this.requestService.Remove(this.request.id).subscribe(data => {
      if (data == "success") {
        console.log(data);
        alert(data);
        this.router.navigateByUrl(this.targetRoute);
        return;
      }
      alert("Ошибка отмены заявки");
      console.log(data);
      return;
    });
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetAuthoriseModel();
    await this.requestService.GetForCustomer(this.requestService.GetIdFromLocalStorage()).subscribe(data => {
      this.request = data;
      this.date = new Date(this.request.availableTimeModels[0].date);
    });
  }

}
