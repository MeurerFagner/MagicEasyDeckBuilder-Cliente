import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CadastroUsuario } from 'src/app/models/usuario/cadastro-usuario.model';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
  selector: 'app-cadastro-usuario',
  templateUrl: './cadastro-usuario.component.html',
  styleUrls: ['./cadastro-usuario.component.scss']
})
export class CadastroUsuarioComponent implements OnInit {
  public dadosForm = new CadastroUsuario();
  public erroCadastro?: string;

  constructor(
    private usuarioService: UsuarioService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  cadastrar(){
    this.erroCadastro = '';

    this.usuarioService.cadastrar(this.dadosForm).subscribe(
      result =>{
        this.usuarioService.registrarLogin(result);

        this.router.navigate(['/'])
      },
      erro =>{
        this.erroCadastro = erro.error;
      }
    )
  }

}
