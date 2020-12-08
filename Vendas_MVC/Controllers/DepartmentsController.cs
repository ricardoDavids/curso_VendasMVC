using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas_MVC.Models;

namespace Vendas_MVC.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> list = new List<Department>();
            list.Add(new Department { Id = 1, Name = "Electronics" }); // Aqui estão criado os objectos do meu controlador como por exemplo:. Name = "Electronics" 
            list.Add(new Department { Id = 2, Name = "Fashion" });// Aqui estão criado os objectos do meu controlador

            // Agora esses objectos são passados para View
            return View(list);
        }
    }
}
