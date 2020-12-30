using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Vendas_MVC.Data;
using Vendas_MVC.Services;

namespace Vendas_MVC.Services
{
    public class SalesRecordService  /* Em 1º lugar vamos ter que declarar aqui uma dependencia para o context do EntityFramework */
    {
        private readonly Vendas_MVCContext _context;

        public SalesRecordService(Vendas_MVCContext context)
        {
            _context = context;
        }


        // Agora vamos criar a operação FindByDate

        /* Aqui vai retornar List de SalesRecord e o nome da operação FindByDate(esta operação vai receber as duas datas, a minima e a data maxima).
          Os pontos de interrogação quer dizer que os argumentos serão opcionais */
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) // Vamos implementar agora a logica para encontrar as vendas nesse intervalo de datas
        {



            // Aqui embaixo vou ter um objecto inicial de consulta, lembrando que essa consulta nao é executavel pela simples definição dela
            // E entao este objecto "result" que já foi construido com o Linq, eu posso acrescentar mais operações
            var result = from obj in _context.SalesRecord select obj;  /* Esta declaração aqui vai pegar este SalesRecord que é do tipo dbset e construir para um objecto result do tipo Iqueryable.
                                                                     Em cima desse objecto eu vou poder acrescentar outros detalhes da minha consulta*/





            if (minDate.HasValue)   /* Por exemplo, este teste é , se a data minima "HasValue",ou seja, eu informei uma data minima,
                                     nesse caso o que irei fazer é acrescentar uma restrição de data minima da minha consulta.
                                       vou ter que com a função lambda "Where" expresse a minha restrição de data, ou seja, eu quero objecto x, tal que date seja maior ou igual ao data minima. valor*/
            {


                result = result.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }



            /* E agora para executar a minha consulta eu poderia apenas fazer como escrevo embaixo:*/


            //  return result.ToList(); 


            /* Isso ai em cima executa a consulta para mim e devolve o resultado na forma de Lista;

             Só que eu ainda vou acrescentar outras coisas aqui, vou fazer um Join com a tabela de vendedor e com a tabela de departamentos e depois ainda vou
               ordenar decrescentemente por data;

             Tudo isso vai ser feito com LINQ  e aquela funcção Include do EntityFramework*/


            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
    }
}
