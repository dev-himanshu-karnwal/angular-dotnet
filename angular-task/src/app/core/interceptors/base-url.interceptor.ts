import { HttpInterceptorFn, HttpRequest } from '@angular/common/http';

const BASE_URL = 'http://192.168.29.220:5000';

export const baseUrlInterceptor: HttpInterceptorFn = (
  request: HttpRequest<any>,
  next
) => {
  const url = encodeURI(`${BASE_URL}${request.url}`);
  const modifiedRequest = request.clone({ url });

  return next(modifiedRequest);
};
