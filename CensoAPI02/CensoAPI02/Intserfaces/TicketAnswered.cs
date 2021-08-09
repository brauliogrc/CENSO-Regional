using CENSO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Intserfaces
{
    public class TicketAnswered
    {
        private AnswerData respuesta = new AnswerData();

        public AnswerData answered(CDBContext _context, int tikcetId)
        {
            try
            {
                if (answerTiket(_context, tikcetId) != null) return answerTiket(_context, tikcetId);
                else if (answerAnonTicket(_context, tikcetId) != null) return answerAnonTicket(_context, tikcetId);
                else
                {
                    //this.respuesta = null;
                    this.respuesta.flag = 0;
                    return this.respuesta;
                }
            }
            catch(Exception ex)
            {
                this.respuesta = null;
                this.respuesta.flag = 0;
                return this.respuesta;
            }
        }

        // Busqqueda de una respuesta sociada a un ticket
        private AnswerData answerTiket(CDBContext _context, int ticketId)
        {
            try
            {
                var ticket = from answer in _context.Answer
                             where answer.RequestId == ticketId
                             select new {
                                 // Datos de la respuesta
                                 answer.asId,
                                 answer.asAnswer
                             };

                if (ticket == null || ticket.Count() == 0)
                {
                    return null;
                }

                this.respuesta.asId = ticket.First().asId;
                this.respuesta.asAnswer = ticket.First().asAnswer;
                this.respuesta.flag = 1;

                return this.respuesta;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // Busqueda de la respuesta asociada a un ticket anonimo
        private AnswerData answerAnonTicket(CDBContext _context, int ticketId)
        {
            try
            {
                var ticket = from answer in _context.Answer
                             where answer.AnonRequestId == ticketId
                             select new {
                                 // Datos de la respuesta
                                 answer.asId,
                                 answer.asAnswer
                             };

                if (ticket == null || ticket.Count() == 0)
                {
                    return null;
                }

                this.respuesta.asId = ticket.First().asId;
                this.respuesta.asAnswer = ticket.First().asAnswer;
                this.respuesta.flag = 1;

                return this.respuesta;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
