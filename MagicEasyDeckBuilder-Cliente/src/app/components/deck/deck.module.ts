import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { DeckDashboardComponent } from './deck-dashboard/deck-dashboard.component';
import { DeckRoutingModule } from "./deck.routing";

@NgModule({
  declarations:[
    DeckDashboardComponent
  ],
  imports:[
    CommonModule,
    DeckRoutingModule
  ],
  exports:[],
  providers:[]
})
export class DeckModule{

}