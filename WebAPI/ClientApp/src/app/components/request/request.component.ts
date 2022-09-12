import { Component, Input, OnInit } from '@angular/core';
import { AvailabilityRequestModel } from 'src/app/models/AvailabilityRequestModel';
import { RequestService } from 'src/app/services/request.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.scss']
})
export class RequestComponent implements OnInit {

  @Input() page: string | undefined;
  public requests: AvailabilityRequestModel[] = [];
  private confirmInfoRoute = "advert-confirm/confirm-list/info";
  private requestInfoRoute = "advert-request/my-requests/info";

  constructor(private requestService: RequestService, private router: Router, private route: ActivatedRoute) { }

  public navigateToInfo(id: number): void {
    this.requestService.setIdInLocalStorage(id);
    if (this.page == 'in')
      this.router.navigateByUrl(this.requestInfoRoute);
    if (this.page == 'out')
      this.router.navigateByUrl(this.confirmInfoRoute);
  }

  public async ngOnInit(): Promise<void> {
    this.requestService.clearIdLocalStorage();
    if (this.page == 'in')
      await this.requestService.getListForCustomer(this.requestService.getAdvertIdInLocalStorage()).subscribe(data => {
        this.requests = data;
      });
    if (this.page == 'out')
      await this.requestService.getListForLandlord(this.requestService.getAdvertIdInLocalStorage()).subscribe(data => {
        this.requests = data;
      });
  }
}
