using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas_MVC.Models;
using Vendas_MVC.Data;


namespace Vendas_MVC.Services
{   
    
    
    // Nota: Ler abaixo
    /* A nossa classe SellerService vai ter que ter uma dependencia para o nosso dbContext que é a nossa Classe "Vendas_MVCContext, entao teremos que declarar uma dependencia para "Vendas_MVCContext"  */

    public class SellerService /* Agora vamos ter que implementar uma operação FindAll no nosso serviço, essa operação vai retornar uma lista com todos os vendedores */
    {

        // o readonly é par prevenir que essa dependencia possa ser alterada
        private readonly Vendas_MVCContext _context;






        // Agora vamos criar aqui um constructor para que a injecao de dependencia possa ocorrer:
        public SellerService(Vendas_MVCContext context)
        {
            // e ai esse meu constructor vai atribuir para o meu _context o context que veio como argumento

            _context = context;
        }




        // Implementar a nossa Operação FindAll para retornar uma lista com todos os vendedores do banco de dados

        public List<Seller> FindAll()

        {
            // uma operação para retornar do banco de dados tds os vendedores, basta fazer o seguinte:
            return _context.Seller.ToList(); // Isto aqui vai acessar a minha fonte de dados relacionado com a tabela de vendedores e converter isso para uma lista


            // NOta: por enquanto isto vai ser uma operação sincrona -> ou seja, _context.Seller.ToList vai rodar o acesso ao banco de dados e a aplicação vai ficar bloqueada esperando essa operação terminar

        }

        public void Insert(Seller obj) // Aqui criamos um metodo para inserir um novo vendedor no banco de dados
        {
         //   obj.Department = _context.Department.First(); // solução provisória só para nao dar erro quando tentar inserir mais um vendedor por causa do DepartmentId que está acusando null na db e assim vai buscar o DepartmentId=1
            _context.Add(obj);
            _context.SaveChanges();
        }



        public Seller FindById(int id) // Vai ter que retornar o vendedor que possui esse id que está dentro parenteses nesta linha
        {
            //CHAMAMOS a operação LINQ FirstOrDefault daquele objecto obj cujo obj.id seja igual ao id que eu estou informando como parametro a seguir a funcao "FindById"
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {

            /* Vou fazer aqui uma implementação da seguinte maneira: 1 vou pegar o objecto (obj) chamando o _context.Seller.Find passando o id para dentro do parenteses */
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj); // Com o objecto na mao eu vou chamar o _context.Seller. remove do nosso DbSet, entao vou falar para remover esse objecto (obj) do dbSet 
            _context.SaveChanges(); // Agora tenho que confirmar essa alteração para o EntityFramework efectiva-la lá no db
        }
    }
}
