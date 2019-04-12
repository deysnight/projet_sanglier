import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MaterialModule } from '../material-module';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { MatNativeDateModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthService } from './auth.service';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PSignupComponent } from './p-signup/p-signup.component';
import { PSigninComponent } from './p-signin/p-signin.component';
import { PDashboardComponent } from './p-dashboard/p-dashboard.component';
import { AutocompleteAccaComponent } from './autocomplete-acca/autocomplete-acca.component';

@NgModule({
  declarations: [
    AppComponent,
    PSignupComponent,
    PSigninComponent,
    PDashboardComponent,
    AutocompleteAccaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    BrowserAnimationsModule
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
