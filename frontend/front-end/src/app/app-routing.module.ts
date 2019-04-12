import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PSignupComponent } from './p-signup/p-signup.component';
import { PSigninComponent } from './p-signin/p-signin.component';
import { PDashboardComponent } from './p-dashboard/p-dashboard.component';

const routes: Routes = [
  { path: 'signup', component: PSignupComponent },
  { path: 'dashboard', component: PDashboardComponent },
  { path: '', component: PSigninComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  declarations: []
})

export class AppRoutingModule { }
