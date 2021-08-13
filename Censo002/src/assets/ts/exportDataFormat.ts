import { LocationValidate } from './validations';

class ObjectFromat {
    ticketId: any;
    userId: any;
    userName:any;
    enployeeLeader: any;
    employeeType: any;
    creationDate: any;
    location: any;
    theme: any;
    question: any;
    area: any;
    status: any;

    
    static ticket ( {rId, rUserId, rUserName, rEmployeeLeader, rEmployeeType, rCreationDate, lName, tName, qName, aName, rsStatus} ) {
        const validation = new LocationValidate();
        rEmployeeType = validation.employeeTypeValidation(rEmployeeType);
        return new ObjectFromat(rId, rUserId, rUserName, rEmployeeLeader, rEmployeeType, rCreationDate, lName, tName, qName, aName, rsStatus);
    }

    static anonTicket( {arId, arEmployeeType, arCreationDate, lName, tName, qName, aName, rsStatus} ){
        const validation = new LocationValidate();
        arEmployeeType = validation.employeeTypeValidation(arEmployeeType);
        return new ObjectFromat(arId, null, null, null, arEmployeeType, arCreationDate, lName, tName, qName, aName, rsStatus);
    }

    constructor( ticketId: any, userId: any, userName: any, enployeeLeader: any, employeeType: any, creationDate: any, location: any, theme: any, question: any, area: any, status: any ) {
        this.ticketId = ticketId;
        this.userId = userId;
        this.userName = userName;
        this.enployeeLeader = enployeeLeader;
        this.employeeType = employeeType;
        this.creationDate = new Date(creationDate);
        this.location = location;
        this.theme = theme;
        this.question = question;
        this.area = area;
        this.status = status;
    }
}

export class ConverToObjectArray {
    private list: any = {};

    constructor() {
        this.list = {};
    }

    get ticketList() {
        const listado = [];
        Object.keys(this.list).forEach( key => {
            const ticket = this.list[key];
            listado.push(ticket);
        })

        return listado;
    }

    addTciket(ticketData: any): void {
        const ticket = ObjectFromat.ticket(ticketData);
        this.list[ticket.ticketId] = ticket;
    }

    addAnonTicket(anonTicketData : any): void {
        const anonTicket = ObjectFromat.anonTicket(anonTicketData);
        this.list[anonTicket.ticketId] = anonTicket;
    }
}