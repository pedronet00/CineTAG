using Microsoft.AspNetCore.Mvc;
using Projeto2023_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto2023_v2.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cadastro(int? mostraMensagem) {
            if (mostraMensagem.HasValue)
            {
                ViewBag.mensagem = "Dados salvos com sucesso!";
                ViewBag.classe = "alert-success";
            }
            return View(new CategoriaModel());
        }
        [HttpPost]
        public IActionResult salvar(CategoriaModel model) {
            if (ModelState.IsValid)
            {
                try
                {
                    CategoriaModel catmodel = new CategoriaModel();
                    catmodel.salvar(model);
                    return RedirectToAction("cadastro", new { mostraMensagem = 1 });
                }
                catch (Exception ex)
                {

                    ViewBag.mensagem = "ops... Erro ao salvar!" + ex.Message + "/" + ex.InnerException;
                    ViewBag.classe = "alert-danger";
                    return View("cadastro", model);
                }
            }
            else
            {
                ViewBag.mensagem = "ops... Erro ao salvar! verifique os campos";
                ViewBag.classe = "alert-danger";
                return View("cadastro", model);
            }

           
        }


        public IActionResult listar()
        {
            CategoriaModel catModel = new CategoriaModel();
            List<CategoriaModel> lista = catModel.listar();
            return View(lista);//lista por parametro para a view
        }


        public IActionResult prealterar(int id) {
            CategoriaModel model = new CategoriaModel();
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id) {
            CategoriaModel model = new CategoriaModel();
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
