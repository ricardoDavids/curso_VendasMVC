using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas_MVC.Services;

namespace Vendas_MVC.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService) // Constructor aqui feito para injeccao da dependencia 
        {
            _sellerService = sellerService;
        }


        public IActionResult Index() // Agora vamos criar uma pagina de Index que vai ser a "View" Entao temos que ir na pasta da "Views" e criar uma subpasta "Sellers" e dentro dessa subpasta adicionar a View Name com template vazio
                                     /* ESSE Index vai ter que chamar a nossa operação FindAll lá do SellerService e para fazer isso, vamos ter que declarar em 1º lugar uma dependencia para o seller service */
            
        {
            // chamar a nossa operação findAll lá do SellerService
            var list = _sellerService.FindAll(); // Esta operação vai me retornar uma lista de Seller e depois vou passar essa lista como argumento no meu metodo View, para o metodo gerar um "IActionResult" tendo essa lista aqui
            return View();


            // Nota muito IMPORTANTE : Resumindo o que se fez nesta função;

            // Eu chamei o controlador(Index())
            //Depois o controlador acessou o meu Model (var list = _sellerService.FindAll();)
            //Pegou o dado na lista(list) e vai encaminhar esses dados para mminha View
        }
    }
}
