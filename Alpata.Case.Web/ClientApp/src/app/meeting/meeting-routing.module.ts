import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MeetingListComponent } from './meeting-list/meeting-list.component';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'list', component: MeetingListComponent },
        { path: '**', redirectTo: '/notfound' }
    ])],
    exports: [RouterModule]
})
export class MeetingRoutingModule { }
