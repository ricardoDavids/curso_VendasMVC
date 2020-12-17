using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas_MVC.Services;
using Vendas_MVC.Models;
using Vendas_MVC.Models.ViewModels;

namespace Vendas_MVC.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService; /* Os "SellersController" tambem vai ter uma dependencia para o "DepartmentService" 
                                                                  Agora vou ter que acrescentar esse _departmentService no constructor para que,
                                                                  ele possa ser injectado no meu objecto */



        public SellersController(SellerService sellerService, DepartmentService departmentService) // Constructor aqui feito para injeccao da dependencia 
        {
            _sellerService = sellerService;
            _departmentService = departmentService; // _departmentService da Classe recebe o departmentService do Argumento;
        }





        public IActionResult Index() // Agora vamos criar uma pagina de Index que vai ser a "View" Entao temos que ir na pasta da "Views" e criar uma subpasta "Sellers" e dentro dessa subpasta adicionar a View Name com template vazio
        /* ESSE Index vai ter que chamar a nossa operação FindAll lá do SellerService e para fazer isso, vamos ter que declarar em 1º lugar uma dependencia para o seller service */

        {

            // chamar a nossa operação findAll lá do SellerService
            var list = _sellerService.FindAll(); // Esta operação vai me retornar uma lista de Seller e depois vou passar essa lista como argumento no meu metodo View, para o metodo gerar um "IActionResult" tendo essa lista aqui
            return View(list);


            // Nota muito IMPORTANTE : Resumindo o que se fez nesta função;

            // Eu chamei o controlador(Index())
            //Depois o controlador acessou o meu Model (var list = _sellerService.FindAll();)
            //Pegou o dado na lista(list) e vai encaminhar esses dados para mminha View
        }







        // 1- Vamos agora criar uma nova acção:
        //  2- Agora Vamos ter que tambem atualizar o nosso metodo create, que é o metodo que vai abrir para nos o formulario para registrar um vendedor
        /*2.1 - Vou ter que atualizar os departamentos; */


        public IActionResult Create()
        {
            var departments = _departmentService.FindAll(); //O metodo FindAll serve Para ele buscar do banco de dados todos os departamentos naquele DepartmentService
                                                            //Agora vamos criar novo objecto do nosso View Model que recebe um novo "SellerFormViewModel" que tem dois dados, os departamentos e os vendedores 
            var viewModel = new SellerFormViewModel { Departments = departments };// No caso aqui do departamentos já vou iniciar um objecto com a "lista" que acabamos de buscar na linha de cima onde tem var departments;
            return View(viewModel); // Feito isto vou passar este objecto ViewModel para minha View



            // Agora as minha tela de registro quando ela for acionada pela 1 vez ela já vai receber este objecto "viewModel" com os departamentos populados
        }








        /*Agora vou ter que indicar que a accao Create(Seller seller) ela vai ser uma acção de "Post" e não de "Get" --> esta ultima é quando voçe cria uma accao no ASP.NET, o "Get" serve para alterar accao */

        [HttpPost]
        [ValidateAntiForgeryToken]



        /*Agora vamos ter que criar accao create com o meotodo Post action que é para inserir.
          No caso dessa operação "Create" ela vai receber um objecto Vendedor que veio na requesição, para que eu receba esse objecto da requisição e instancie esse Vendedor basta colocar ele como parametro*/



        public IActionResult Create(Seller seller)
        {
            // Agora vamos implementar aqui a accao de inserir o create(Seller seller) no banco de dados;
            _sellerService.Insert(seller); // Aqui chamamos o metodo que criamos no "SellerService", repassando este objecto(seller) para lá.
            return RedirectToAction(nameof(Index)); // Agora vou redirecionar a minha requesição para accao "Index" que é a accao que vai mostrar novamente a tela principal do meu "CRUD" de vendedores
        }




        public IActionResult Delete(int? id) // Este delete vai ser opcional e para isso vou ter que testar
        {
            if (id == null)
            {
                // Se o id for nulo signiica que a requesição foi feita de uma forma indevida
                return NotFound(); // Este objecto NotFound ele cria lá uma resposta básica mas depois vamos personalizar isto com uma pagina de erro
            }








            // Proximo passo é pegar quem é este objecto que eu estou mandando deletar
            var obj = _sellerService.FindById(id.Value); // Pronto, busquei do banco de dados 
            if (obj == null)// Agora esse id pode ser um id que nao existe, se nao exisir o meu FindById torna Nullo
            {
                return NotFound();
            }






            // Agora se tudo deu certo eu vou mandar o meu metodo retornar uma view passsando esse objecto como argumento
            return View(obj);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }





        // Vamos criar aqui uma acccao "Details"
        // Essa accao vai receber um Id opcional
        /* Alogica aqui vai ser identica á logica do Remove, porque vou ter que verificar se o "Id" é null,
           depois buscar o meu objecto se ele for null, terei que tambem dar um "NotFound" e depois eu irei retornar o objecto*/

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

    }
}
