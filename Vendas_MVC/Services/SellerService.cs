using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas_MVC.Models;
using Vendas_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Vendas_MVC.Services.Exceptions;


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

        public async Task<List<Seller>> FindAllAsync()

        {
            // uma operação para retornar do banco de dados tds os vendedores, basta fazer o seguinte:
            return await _context.Seller.ToListAsync(); // Isto aqui vai acessar a minha fonte de dados relacionado com a tabela de vendedores e converter isso para uma lista


            // NOta: por enquanto isto vai ser uma operação sincrona -> ou seja, _context.Seller.ToList vai rodar o acesso ao banco de dados e a aplicação vai ficar bloqueada esperando essa operação terminar

        }





        public async Task InsertAsync(Seller obj) // Aqui criamos um metodo para inserir um novo vendedor no banco de dados
        {
         //   obj.Department = _context.Department.First(); // solução provisória só para nao dar erro quando tentar inserir mais um vendedor por causa do DepartmentId que está acusando null na db e assim vai buscar o DepartmentId=1
            _context.Add(obj);
           await _context.SaveChangesAsync(); // Esta operação é que vai aceder ao banco de dados, entao nela é que deve ter a versao assincrona(async)
        }






        public async Task<Seller> FindByIdAsync(int id) // Vai ter que retornar o vendedor que possui esse id que está dentro parenteses nesta linha

        {
            //CHAMAMOS a operação LINQ FirstOrDefault daquele objecto obj cujo obj.id seja igual ao id que eu estou informando como parametro a seguir a funcao "FindById"
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id); /* Isto aqui por padrao carrega simplesmente o "Id" do Vendedor
                                                                            Para carregar o departamento junto, eu vou ter que dar uma instrução
                                                                             especifica aqui para o meu EntityFramework para ele fazer o Join das tabelas,
                                                                              ou seja, para ele buscar tambem o departamento.
                                                                               vou mostrar em baixo como era antes sem a inclusão do departamento*/



            // return _context.Seller.FirstOrDefault(obj => obj.Id == id); // Sem inclusão do departamento quando viamos na tela os detalhes, nao mostrava qual o departamento associado
        }







        public async Task RemoveAsync(int id)
        {

            /* Vou fazer aqui uma implementação da seguinte maneira: 1 vou pegar o objecto (obj) chamando o _context.Seller.Find passando o id para dentro do parenteses */
            var obj =await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj); // Com o objecto na mao eu vou chamar o _context.Seller. remove do nosso DbSet, entao vou falar para remover esse objecto (obj) do dbSet 
            await _context.SaveChangesAsync(); // Agora tenho que confirmar essa alteração para o EntityFramework efectiva-la lá no db
        }












        // Agora vamos incluir uma operação "Update" , no caso um objecto do tipo "Seller"
        public async Task UpdateAsync(Seller obj) /* o que é que vai ser "Update" um objecto do tipo Seller?
                                          A primeira coisa que vou ter que fazer é testar se esse "id" desse objecto já existe no banco.
                                           Porque como estou atualizando o "id" desse objecto, já tem que existir*/

            /* Para fazer isto eu vou ter que chamar o meu _context.Seller.Any ( o any serve para falar se existe algum registo no banco de dados com a condição que voçe irá colocar dentro dos ()
                x que leva em x.id igual ao obj.Id.
                 Então eu estou testando se ja existe no banco de dados algum vendedor x cujo o "id" seja igual ao "id" do meu objecto*/



        {
            // Aqui o bool hasAny --> quer se tem algum e depois receberá a chamada
            bool hasAny =await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny) // Se nao existir vou ter que lançar uma excepção
            {
                throw new NotFoundException(" I did not found");
            }
            // Se passar por esse If é porque já existe esse objecto la na bd e irei apenas atualizar;

            // Entao agora para atualizar e salvar a atualização vou fazer um bloco try e depois um catch para capturar _context.Update(obj) e depois save


            /* Mas agora aqui tem um problema quando voçe chama a operaç~ºao atualizar no banco de dados, este pode retornar uma excepção de conflito de concorrencia,
                Se esse erro ocorrer no banco de dados, o Entity Framework vai produzir uma excepção chamada dbupdate, entao iremos colocar um bloco try  e um catch para capturar
                 uma possivel excepção de concorrencia do banco de dados.
                  No caso se acontecer ocorrer a tal excepção de concorrencia de dados ai vou ter que lançar uma nova excepcao de serviços e ai irei colocar uma "messagem" que veio do banco de dados*/
            try
            {
                _context.Update(obj);
               await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
            
            // Fuck





            // Nota: Conclusão

            /* O que é que eu estou fazendo com o try e  catch?
                Eu estou interceptando uma excepção no caso do "catch" do nivel de acessos a dados e depois estou relançando essa excepção só que lançando essa minha excepção a nivel de serviços
            
             A minha camada de serviço não vai propagar, difundir, uma excepção do nivel de acesso a dados, se uma excepção de nivel de acesso a dados acontecer, a minha camada de serviços ela vai lançar
             uma excepção da camada dela. e ai o meu controlador que no caso vai ser o "SellerController", ele vai ter que lidar só com as excepçõs da camada de serviços
            



             Nota muito importante:
             
             O controlador conversa com a camada de serviços, excepções de nivel de acesso a dados são capturadas pelo o serviço e relançadas nas formas de excepções de serviço para o controlador*/
        }
    }
}
