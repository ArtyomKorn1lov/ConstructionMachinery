import { Component, ElementRef, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { AdvertComponent } from 'src/app/components/advert/advert.component';
import { Filter } from 'src/app/models/Filter';
import { Title } from '@angular/platform-browser';
import { AdvertService } from 'src/app/services/advert.service';
import { Observable, distinctUntilChanged, map, mergeMap } from 'rxjs';
import { AppComponent } from 'src/app/app.component';
import { eventSubscriber } from 'src/app/services/common-child.interface';
import { TechniqueTypeList } from 'src/app/models/TechniqueTypeList';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-advert-list',
  templateUrl: './advert-list.component.html',
  styleUrls: ['./advert-list.component.scss']
})
export class AdvertListComponent implements OnInit {

  public filters: Filter[] = [
    {
      name: 'Все объявления',
      param: 'all'
    },
    {
      name: 'По возрастанию цены',
      param: 'max_price',
    },
    {
      name: 'По убыванию цены',
      param: 'min_price',
    },
    {
      name: 'С самым высоким рейтингом',
      param: 'max_rating',
    },
    {
      name: 'С самым низким рейтингом',
      param: 'min_rating',
    },
    {
      name: 'Самые новые объявления',
      param: 'max_date',
    },
    {
      name: 'Самые старые объявления',
      param: 'min_date',
    }
  ];
  public techniqueObject: TechniqueTypeList = new TechniqueTypeList();;
  public filterState: string = "all";
  public menuFlag = false;
  public selectedKey = 0;
  public rangePublish = new FormGroup({
    startPublish: new FormControl<Date | null>(null),
    endPublish: new FormControl<Date | null>(null),
  });
  public messagePublish: string | undefined;
  public invalidPublish: boolean = false;
  public rangeDate = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });
  public messageDate: string | undefined;
  public invalidDate: boolean = false;
  public startTime: string | undefined;
  public endTime: string | undefined;
  public messageTime: string | undefined;
  public invalidTime: boolean = false;
  public startPrice: string | undefined;
  public endPrice: string | undefined;
  public messagePrice: string | undefined;
  public invalidPrice: boolean = false;
  public description: string | undefined;
  public filterFlag = false;
  @ViewChild(AdvertComponent) child: AdvertComponent | undefined;
  @ViewChildren("observeMenu") observeMenu!: QueryList<ElementRef>;
  private targetRoute = "/advert-list";

  constructor(private accountService: AccountService, public titleService: Title, public advertService: AdvertService,
    private appComponent: AppComponent, private router: Router, private route: ActivatedRoute, private formBuilder: FormBuilder) {
    this.titleService.setTitle("Список объявлений");
    this.findAdvert = this.findAdvert.bind(this);
    eventSubscriber(appComponent.executeAction, this.findAdvert);
  }

  public resetValidFlag(): boolean {
    return false;
  }

  public async selectEvent(): Promise<void> {
    await this.child?.sortByParam(this.filterState);
  }

  public async findAdvert(): Promise<void> {
    await this.child?.resetAllParams();
  }

  private validateFilter(): boolean {
    let valid = true;
    let toScroll = true;
    const oneDay = 1000 * 60 * 60 * 24;
    if (this.rangePublish.controls.startPublish.errors != null || this.rangePublish.controls.endPublish.errors != null) {
      this.messagePublish = "Неверный диапазон дат";
      this.invalidPublish = true;
      valid = false;
      if (toScroll) {
        document.getElementById("publishPicker")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.rangeDate.controls.start.errors != null || this.rangeDate.controls.end.errors != null) {
      this.messageDate = "Неверный диапазон дат";
      this.invalidDate = true;
      valid = false;
      if (toScroll) {
        document.getElementById("datePicker")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.rangePublish.value.startPublish != null && this.rangePublish.value.endPublish != null) {
      const startPublish = new Date(this.rangePublish.value.startPublish);
      const endPublish = new Date(this.rangePublish.value.endPublish);
      const diffInTime = endPublish.getTime() - startPublish.getTime();
      const diffInDays = Math.round(diffInTime / oneDay);
      if (diffInDays <= 0) {
        this.messagePublish = "Неверный диапазон дат";
        this.invalidPublish = true;
        valid = false;
        if (toScroll) {
          document.getElementById("publishPicker")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
          toScroll = false;
        }
      }
    }
    if (this.rangeDate.value.start != null && this.rangeDate.value.end != null) {
      const startDate = new Date(this.rangeDate.value.start);
      const endDate = new Date(this.rangeDate.value.end);
      const diffInTime = endDate.getTime() - startDate.getTime();
      const diffInDays = Math.round(diffInTime / oneDay);
      if (diffInDays <= 0) {
        this.messageDate = "Неверный диапазон дат";
        this.invalidDate = true;
        valid = false;
        if (toScroll) {
          document.getElementById("datePicker")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
          toScroll = false;
        }
      }
    }
    if (this.startTime != null && this.endTime != null) {
      let startHour = parseInt(this.startTime);
      let endHour = parseInt(this.endTime);
      if (startHour > endHour) {
        this.messageTime = "Неверный диапазон времени";
        this.invalidTime = true;
        valid = false;
        if (toScroll) {
          document.getElementById("timePicker")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
          toScroll = false;
        }
      }
    }
    if (this.startPrice != null && this.endPrice != null) {
      let startPrice = parseInt(this.startPrice);
      let endPrice = parseInt(this.endPrice);
      if (startPrice > endPrice) {
        this.messagePrice = "Неверный ценовой диапазон";
        this.invalidPrice = true;
        valid = false;
        if (toScroll) {
          document.getElementById("pricePicker")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
          toScroll = false;
        }
      }
    }
    if (this.startPrice != undefined && this.endPrice?.trim() == "") {
      this.messagePrice = "Неверный ценовой диапазон";
      this.invalidPrice = true;
      valid = false;
      if (toScroll) {
        document.getElementById("pricePicker")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    return valid;
  }

  public async setFilter(): Promise<void> {
    if (!this.validateFilter()) {
      return;
    }
    let startPublish = " ";
    if (this.rangePublish.value.startPublish != null || this.rangePublish.value.startPublish != undefined) {
      startPublish = this.rangePublish.value.startPublish.toDateString();
    }
    let endPublish = " ";
    if (this.rangePublish.value.endPublish != null || this.rangePublish.value.endPublish != undefined) {
      endPublish = this.rangePublish.value.endPublish.toDateString();
    }
    let startDate = " ";
    if (this.rangeDate.value.start != null || this.rangeDate.value.start != undefined) {
      startDate = this.rangeDate.value.start.toDateString();
    }
    let endDate = " ";
    if (this.rangeDate.value.end != null || this.rangeDate.value.end != undefined) {
      endDate = this.rangeDate.value.end.toDateString();
    }
    let startTime = 0;
    if (this.startTime != null || this.startTime != undefined) {
      startTime = parseInt(this.startTime);
    }
    let endTime = 0;
    if (this.endTime != null || this.endTime != undefined) {
      endTime = parseInt(this.endTime);
    }
    let startPrice = 0;
    if (this.startPrice != null || this.startPrice != undefined) {
      if (!isNaN(parseInt(this.startPrice))) {
        startPrice = parseInt(this.startPrice);
      }
    }
    let endPrice = 0;
    if (this.endPrice != null || this.endPrice != undefined) {
      if (!isNaN(parseInt(this.endPrice))) {
        endPrice = parseInt(this.endPrice);
      }
    }
    let description = " ";
    if (this.description != null || this.description != undefined) {
      if (this.description?.trim() != "") {
        description = this.description?.trim();
      }
    }
    this.router.navigate([this.targetRoute], {
      queryParams: {
        startPublish: startPublish,
        endPublish: endPublish,
        startDate: startDate,
        endDate: endDate,
        startTime: startTime,
        endTime: endTime,
        startPrice: startPrice,
        endPrice: endPrice,
        description: description
      },
      queryParamsHandling: 'merge'
    });
    await this.child?.resetAllParams();
  }

  public async resetFilter(): Promise<void> {
    const searchString = this.route.snapshot.queryParamMap.get('search');
    if (searchString == undefined) {
      this.router.navigateByUrl(this.targetRoute);
      await this.child?.resetAllParams();
      this.resetAllParams();
      return;
    }
    this.router.navigate([this.targetRoute], {
      queryParams: {
        search: searchString
      }
    });
    await this.child?.resetAllParams();
    this.resetAllParams();
  }

  public findbyKeyWord(name: string, index: number): void {
    index++;
    if (this.selectedKey == index) {
      this.selectedKey = 0;
      if (this.route.snapshot.queryParamMap.get('startPublish') != undefined) {
        this.saveFilterParams();
      }
      else {
        this.router.navigateByUrl(this.targetRoute);
      }
      this.findAdvert();
      return;
    }
    this.selectedKey = index;
    this.router.navigate([this.targetRoute], {
      queryParams: {
        search: name
      },
      queryParamsHandling: 'merge'
    });
    this.findAdvert();
  }

  public numberOnly(event: any): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

  public async ngOnInit(): Promise<void> {
    const searchString = this.route.snapshot.queryParamMap.get('search');
    if (searchString != undefined) {
      this.getStartParams(searchString);
    }
    const startPublish = this.route.snapshot.queryParamMap.get('startPublish');
    if (startPublish != undefined) {
      this.getFilterParams();
    }
    await this.accountService.getAuthoriseModel();
  }

  public ngOnDestroy(): void {
    eventSubscriber(this.appComponent.executeAction, this.findAdvert, true);
  }

  public async ngAfterViewInit(): Promise<void> {
    this.observeMenu.forEach((view: ElementRef) =>
      this.createAndObserve(view).subscribe({
        next: async (response) => {
          if (response) {
            this.menuFlag = true;
          }
          else {
            this.menuFlag = false;
          }
        }
      })
    );
  }

  private createAndObserve(element: ElementRef): Observable<boolean> {
    return new Observable((observer) => {
      const intersectionObserver = new IntersectionObserver((entries) => {
        observer.next(entries);
      });
      intersectionObserver.observe(element.nativeElement);
      return () => {
        intersectionObserver.disconnect();
      };
    }).pipe(
      mergeMap((entries: any) => entries),
      map((entry: any) => entry.isIntersecting),
      distinctUntilChanged()
    );
  }

  private getStartParams(param: string): void {
    for (let count = 0; count < this.techniqueObject.techniqueList.length; count++) {
      if (this.techniqueObject.techniqueList[count].search == param) {
        this.selectedKey = count + 1;
        return;
      }
    }
  }

  private saveFilterParams(): void {
    const startPublish = this.route.snapshot.queryParamMap.get('startPublish');
    const endPublish = this.route.snapshot.queryParamMap.get('endPublish');
    const startDate = this.route.snapshot.queryParamMap.get('startDate');
    const endDate = this.route.snapshot.queryParamMap.get('endDate');
    const startTime = this.route.snapshot.queryParamMap.get('startTime');
    const endTime = this.route.snapshot.queryParamMap.get('endTime');
    const startPrice = this.route.snapshot.queryParamMap.get('startPrice');
    const endPrice = this.route.snapshot.queryParamMap.get('endPrice');
    const description = this.route.snapshot.queryParamMap.get('description');
    this.router.navigate([this.targetRoute], {
      queryParams: {
        startPublish: startPublish,
        endPublish: endPublish,
        startDate: startDate,
        endDate: endDate,
        startTime: startTime,
        endTime: endTime,
        startPrice: startPrice,
        endPrice: endPrice,
        description: description
      }
    });
  }

  private getFilterParams(): void {
    const startPublish = this.route.snapshot.queryParamMap.get('startPublish');
    const endPublish = this.route.snapshot.queryParamMap.get('endPublish');
    const startDate = this.route.snapshot.queryParamMap.get('startDate');
    const endDate = this.route.snapshot.queryParamMap.get('endDate');
    const startTime = this.route.snapshot.queryParamMap.get('startTime');
    const endTime = this.route.snapshot.queryParamMap.get('endTime');
    const startPrice = this.route.snapshot.queryParamMap.get('startPrice');
    const endPrice = this.route.snapshot.queryParamMap.get('endPrice');
    const description = this.route.snapshot.queryParamMap.get('description');
    if (startPublish != null && startPublish.trim() != "") {
      this.rangePublish.get("startPublish")?.setValue(new Date(startPublish));
    }
    if (endPublish != null && endPublish.trim() != "") {
      this.rangePublish.get("endPublish")?.setValue(new Date(endPublish));
    }
    if (startDate != null && startDate.trim() != "") {
      this.rangeDate.get("start")?.setValue(new Date(startDate));
    }
    if (endDate != null && endDate.trim() != "") {
      this.rangeDate.get("end")?.setValue(new Date(endDate));
    }
    if (startTime != null && startTime.trim() != "") {
      this.startTime = startTime;
    }
    if (endTime != null && endTime.trim() != "") {
      this.endTime = endTime;
    }
    if (startPrice != null && parseInt(startPrice) != 0) {
        this.startPrice = startPrice;
    }
    if (endPrice != null && parseInt(endPrice) != 0) {
        this.endPrice = endPrice;
    }
    if (description != null && description.trim() != "") {
        this.description = description;
    }
  }

  private resetAllParams(): void {
    this.rangePublish = this.formBuilder.group({
      startPublish: new FormControl<Date | null>(null),
      endPublish: new FormControl<Date | null>(null)
    });
    this.rangeDate = this.formBuilder.group({
      start: new FormControl<Date | null>(null),
      end: new FormControl<Date | null>(null)
    });
    this.startTime = undefined;
    this.endTime = undefined;
    this.startPrice = undefined;
    this.endPrice = undefined;
    this.description = undefined;
  }

}
