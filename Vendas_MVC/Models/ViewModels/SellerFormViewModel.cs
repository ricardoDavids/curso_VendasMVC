using System;
using System.Collections.Generic;


namespace Vendas_MVC.Models.ViewModels
{
    public class SellerFormViewModel
    {

        // Quais serao os dados necessarios para uma tela de registro de vendedor?
        /* Vai precisar de um vendedor, e de uma lista de departam. para que possa criar a caixa e selcionar o depart do vendedor */
        public  Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
