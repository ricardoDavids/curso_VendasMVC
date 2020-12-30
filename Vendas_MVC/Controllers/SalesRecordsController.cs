using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas_MVC.Services;

namespace Vendas_MVC.Controllers
{
    public class SalesRecordsController : Controller
    {

        /* Para que eu possa utilizar o meu serviço dentro do Controllador eu terei que declarar a dependencia do serviço aqui: */
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }






        // ACTUALIZAR ACÇÃO

        /* Agora na minha accao "sIMPLEsEARCH" eu vou chamar o metodo FindByDate do meu Serviço */
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            /* Tenho que agora testar para as datas aparecerem na tela quando eu escolher, entao, se a minha data minima ela possui um valor? Se ela nao possuir um valor, eu vou atribuir um valor
             padrão para ela, por exemplo dia 1º de janeiro do ano actual, vamos supor se o cara nao informar a data minima eu quero mostrar por padrao as vendas a apartir do inicio do ano*/

            // Neste se não possui um vaor minimo, esse valor minimo vai receber o dia 1º de jan do ano actual
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            /*Feito isto dos "If´s" eu vou passar estes dados minDate e maxDate lá para minha "View"
               No caso aqui eu vou passar estes dados aqui utilizando o dicionario VIewData*/

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd"); // ViewData maxDate vai receber maxDate, isto para funcionar View "SimpleSearch"






            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);

            return View(result); // Aqui estamos a mandar a lista como resultado da minha accao 
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
