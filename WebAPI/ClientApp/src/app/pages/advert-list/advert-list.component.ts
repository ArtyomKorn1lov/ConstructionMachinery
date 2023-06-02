import { Component, ElementRef, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { AdvertComponent } from 'src/app/components/advert/advert.component';
import { DialogFilterComponent } from 'src/app/components/dialog-filter/dialog-filter.component';
import { MatDialog } from '@angular/material/dialog';
import { Filter } from 'src/app/models/Filter';
import { Title } from '@angular/platform-browser';
import { AdvertService } from 'src/app/services/advert.service';
import { Observable, distinctUntilChanged, map, mergeMap } from 'rxjs';
import { AppComponent } from 'src/app/app.component';
import { eventSubscriber } from 'src/app/services/common-child.interface';
import { TechniqueTypeList } from 'src/app/models/TechniqueTypeList';
import { ActivatedRoute, Router } from '@angular/router';

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
  @ViewChild(AdvertComponent) child: AdvertComponent | undefined;
  @ViewChildren("observeMenu") observeMenu!: QueryList<ElementRef>;
  private targetRoute = "/advert-list";

  constructor(private accountService: AccountService, private dialog: MatDialog, public titleService: Title, public advertService: AdvertService,
    private appComponent: AppComponent, private router: Router, private route: ActivatedRoute) {
    this.titleService.setTitle("Список объявлений");
    this.findAdvert = this.findAdvert.bind(this);
    eventSubscriber(appComponent.executeAction, this.findAdvert);
  }

  public async selectEvent(): Promise<void> {
    await this.child?.sortByParam(this.filterState);
  }

  public async findAdvert(): Promise<void> {
    await this.child?.resetAllParams();
  }

  public findbyKeyWord(name: string, index: number): void {
    index++;
    if (this.selectedKey == index) {
      this.selectedKey = 0;
      this.router.navigateByUrl(this.targetRoute);
      this.findAdvert();
      return;
    } 
    this.selectedKey = index;
    this.router.navigate([this.targetRoute], {
      queryParams: {
        search: name
      }
    });
    this.findAdvert();
  }

  public openFilterDialog(): void {
    const dialogRef = this.dialog.open(DialogFilterComponent);
    dialogRef.afterClosed().subscribe(async result => {
      if (result == undefined)
        return;
      this.filters = result.data;
      await this.child?.sortByParam(this.filters[0].param);
    });
  }

  public async ngOnInit(): Promise<void> {
    const searchString = this.route.snapshot.queryParamMap.get('search');
    if (searchString != undefined) {
      this.getStartParams(searchString);
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

}
