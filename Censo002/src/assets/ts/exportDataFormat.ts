import { LocationValidate } from './validations';

class ObjectFromat {
    ticketId: any;
    userId: any;
    userName:any;
    enployeeLeader: any;
    Issue : any;
    Attachement : any;
    employeeType: any;
    creationDate: any;
    location: any;
    theme: any;
    question: any;
    area: any;
    status: any;
    asId : any;
    asAnswer : any;
    asAttachement : any;
    asCreationDate : any;
    uEmployeeNumber : any;
    uName : any;
    uEmail : any;
    uSupervisorNumber : any;

    
    static ticket ( {
        // Datos del ticket
        rId,
        rUserId,
        rUserName,
        rIssue,
        rAttachement,
        rEmployeeLeader,
        rEmployeeType,
        rCreationDate,
        // Datos de la localidad
        lName,
        // Datos del tema
        tName,
        // Datos de la pregunta
        qName,
        // Datos del area
        aName,
        // Datos del estatus
        rsStatus,
        // Datos de la respuesta
        asId,
        asAnswer,
        asAttachement,
        asCreationDate,
        // Datos del usuario que responde
        uEmployeeNumber,
        uName,
        uEmail,
        uSupervisorNumber
    } ) {
        const validation = new LocationValidate();
        rEmployeeType = validation.employeeTypeValidation(rEmployeeType);
        return new ObjectFromat(rId, rUserId, rUserName, rIssue, rAttachement, rEmployeeLeader, rEmployeeType, rCreationDate, lName, tName, qName, aName, rsStatus, asId, asAnswer, asAttachement, asCreationDate, uEmployeeNumber,uName, uEmail, uSupervisorNumber);
    }

    static anonTicket( {
        // Datos del ticket
        arId,
        arIssue,
        arAttachement,
        arEmployeeType,
        arCreationDate,
        // Datos de la localidad
        lName,
        // Datos del tema
        tName,
        // Datos de la pregunta
        qName,
        // Datos del area
        aName,
        // Datos del status
        rsStatus,
        // Datos de la respuesta
        asId,
        asAnswer,
        asAttachement,
        asCreationDate,
        // Datos del usuario que responde
        uEmployeeNumber,
        uName,
        uEmail,
        uSupervisorNumber,
    } ){
        const validation = new LocationValidate();
        arEmployeeType = validation.employeeTypeValidation(arEmployeeType);
        return new ObjectFromat(arId, null, null, arIssue, arAttachement, null, arEmployeeType, arCreationDate, lName, tName, qName, aName, rsStatus, asId, asAnswer, asAttachement, asCreationDate, uEmployeeNumber, uName, uEmail, uSupervisorNumber);
    }

    constructor( 
        ticketId: any,
        userId: any,
        userName: any,
        Issue: any,
        Attachement: any,
        enployeeLeader: any,
        employeeType: any,
        creationDate: any,
        location: any,
        theme: any,
        question: any,
        area: any,
        status: any,
        asId: any,
        asAnswer: any,
        asAttachement: any,
        asCreationDate: any,
        uEmployeeNumber: any,
        uName: any,
        uEmail: any,
        uSupervisorNumber: any,
        ) {
        this.ticketId = ticketId;
        this.userId = userId;
        this.userName = userName;
        this.Issue = Issue;
        this.Attachement = Attachement;
        this.enployeeLeader = enployeeLeader;
        this.employeeType = employeeType;
        this.creationDate = new Date(creationDate);
        this.location = location;
        this.theme = theme;
        this.question = question;
        this.area = area;
        this.status = status;
        this.asId = asId;
        this.asAnswer = asAnswer;
        this.asAttachement = asAttachement;
        // this.asCreationDate = new Date(asCreationDate);
        this.uEmployeeNumber = uEmployeeNumber;
        this.uName = uName;
        this.uEmail = uEmail;
        this.uSupervisorNumber = uSupervisorNumber;

        ( asCreationDate ) ? this.asCreationDate = new Date(asCreationDate) : null;
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