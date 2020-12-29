using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas_MVC.Services;
using Vendas_MVC.Models;
using Vendas_MVC.Models.ViewModels;
using Vendas_MVC.Services.Exceptions;
using System.Diagnostics;

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





        public async Task<IActionResult> Index() // Agora vamos criar uma pagina de Index que vai ser a "View" Entao temos que ir na pasta da "Views" e criar uma subpasta "Sellers" e dentro dessa subpasta adicionar a View Name com template vazio
        /* ESSE Index vai ter que chamar a nossa operação FindAll lá do SellerService e para fazer isso, vamos ter que declarar em 1º lugar uma dependencia para o seller service */

        {

            // chamar a nossa operação findAll lá do SellerService

            // A nossa indicação "await" para o metodo esperar a resposta dessa chamada que é assincrona
            var list = await _sellerService.FindAllAsync(); // Esta operação vai me retornar uma lista de Seller e depois vou passar essa lista como argumento no meu metodo View, para o metodo gerar um "IActionResult" tendo essa lista aqui
            return View(list);


            // Nota muito IMPORTANTE : Resumindo o que se fez nesta função;

            // Eu chamei o controlador(Index())
            //Depois o controlador acessou o meu Model (var list = _sellerService.FindAll();)
            //Pegou o dado na lista(list) e vai encaminhar esses dados para mminha View
        }







        // 1- Vamos agora criar uma nova acção:
        //  2- Agora Vamos ter que tambem atualizar o nosso metodo create, que é o metodo que vai abrir para nos o formulario para registrar um vendedor
        /*2.1 - Vou ter que atualizar os departamentos; */


        public async Task<IActionResult> Create()
        {
            var departments =await _departmentService.FindAllAsync(); //O metodo FindAll serve Para ele buscar do banco de dados todos os departamentos naquele DepartmentService
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



        public async Task<IActionResult> Create(Seller seller) // Como eu faço para incluir aqui um teste para testar se esse (Seller seller) é valido ou não? vou ter que incluir abaixo um "if"
        {
            if (!ModelState.IsValid) // Este teste aqui serve para testar se o modelo foi validado, se ele nao foi validado, simplesmente vou retornar a mesma View(tela) que é o create, repassando o meu objecto (seller);
            {
                var departments = await _departmentService.FindAllAsync(); // carreguei aqui os departamentos
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments }; // Aqui o atributo departments vai ser a lista dos departments que eu carreguei
                return View(viewModel);
            }


            // Agora vamos implementar aqui a accao de inserir o create(Seller seller) no banco de dados;
            await _sellerService.InsertAsync(seller); // Aqui chamamos o metodo que criamos no "SellerService", repassando este objecto(seller).
            return RedirectToAction(nameof(Index)); // Agora vou redirecionar a minha requesição para accao "Index" que é a accao que vai mostrar novamente a tela principal do meu "CRUD" de vendedores
        }




        public async Task<IActionResult> Delete(int? id) // Este delete vai ser opcional e para isso vou ter que testar
        {
            if (id == null)
            {
                // Se o id for nulo signiica que a requesição foi feita de uma forma indevida
                // return NotFound(); // Este objecto NotFound ele cria lá uma resposta básica mas depois vamos personalizar isto com uma pagina de erro


                /* Agora aqui vamos substituir o metodo NotFound por redirectToAction e vamos redirecionar para qual acção nos vamos redirecionar? para accao Error e
                    essa acção Error recebe um argumento que é a mensagem, para pssar esse argumento, eu vou criar aqui um objecto anonimo, entao irei fazer um "new"
                     e depois colocar a mensagem que eu quiser.
                      No caso de eu deletar e o "id" não existir, irei colocar "id not found"*/
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }








            // Proximo passo é pegar quem é este objecto que eu estou mandando deletar
            var obj =await _sellerService.FindByIdAsync(id.Value); // Pronto, busquei do banco de dados 
            if (obj == null)// Agora esse id pode ser um id que nao existe, se nao exisir o meu FindById torna Nullo
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }






            // Agora se tudo deu certo eu vou mandar o meu metodo retornar uma view passsando esse objecto como argumento
            return View(obj);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e) // Se acontecer uma excepção de integrityException eu vou redericionar para pagina de erro com a msg dessa excepção.
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
           
        }





        // Vamos criar aqui uma acccao "Details"
        // Essa accao vai receber um Id opcional
        /* A logica aqui vai ser identica á logica do Remove, porque vou ter que verificar se o "Id" é null,
           depois buscar o meu objecto se ele for null, terei que tambem dar um "NotFound" e depois eu irei retornar o objecto*/

        public async Task<IActionResult> Details(int? id) // o ? quer dizer que é um "id" opcional
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj =await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }



        // Agora vamos criar uma acção "Edit" serve para abrir a tela para editar o nosso vendedor e recebe um id como argumento e o opcional é só para evitar de acontecer algum erro de execução porque na verdade o id é obrigatorio

        public async Task<IActionResult> Edit(int? id )
        {
            if( id == null) // Aqui testei se o "id" nao existe
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }



            // Agor é testar se realmente esse "id" existe ou nao no nosso Banco de dados

            var obj =await _sellerService.FindByIdAsync(id.Value); // passando este "id" como argumento.
            if (obj == null) // Agora vou testar se obj que eu tentei buscar for igual a null é pk o "id" não existia no banco de dados, entao tabem irei mandar retornar o NotFound
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }



            // se as anteriores condições passarem entao é porque vou ter que abrir uma tela de edição e par aisso vou ter que carregar os departamentos para povoar a minha caixa de eleição

            List<Department> departments =await _departmentService.FindAllAsync();

            //Agora vou criar um objecto do tipo SellerFormViewModel e depois vou passar os dados para ele, o "Seller agora vai ser iniciado com o "obj" que é o objecto(na linha acima " var obj= _sellerService.FindById(id.Value); " que buscamos no banco de dados.
            // O Departments vai ser igual a esta lista de departments que acabei de carregar

            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments }; // Instanciamos o nosso ViewModel e entao agora vamos retornar uma view passando esse "ViewModel" como argumento

            return View(viewModel);
        }




        /* Criar a accao "Edit" para o metodo POST*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {

            if (!ModelState.IsValid) // Este teste aqui serve para testar se o modelo foi validado, se ele nao foi validado, simplesmente vou retornar a mesma View(tela) que é o create, repassando o meu objecto (seller);
            {
                var departments =await _departmentService.FindAllAsync(); // carreguei aqui os departamentos
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments }; // Aqui o atributo departments vai ser a lista dos departments que eu carreguei
                return View(viewModel);
            }



            if (id != seller.Id) /* Se o "id" que vem aqui no parametro do metdodo for diferente do seller.id significa que alguma coisa está errada.
                                    O "id" do vendedor que eu estou atualizando não pode ser diferente do "id" da URL da requesição, se isso acontecer irei chamar o "return BadRequest"*/
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try // a chamada está sendo colocada dentro de um "try"
            {
               await _sellerService.UpdateAsync(seller); // Aqui a chamada do update ela pode lançar excepções, tanto um NotFoundException quanto um DbConcurrencyException
                return RedirectToAction(nameof(Index));
            }

            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            // Se acontecer tambem aquela excepção abaixo eu irei dar aqui provisoriamente um return BadRequest
            


           
        }
        /* Essa acção é para retornar o "ViewError*/

        public IActionResult Error(string message)
        {
            /*A primeira coisa a fazer é instanciar um viewModel que vai ser um new ErrorViewModel(aquela classe que nós mexemos) nesse ViewModel, o atributo message dele vai ser essa 
              mensagem que deu como argumento*/

            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier/*E quem vai ser o meu RequestId da minha requesiçao? vou ter que chamar a classe activity... isto depois é para pegar o "id" interno da requesição*/
            };

            // Agora já criamos o nosso objecto viewModel, vamos fazer um return

            return View(viewModel);
        }

    }
}
