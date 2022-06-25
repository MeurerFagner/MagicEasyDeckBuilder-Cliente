import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { DeckDashboardComponent } from "./deck-dashboard/deck-dashboard.component";

const deckRouterConfig: Routes = [
  {path:'',redirectTo:'dashboard', pathMatch: 'prefix'},
  {path:'dashboard', component:DeckDashboardComponent}
]

@NgModule({
  imports:[
    RouterModule.forChild(deckRouterConfig)
  ],
  exports:[
    RouterModule
  ]
})
export class DeckRoutingModule{}