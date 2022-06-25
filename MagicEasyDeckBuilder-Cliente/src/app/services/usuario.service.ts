import { HttpClient } from "@angular/common/http"
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { CadastroUsuario } from "../models/usuario/cadastro-usuario.model";
import { LoginModel } from "../models/usuario/login.model";
import { Usuario } from "../models/usuario/usuario.model";

@Injectable()
export class UsuarioService{
  private url = environment.baseUrl + '/usuario'

  constructor(
    private http: HttpClient
  ){}

  public logar(loginModel: LoginModel) : Observable<Usuario>{
    return this.http.post<Usuario>(this.url+'/login', loginModel);
  }

  public cadastrar(usuario: CadastroUsuario){
    return this.http.post<Usuario>(this.url + '/cadastro',usuario);
  }

  public registrarLogin(usuario: Usuario){
    localStorage.setItem("nome",usuario.nome)
    localStorage.setItem("email",usuario.email)
    localStorage.setItem("token",usuario.token)
    
  }

  public getToken() : string| null {
    return localStorage.getItem('token');
  }

  public getNome() : string| null {
    return localStorage.getItem('nome');
  }

  public limparDados(){
    localStorage.clear()
  }

  public usuarioLogado(): boolean{
    return this.getNome() !== null
  }

}