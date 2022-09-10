import { Component, Input, OnInit } from '@angular/core';
import { AvailabilityRequestModel } from 'src/app/models/AvailabilityRequestModel';
import { RequestService } from 'src/app/services/request.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.scss']
})
export class RequestComponent implements OnInit {

  @Input() page: string | undefined;

  public requests: AvailabilityRequestModel[] = [];
  private confirmInfoRoute = "confirm-list/info";
  private requestInfoRoute = "my-requests/info";

  constructor(private requestService: RequestService, private router: Router) { }

  public navigateToInfo(id: number): void {
    this.requestService.SetIdInLocalStorage(id);
    if (this.page == 'in')
    {
      this.router.navigateByUrl(this.requestInfoRoute);
    }
    if (this.page == 'out')
    {
      this.router.navigateByUrl(this.confirmInfoRoute);
    }
  }

  public ngOnInit(): void {
    this.requestService.ClearLocalStorage();
    if (this.page == 'in')
      this.requestService.GetListForCustomer().subscribe(data => {
        this.requests = data;
      });
    if (this.page == 'out')
      this.requestService.GetListForLandlord().subscribe(data => {
        this.requests = data;
      });
  }

}
