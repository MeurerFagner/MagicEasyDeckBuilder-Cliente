import { HttpInterceptorFn } from '@angular/common/http';

export const apiAuthInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req);
};
