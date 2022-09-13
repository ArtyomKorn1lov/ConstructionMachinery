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
  public count: number = 10;
  public scrollFlag = true;
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

  public scrollEvent = async (event: any): Promise<void> => {
    if (event.target.scrollingElement.offsetHeight + event.target.scrollingElement.scrollTop >= event.target.scrollingElement.scrollHeight) {
      const length = this.requests.length;
      if (this.page == 'in')
        await this.requestService.getListForCustomer(this.requestService.getAdvertIdInLocalStorage(), this.count).subscribe(data => {
          this.requests = data;
          this.scrollFlag = this.requestService.checkLenght(length, this.requests.length);
          this.flagState();
        });
      if (this.page == 'out')
        await this.requestService.getListForLandlord(this.requestService.getAdvertIdInLocalStorage(), this.count).subscribe(data => {
          this.requests = data;
          this.scrollFlag = this.requestService.checkLenght(length, this.requests.length);
          this.flagState();
        });
      this.count++;
    }
  }

  public flagState(): void {
    if(this.scrollFlag == false) {
      this.count = 0;
      window.removeEventListener('scroll', this.scrollEvent, true);
    }
  }

  public async ngOnInit(): Promise<void> {
    window.addEventListener('scroll', this.scrollEvent, true);
    this.requestService.clearIdLocalStorage();
    if (this.page == 'in')
      await this.requestService.getListForCustomer(this.requestService.getAdvertIdInLocalStorage(), this.count).subscribe(data => {
        this.requests = data;
      });
    if (this.page == 'out')
      await this.requestService.getListForLandlord(this.requestService.getAdvertIdInLocalStorage(), this.count).subscribe(data => {
        this.requests = data;
      });
  }

  public ngOnDestroy(): void {
    window.removeEventListener('scroll', this.scrollEvent, true);
  }
}
