using System;

namespace Vendas_MVC.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string  Message { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); /* A função vai retornar se ele não for nulo ou vazio */
    }
}
