import { RouterModule, Routes } from "@angular/router";
import { PaginaPrincipalComponent } from "./pagina-principal/pagina-principal.component";
import { NgModule } from "@angular/core";

const routes: Routes = [{
    path: '',
    component: PaginaPrincipalComponent
  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }
