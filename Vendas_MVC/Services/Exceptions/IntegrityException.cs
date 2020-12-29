using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendas_MVC.Services.Exceptions
{ 
    // vamos fazer aqui a implementação dda excepção personalizada 
    public class IntegrityException : ApplicationException // a classe Integrityexception vai herdar da ApplicationException
    {
        // Agora aqui criamos um constructor recebendo um string message e depois vou simplesmente repassar essa chamada para a Superclasse

        // Esta aqui vai ser a nossa excepção personalizada de serviço para erros de integridade referencial
        public IntegrityException(string message) : base(message )
        {

        }
    }
}
