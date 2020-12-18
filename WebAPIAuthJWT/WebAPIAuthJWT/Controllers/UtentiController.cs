using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIAuthJWT.Helpers;
using WebAPIAuthJWT.Models;

namespace WebAPIAuthJWT.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/user")]
    public class UtentiController : Controller
    {
        Database db = new Database();

        [HttpPost("auth")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InfoMsg))]
        public IActionResult Authenticate([FromBody] Utenti userParam)
        {
            string tokenJWT = "";

            bool IsOk = db.Authenticate(userParam.Username, userParam.Password);

            if (!IsOk)
            {
                return BadRequest(new InfoMsg(DateTime.Today, string.Format($"Username e/o Password errati.")));
            }
            else
            {
                tokenJWT = db.GetToken(userParam.Username);
            }

            return Ok(new { token = tokenJWT });

        }

        [HttpPost("inserisci")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoMsg))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InfoMsg> SaveUtente([FromBody] Utenti utente)
        {
            if (utente == null)
            {
                return BadRequest(new InfoMsg(DateTime.Today, "Dati utente mancanti."));
            }


            //Contolliamo se l'utente è presente
            var isPresent = db.CheckUser(utente.Username);

            if (isPresent.Rows.Count != 0)
            {
                return StatusCode(422, new InfoMsg(DateTime.Today, $"Utente {utente.Username} già presente."));
            }

            PasswordHasher Hasher = new PasswordHasher();

            //Crptiamo la Password
            utente.Password = Hasher.Hash(utente.Password);

            //verifichiamo che i dati siano stati regolarmente inseriti nel database

            string sql = "";

            // prepara la QUERY
            sql = "";
            sql = sql + "INSERT INTO ";
            sql = sql + "   Utenti(Username, Password) ";
            sql = sql + "VALUES ";
            sql = sql + "   ('" + utente.Username + "','" + utente.Password + "')";

            int retval = db.DBExecuteSQL(sql);

            if (retval==0)
            {
                return StatusCode(500, new InfoMsg(DateTime.Today, $"Errori in inserimento Utente {utente.Username}."));
            }

            return Ok(new InfoMsg(DateTime.Today, $"Inserimento Utente {utente.Username} eseguito con successo."));
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<DataTable>))]
        public ActionResult<DataTable> GetAllUtenti()
        {

            string sql = "";

            // prepara la QUERY
            sql = "";
            sql = sql + "SELECT ";
            sql = sql + "   * ";
            sql = sql + "FROM ";
            sql = sql + "   Utenti ";
            sql = sql + "ORDER BY ";
            sql = sql + "   Username ";

            DataTable dt = db.DBTable(sql);

            if (dt.Rows.Count == 0)
            {
                return NotFound(string.Format("Utenti non presenti."));
            }

            return Ok(dt);
        }
    }
}
