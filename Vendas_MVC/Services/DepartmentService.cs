using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas_MVC.Models;
using Vendas_MVC.Data;
using Microsoft.EntityFrameworkCore;

namespace Vendas_MVC.Services
{

    // Nota: Ler abaixo
    /* A nossa classe DepartmentService vai ter que ter uma dependencia para o nosso dbContext que é a nossa Classe "Vendas_MVCContext que está na pasta "DATA",
       entao teremos que declarar uma dependencia para "Vendas_MVCContext"  */
    public class DepartmentService
    {
        // o readonly é par prevenir que essa dependencia possa ser alterada
        private readonly Vendas_MVCContext _context;






        public DepartmentService(Vendas_MVCContext context)
        {
            _context = context;
        }





        //-----------------------------------------------------------------------------------------------------------------------------------------------------------


        /* AGORA VAMOS CRIAR UM METODO PARA RETORNAR TODOS OS DEPARTAMENTOS e o nome do metodo vai ser "FindAll"
           Aqui foi chamada a funcao do LINQ, que é o "Orderby" e assim vou retornar a minha lista ordenada por nome e assim tds os depart. ficam ordenados*/



        /*
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
        */

        //Nota: Foi criado o nosso servico de Departamentos com um metodo para retornar os departamentos ordenados

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------


        /* Nota: Importante ler para perceber -> isto refere se ai videoaula nº264
          
         Então agora para que aplicação corra mais rapido de forma assincrono, ou seja, que não seja em simultaneo.
        
         Entao para converter a operação que está em cima para uma operação assincrona, vou mostrar abaixo
        
         */


        public async Task<List<Department>> FindAllAsync() // Antes retornava originalmente uma list de departments mas agora vai retornar um "Task" de list<Department>
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync(); // a palavra "await" é para avisar o compilador que esta chamada vai ser assincrona
        }





    }
}
