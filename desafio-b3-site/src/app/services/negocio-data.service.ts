import { Injectable } from "@angular/core";
import { ApiGraficoService } from "./api.service";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class NegocioDataService {

  constructor(private api: ApiGraficoService) {

  }

  public GetCalculoCDB(valorMonetario: any, prazoMeses: any): Observable<any>{
    return this.api.GetCalculoCDB(valorMonetario, prazoMeses);
  }
}
