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

  public request: AvailabilityRequestModelForCustomer = new AvailabilityRequestModelForCustomer(0, "", "", "", "", 0, 0, [new ImageModel(0, "", "", 0)], []);
  public date: Date = new Date();
  private targetRoute: string = "/advert-request/my-requests"

  constructor(private accountService: AccountService, private requestService: RequestService, private router: Router) { }

  public cancel(): void {
    this.requestService.remove(this.request.id).subscribe({
      next: async (data) => {
        console.log(data);
        alert(data);
        this.router.navigateByUrl(this.targetRoute);
        return;
      },
      error: (bad) => {
        alert("Ошибка отмены заявки");
        console.log(bad);
        return;
      }
    });
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    await this.requestService.getForCustomer(this.requestService.getIdFromLocalStorage()).subscribe(data => {
      this.request = data;
      this.date = new Date(this.request.availableTimeModels[0].date);
    });
  }

}
