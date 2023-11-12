import jwt_decode from "jwt-decode";



export class CurrentUser {

    static decodedToken() {
        var token: any = localStorage.getItem("accessToken");
        if (token) {
            var decodedToken: any = jwt_decode(token);
            return decodedToken;
        }
        return "";

    }

    static get Name() {
        return CurrentUser.decodedToken().Name;
    }

    static get Surname() {
        return CurrentUser.decodedToken().Surname;
    }

    static get FullName() {
        return CurrentUser.decodedToken().FullName;
    }

    static get Email() {
        return CurrentUser.decodedToken().Email;
    }

    static get PhoneNumber() {
        return CurrentUser.decodedToken().PhoneNumber;
    }

    static get Id() {
        return CurrentUser.decodedToken().Guid;
    }
}