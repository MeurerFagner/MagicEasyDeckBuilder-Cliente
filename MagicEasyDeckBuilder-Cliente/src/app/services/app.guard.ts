import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, CanLoad, Route, RouterStateSnapshot, UrlSegment, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { UsuarioService } from "./usuario.service";

@Injectable()
export class AuthGuard implements CanLoad, CanActivate{
  constructor(
    private usuarioService: UsuarioService
  ){}
  canActivate(): boolean  {
    return this.usuarioService.usuarioLogado()
  }
  canLoad(): boolean {
    return this.usuarioService.usuarioLogado()
  }

}