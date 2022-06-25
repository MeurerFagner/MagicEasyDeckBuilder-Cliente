import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginModel } from 'src/app/models/usuario/login.model';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public login = new LoginModel();
  public loginErro?: string;

  constructor(
    private usuarioService: UsuarioService,    
    private router: Router

  ) { }

  ngOnInit(): void {
  }

  logar()
  {
    this.loginErro = ""
    this.usuarioService.logar(this.login).subscribe(
      result => {
        this.usuarioService.registrarLogin(result);

        this.router.navigate(['/'])
      },
      erro => {
        this.loginErro = erro.error
      }
    )

  }

}
