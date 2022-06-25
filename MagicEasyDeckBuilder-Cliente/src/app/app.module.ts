import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SaibaMaisComponent } from './components/saiba-mais/saiba-mais.component';
import { LembraSenhaComponent } from './components/lembra-senha/lembra-senha.component';
import { CadastroUsuarioComponent } from './components/cadastro-usuario/cadastro-usuario.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FormsModule } from '@angular/forms';
import { UsuarioService } from './services/usuario.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ComparaCampoValidatorDirective } from './directives/compara-campo-validator.directive';
import { AuthGuard } from './services/app.guard';
import { APP_BASE_HREF } from '@angular/common';
import { DeckModule } from './components/deck/deck.module';

@NgModule({
  declarations: [
    AppComponent,
    SaibaMaisComponent,
    LembraSenhaComponent,
    CadastroUsuarioComponent,
    LoginComponent,
    HomeComponent,
    FooterComponent,
    NavbarComponent,
    ComparaCampoValidatorDirective,
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    NgbModule,
    FormsModule,
    DeckModule
  ],
  providers: [
    {provide: APP_BASE_HREF, useValue: '/'},
    HttpClient,
    UsuarioService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
