using System;

namespace Vendas_MVC.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string  Message { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); /* A fun��o vai retornar se ele n�o for nulo ou vazio */
    }
}
