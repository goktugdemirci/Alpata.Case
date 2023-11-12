
export class User {
    constructor(
        id: string
    ) {
        this.id = id
    }
    id: string;
    name!: string;
    surname!: string;
    fullName!: string;
    email!: string;
    phoneNumber!: string;
    password!: string;
}