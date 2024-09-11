import { Routes } from '@angular/router';
import { UserInfoComponent } from './user-info.component';
import { UpdateProfileComponent } from './update-profile/update-profile.component';
import { NotificationComponent } from './notification/notification.component';


export const routes :Routes=[
   
    {
        path:'',
        component:UserInfoComponent
    },
    {
        path:'update-profile',
        component:UpdateProfileComponent
    },
    {
        path:'notifications',
        component:NotificationComponent
    }
]