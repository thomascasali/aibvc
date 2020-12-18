using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIAuthJWT.Helpers;
using WebAPIAuthJWT.Models;

namespace WebAPIAuthJWT.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/atleti")]
    [Authorize(Roles = "ADMIN, USER")]
    public class AtletiController : Controller
    {
        Database db = new Database();

        [HttpGet("test")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "ADMIN,USER")]
        public IActionResult Test()
        {
            return Ok(new InfoMsg(DateTime.Today, "Test Connessione Ok"));
        }

        //[HttpGet("cerca/codice/{Atleti_Id}", Name = "GetAtleti")]
        [HttpGet("cerca/codice/{Atleti_Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(DataTable))]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetAtletiByCode(string Atleti_Id)
        {
            string sql = "SELECT * FROM Atleti WHERE Atleti_Id = " + Atleti_Id;
            DataTable dt = db.DBTable(sql);

            if (dt.Rows.Count == 0)
            {
                string errore = (this.HttpContext == null) ? "404" : this.HttpContext.Response.StatusCode.ToString();

                return NotFound(new ErrMsg(string.Format("Non è stato trovato l'atleta con il codice '{0}'", Atleti_Id),
                    errore));
            }

            return Ok(dt);
        }
    }
}
