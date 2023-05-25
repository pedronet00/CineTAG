using Microsoft.AspNetCore.Mvc;
using Projeto2023_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto2023_v2.Controllers
{
    public class HomeSiteController : Controller
    {
        public IActionResult Index()
        {
            ProdutoModel model = new ProdutoModel();
            return View(model.listar());
        }


        public IActionResult detalhes(int id)
        {
            ProdutoModel model = new ProdutoModel();
            ProdutoModel produto= model.selecionar(id);
            return View(produto);
        }
    }
}
