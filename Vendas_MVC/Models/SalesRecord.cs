using System;
using Vendas_MVC.Models.Enums;

namespace Vendas_MVC.Models
{
    public class SalesRecord
    {
        //Atributos básicos como por exemplo Id

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public SalesStatus Status { get; set; }
        public Seller Seller { get; set; } // Aqui nesta classe uma venda possui um vendedor

        public SalesRecord()
        {

        }

        public SalesRecord(int id,DateTime date, double amount, SalesStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = Seller;
        }
    }

}
