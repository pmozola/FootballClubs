import { environment } from "src/environments/environment";

export const apiUrls = {
    auth:{
        register: environment.apiUrl + "auth/register",
        login: environment.apiUrl + "auth/login"
    }
}