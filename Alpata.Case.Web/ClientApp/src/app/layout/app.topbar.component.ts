import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { CurrentUser } from '../common/descriptions/currentUser';
import { AppSidebarComponent } from './app.sidebar.component';

@Component({
    selector: 'app-topbar',
    templateUrl: './app.topbar.component.html'
})
export class AppTopbarComponent {

    @ViewChild('menubutton') menuButton!: ElementRef;

    @ViewChild(AppSidebarComponent) appSidebar!: AppSidebarComponent;
    constructor(public layoutService: LayoutService, public el: ElementRef, private router: Router) {

    }

    get userFullName() {
        if (CurrentUser) {
            return CurrentUser.FullName;
        }
        else{
            return "";
        }
    }

    onMenuButtonClick() {
        this.layoutService.onMenuToggle();
    }

    onProfileButtonClick() {
        this.layoutService.showRightMenu();
    }

    onSearchClick() {
        this.layoutService.toggleSearchBar();
    }

    onRightMenuClick() {
        this.layoutService.showRightMenu();
    }

    get logo() {
        const logo = this.layoutService.config.menuTheme === 'white' || this.layoutService.config.menuTheme === 'orange' ? 'dark' : 'white';
        return logo;
    }
    myProfileClick(){
        this.router.navigate(["/profile/create"])
    }

    logout() {
        localStorage.clear();
        this.router.navigate(["/auth/login"])
    }
}
