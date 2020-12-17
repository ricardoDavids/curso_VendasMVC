using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas_MVC.Models;
using Vendas_MVC.Data;

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








        /* AGORA VAMOS CRIAR UM METODO PARA RETORNAR TODOS OS DEPARTAMENTOS e o nome do metodo vai ser "FindAll"
           Aqui foi chamada a funcao do LINQ, que é o "Orderby" e assim vou retornar a minha lista ordenada por nome e assim tds os depart. ficam ordenados*/


        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }


        //Nota: Foi criado o nosso servico de Departamentos com um metodo para retornar os departamentos ordenados
    }
}
