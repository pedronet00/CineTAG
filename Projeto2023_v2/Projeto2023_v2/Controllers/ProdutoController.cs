using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projeto2023_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto2023_v2.Controllers
{
    public class ProdutoController : Controller
    {

        private readonly IWebHostEnvironment webHostEnvironment;
        public ProdutoController(IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cadastro() {
            List<CategoriaModel> lista = (new CategoriaModel()).listar();
            ViewBag.listacategorias = lista.Select(c=>new SelectListItem() { 
               Value = c.id.ToString(), Text = c.descricao
            });
            return View(new ProdutoModel());
        }


       

        [HttpPost]
        public IActionResult salvar(ProdutoModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    ProdutoModel catmodel = new ProdutoModel();
                    catmodel.salvar(model, webHostEnvironment);
                    ViewBag.mensagem = "Dados salvos com sucesso!";
                    ViewBag.classe = "alert-success";
                }
                catch (Exception ex)
                {

                    ViewBag.mensagem = "ops... Erro ao salvar!" + ex.Message + "/" + ex.InnerException;
                    ViewBag.classe = "alert-danger";
                }
            }
            else
            {
                ViewBag.mensagem = "ops... Erro ao salvar! verifique os campos";
                ViewBag.classe = "alert-danger";

            }

            List<CategoriaModel> lista = (new CategoriaModel()).listar();
            ViewBag.listacategorias = lista.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });

            return View("cadastro", model);
        }


        public IActionResult listar()
        {
            ProdutoModel catModel = new ProdutoModel();
            List<ProdutoModel> lista = catModel.listar();
            return View(lista);//lista por parametro para a view
        }


        public IActionResult prealterar(int id)
        {
            ProdutoModel model = new ProdutoModel();
            List<CategoriaModel> lista = (new CategoriaModel()).listar();
            ViewBag.listacategorias = lista.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            ProdutoModel model = new ProdutoModel();
            try
            {

                model.excluir(id);
                ViewBag.mensagem = "Dados excluidos com sucesso!";
                ViewBag.classe = "alert-success";
            }
            catch (Exception ex)
            {

                ViewBag.mensagem = "Ops... Não foi possível excluir!" + ex.Message;
                ViewBag.classe = "alert-danger";
            }

            return View("listar", model.listar());
        }
    }
}
