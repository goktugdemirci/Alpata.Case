import { MeetingAttachment } from "./meetingAttachment";

export class Meeting {
    constructor(
        id: string
    ) {
        this.id = id
    }
    id: string;
    name!: string;
    startTime!:string|Date;
    endTime!:string|Date;
    explanation!:string;
    attachments:MeetingAttachment[] = [];
}