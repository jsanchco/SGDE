import {PreloadAllModules, RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {DashboardPageComponent} from '../pages/dashboard-page/dashboard-page.component';
import {LoginPageComponent} from '../pages/login-page/login-page.component';
import {MainPageComponent} from '../pages/main-page/main-page.component';
import {RegisterPageComponent} from '../pages/register-page/register-page.component';
import {ComingSoonPageComponent} from '../pages/coming-soon-page/coming-soon-page.component';
import {MaintenancePageComponent} from '../pages/maintenance-page/maintenance-page.component';
import {NotFoundPageComponent} from '../pages/not-found-page/not-found-page.component';
import {ProfilePageComponent} from '../pages/profile-page/profile-page.component';
import {UsersPageComponent} from '../pages/users-page/users-page.component';
import {ProfessionsPageComponent} from '../pages/professions-page/professions-page.component';

import {AuthorizatedGuard} from '../shared/guard/authorizated.guard';

// Routes model for application. Some of the pages are loaded lazily to increase startup time.
const APP_ROUTES: Routes = [
  {
    path: 'main', canActivate: [AuthorizatedGuard], component: MainPageComponent, children: [
      {path: 'dashboard', component: DashboardPageComponent},
      {path: 'users', component: UsersPageComponent},
      {path: 'professions', component: ProfessionsPageComponent},
      {
        path: 'car',
        loadChildren: './pages/car-page/car.module#CarModule'
      },
      {path: 'login', component: LoginPageComponent},
      {path: 'profile', component: ProfilePageComponent},
      {path: '', redirectTo: 'dashboard', pathMatch: 'prefix'},
      {path: '**', redirectTo: 'dashboard', pathMatch: 'prefix'}]
  },
  {path: 'login', component: LoginPageComponent},
  {path: 'register', component: RegisterPageComponent},
  {path: 'coming-soon', component: ComingSoonPageComponent},
  {path: 'maintenance', component: MaintenancePageComponent},
  {path: '404', component: NotFoundPageComponent},
  {path: '', redirectTo: '/main/dashboard', pathMatch: 'prefix'},
  {path: '**', redirectTo: '/main/dashboard', pathMatch: 'prefix'}

];

@NgModule({
  imports: [
    RouterModule.forRoot(APP_ROUTES, {preloadingStrategy: PreloadAllModules}),
  ],
  providers: [
    AuthorizatedGuard
  ]
})

export class AppRoutesModule {
}
