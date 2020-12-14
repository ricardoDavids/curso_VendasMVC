using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Vendas_MVC.Models
{

    // Atributos básicos
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; } // criamos uma propriedade department com o nome da propriedade department

        // proxima etapa associar o vendedor com as vendas(dentro do vendedor vou ter um ICOLLECTION DE SALES RECORD)
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); // Aqui está a nossa associação para muitos, ou seja um vendedor pode fazer varias vendas(Sales)

        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        //Metodos: Add e Remover o vendedor  e tamb a operação que calcula o total de vendas desse vendedor no periodo informado

        // Nota Importante: "Sales" --> é a nossa lista de vendas 

        //Add uma venda na nossa lista de vendas, esse metodo vai receber como argumento "SalesRecord" e o que ele faz?
        // Vai vir na minha lista Sales e depois vou chamar a minha operação Add. recebendo (sr) 

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final) // Esta operação vai ter que me retornar o total de Vendas deste vendedor no periodo dentro de parenteses
            
        {
            /*Vamos utilizar o link, vamos chamar a coleção Sales, que é a lista de vendas associada com esse vendedor, entao apartir dessa lista, eu vou chamar as operações do link.
              A seguir ao Sales vou ter que filtrar a minha lista de vendas para obter uma nova lista contendo apenas as vendas nesse intervalo de datas e para filtrar vou chamar a operação "where"
              A seguir ao "where" vou colocar uma expressão lambda para filtrar as minhas vendas, entao eu vou colocar todo o objecto "sr" tal que sr.Date seja >= a minha data inicial e ... entao feita essa filtragem,
              Eu vou mandar calcular a soma baseada em outra expressão lambda. Eu vou querer a soma de quê? vou querer a soma de sr que leva em sr.Amount, porque eu quero a soma das vendas, entao a minha soma vai ser baseada no atributo Amount  */
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
