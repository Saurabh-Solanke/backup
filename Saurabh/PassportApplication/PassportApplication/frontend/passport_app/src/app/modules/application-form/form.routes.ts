import { Routes } from "@angular/router";
import { SectionComponent } from "./section/section.component";
import { RenewalFormComponent } from "./renewal-section/renewal-section.component";
import { AuthGuard } from "../../guards/auth.guard";

export const routes :Routes =[
    
    {
        path:'section/:sectionNo',
        canActivate: [AuthGuard],
        component:SectionComponent,
    },
    {
        path:'renewal-section/:sectionNo',
        canActivate: [AuthGuard],
        component:RenewalFormComponent
    }
]