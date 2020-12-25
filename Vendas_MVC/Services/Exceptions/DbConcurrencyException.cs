using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendas_MVC.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException /* A minha classe NotFoundException vai herdar do ApplicationException e depois vou criar um constructor;
                                                              Agora em baixo ira ser criado um constructor recebendo uma mensagem, e depois simplesmente esse mesmo constructor irá repassar essa chamada para a classe base  */
    {
        public DbConcurrencyException(string message) : base(message)
        {

        }

    }
}
