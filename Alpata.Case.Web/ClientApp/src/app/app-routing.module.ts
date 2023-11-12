import { NgModule } from '@angular/core';
import { ExtraOptions, RouterModule, Routes } from '@angular/router';
import { AppLayoutComponent } from './layout/app.layout.component';
import { AuthGuard } from './auth.guard';

const routerOptions: ExtraOptions = {
    anchorScrolling: 'enabled'
};

const routes: Routes = [
    {
        path: '', component: AppLayoutComponent,
        canActivate:[AuthGuard],
        canActivateChild:[AuthGuard],
        children: [
            { path: 'meeting', loadChildren: () => import('./meeting/meeting.module').then(m => m.MeetingModule) },
            { path: 'profile', loadChildren: () => import('./common/profile/profile.module').then(m => m.ProfileModule) },
        ]
    },
    { path: 'auth', loadChildren: () => import('./common/auth/auth.module').then(m => m.AuthModule) },
    { path: '**', redirectTo: '/notfound' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, routerOptions)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
