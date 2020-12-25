using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vendas_MVC.Services.Exceptions
{
    public class NotFoundException : ApplicationException /* A minha classe NotFoundException vai herdar do ApplicationException e depois vou criar um constructor;
                                                              Agora em baixo ira ser criado um constructor recebendo uma mensagem, e depois simplesmente esse mesmo constructor irá repassar essa chamada para a classe base  */

    {// Aqui é feita excepção personalizada "NotFound"
        public NotFoundException(string message) : base(message)
        { 

        }

        /*Nota: 
           Porque estamos criando uma excepção personlizada?
           é pk nos vamos querer ter excepções especificas da nossa camada de serviços 
           Quando temos uma excepção personalizada, nós temos a possibilidade de tratar exclusivamente essa excepção;
           Então irás ter um controle maior de como tratar cada tipo de excepção que pode ocorrer */

    }
}
