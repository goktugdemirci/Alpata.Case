import { OnInit } from '@angular/core';
import { Component } from '@angular/core';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit {

    model: any[] = [];

    ngOnInit() {
        this.model = [
            {
                label: 'Application',
                icon: 'pi pi-fw pi-check-square',
                items: [
                    {
                        label: 'Meetings',
                        icon: 'pi pi-fw pi-image',
                        routerLink: ['/meeting/list']
                    }
                ]
            }
        ];
    }
}
