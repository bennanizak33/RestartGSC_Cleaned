using McDonalds.Data.Context;
using McDonalds.Data.Models;
using McDonalds.Domain;
using McDonalds.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace McDonalds.ApiControllers
{
    public class AuthorizationsController : ApiController
    {
        private McDonaldsContext Context = new McDonaldsContext();

        private int RestaurantId = default(int);
        private DateTime OperationDateTime => DateTime.Now;

        // POST: api/ServerEvents
        [ResponseType(typeof(AuthorizationModel))]
        public IHttpActionResult PostAuthorization( string ipAddress, DateTime upTimes)
        {
            DateTime upTimeDate = upTimes.Date;

            try
            {
                if (!IpAddressRestriction.IsValid(Context, ipAddress, out RestaurantId))
                {
                    return Ok(new AuthorizationModel()
                    {
                        StatusCode = "KO",
                        Detail = "Addresse Ip invalide"
                    });
                }

                if (!TwoWeekRestartRestriction.CheckLastServerUpTimes(Context, RestaurantId, upTimeDate))
                {
                    Context.ServerEvents.Add(new ServerEvent()
                    {
                        Date = OperationDateTime,
                        RestaurantId = RestaurantId,
                        Event = Event.RedemarrageOK,
                        Detail = "Dernier redémarrage Serveur",
                        UpTimes = upTimeDate
                    });

                    Context.SaveChanges();
                }

                if (!TwoWeekRestartRestriction.IsValid(Context, RestaurantId, OperationDateTime))
                {
                    return Ok(new AuthorizationModel()
                    {
                        StatusCode = "KO",
                        Detail = "Le dernier redémarrage est inferieur a 15 jours"
                    });
                }

                if (!MaxRestartRestriction.IsValid(Context, OperationDateTime))
                {
                    return Ok(new AuthorizationModel()
                    {
                        StatusCode = "KO",
                        Detail = "Vous avez depasser le nombre de redémarrage authorisé"
                    });
                }

                if (!StartingDateRestriction.IsValid(Context, OperationDateTime))
                {

                    return Ok(new AuthorizationModel()
                    {
                        StatusCode = "KO",
                        Detail = "Impossible d'executer un redémarrage aujourd'hui"
                    });
                }

                if (DeploiementDateRestriction.CheckDeploiementDate(RestaurantId, OperationDateTime))
                {
                    return Ok(new AuthorizationModel()
                    {
                        StatusCode = "KO",
                        Detail = "Impossible d'executer un redémarrage aujourd'hui, un deploiement est prevu"
                    });
                }

                if (PriorityRestriction.CheckPriority(Context, RestaurantId))
                {
                    Context.ServerEvents.Add
                    (
                         new ServerEvent()
                         {
                             Date = OperationDateTime,
                             Event = Event.DemandeRejete,
                             Detail = "Demande de redémarrage non prioritaire",
                             UpTimes = upTimeDate
                         }
                    );

                    Context.SaveChanges();

                    return Ok(new AuthorizationModel()
                    {
                        StatusCode = "KO",
                        Detail = "Demande de redémarrage non prioritaire"
                    });
                }

                return Ok(new AuthorizationModel()
                {
                    StatusCode = "OK",
                    Detail = "Vous pouvez redémarrer la machine distante"
                });
            }
            catch(Exception ex)
            {
                return Ok(new AuthorizationModel()
                {
                    StatusCode =  "ERREUR",
                    Detail = ex.StackTrace
                });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
