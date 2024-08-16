using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanca.Api.Controllers
{
    public class BaseController : ControllerBase
   {
        protected int _idUsuario; 
        protected int ObterIdUsuarioLogado()
        {
            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            int.TryParse(id, out int idUsuario);

            return idUsuario;
        }
    }
}