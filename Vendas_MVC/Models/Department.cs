using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Vendas_MVC.Models
{
    public class Department
    {

        public int Id { get; set; }
        public string Name { get; set; }

       // Nota: Se eu quisesse acrecentar aqui mais um atributo?  public string Rating { get; set; } // para apagar
        

        // Instanciar varios vendedores, porque o departamento tem varios vendedores, é necessario fazer essa composição

        public ICollection<Seller> Sellers { get; set; } = new List<Seller>(); /* e agora já vou criar essa coleção só para garantir que a aminha lista fica criada
                                                                                  Aqui acabamos de implementar o "Department" com o <"Seller">  Sellers --> é uma propriedade*/
                                                                                // Sellers --> é tambem a minha lista de vendedores






        // Agora temos que ir na classe Seller implementar a composicção de 1 Seller para 1 departamento


        // Criação de constructores
        public Department()
        {

        }




        // Apagar o atributo rating
        public Department(int id, string name /*string rating*/) // não introduzir o atributo de collecções como no caso Sellers
        {
            Id = id;
            Name = name;
           //Se quisesse acrescentar aqui Rating = rating; 
        }




        //Adicionar um vendedor
        public void AddSeller(Seller seller) // para adicionar um vendedor
        {
            Sellers.Add(seller); // peguei a minha lista de vendedores( Sellers) e adicionei como argumento o seller
        }






        // Total de Vendas do departmento para intervalo de datas?
        // R:. Vou ter que pegar a lista de vendedores, todos os vendedores desse departamento e somar o total de vendas de cada vendedor nesse intervalo de tempo 
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));/* pegar a lista Sellers desse departamento, e depois chamar a oper. Soma, colocando nessa soma uma expressao lambda que filtra apenas as vendas nesse periodo
                                                                               Estou a chamar cada vendedor da minha lista de vendedores, chamando o total de Sales do vendedor naquele periodo e depois é feita uma soma desse resultado para todos os vendedores desse departamento  */
        }

    }
}


