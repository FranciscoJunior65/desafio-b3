import { Injectable } from "@angular/core";
import { catchError, Observable, throwError } from "rxjs";
import { environment } from "../environments/environment";
import { HttpClient, HttpParams } from "@angular/common/http";

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})

export class ApiGraficoService {

  constructor(private http: HttpClient) {}

  public GetCalculoCDB(valorMonetario: any, prazoMeses: any): Observable<any>{
    let params = new HttpParams()
    .set('valorMonetario', valorMonetario.toString())
    .set('prazoMeses', prazoMeses.toString());

    return this.http.get<any>(`${API_URL}/CalculoCDB`, { params })
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any): Observable<never> {
    console.error('Ocorreu um erro', error);
    return throwError(() => new Error(error.message || 'Erro desconhecido'));
  }
}
