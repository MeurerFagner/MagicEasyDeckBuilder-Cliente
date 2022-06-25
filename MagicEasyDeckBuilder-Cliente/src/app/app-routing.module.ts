import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CadastroUsuarioComponent } from './components/cadastro-usuario/cadastro-usuario.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { SaibaMaisComponent } from './components/saiba-mais/saiba-mais.component';
import { AuthGuard } from './services/app.guard';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'saiba-mais', component: SaibaMaisComponent },
  { path: 'cadastro', component: CadastroUsuarioComponent },
  { path: 'login', component: LoginComponent },
  {
    path: 'deck',
    loadChildren: () => import('./components/deck/deck.module')
      .then(m => m.DeckModule),
    canLoad: [AuthGuard],
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
