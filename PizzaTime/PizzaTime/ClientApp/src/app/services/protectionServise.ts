import { User } from "../models/user";
import { Observable } from "rxjs/Observable";
import { Subject } from "rxjs/Subject";
import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs/BehaviorSubject";

@Injectable()
export class ProtectionServise {

    private token: string;

    isAuthorised: boolean;

    user: User;

    constructor(){
        //cookie?
    }

    Login(login:string, password:string): Observable<boolean> {

        if(login=="" || password == "")
            return new BehaviorSubject(false);

        this.user = new User("UserName","user@user.com")

        this.isAuthorised = true;

        return new BehaviorSubject(this.isAuthorised);
    }

    LogOff() {

        this.user = null;
        this.isAuthorised = false;
        this.token = null;
    }

    GetToken(): string {
        if (!this.isAuthorised)
            return null;
        return this.token;
    }

    Register (user:User, password:string):Observable<any>{
      
        return new BehaviorSubject(true);

    }

}
