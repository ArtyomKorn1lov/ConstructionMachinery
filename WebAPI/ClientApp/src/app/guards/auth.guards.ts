import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { TokenService } from '../services/token.service';

@Injectable({
    providedIn: 'root'
})

export class AuthGuard implements CanActivate {

    public constructor(private router: Router, private tokenService: TokenService) { }

    public async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
        const result = await this.tokenService.tokenVerify();
        if(!result)
            this.router.navigate(["/authorize"]);
        return result;
    }
}