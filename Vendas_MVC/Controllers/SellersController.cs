using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendas_MVC.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index() // Agora vamos criar uma pagina de Index que vai ser a "View" Entao temos que ir na pasta da "Views" e criar uma subpasta "Sellers" e dentro dessa subpasta adicionar a View Name com template vazio
            
        {
            return View();
        }
    }
}
