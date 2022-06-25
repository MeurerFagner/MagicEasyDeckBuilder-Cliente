import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  public isCollapsed = true;
  public nomeUsuario!: string | null;

  constructor(
    private usuarioService: UsuarioService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  UsuarioLogado() : boolean{
    this.nomeUsuario = this.usuarioService.getNome()

    return this.nomeUsuario !== null;
  }

  logout(){
    this.usuarioService.limparDados()
    this.router.navigate(['/']);
  }
}
