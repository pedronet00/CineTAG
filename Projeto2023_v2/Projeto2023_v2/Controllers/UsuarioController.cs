using Microsoft.AspNetCore.Mvc;
using Projeto2023_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Projeto2023_v2.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult login()
        {

            return View();
        }

        public IActionResult cadastro(int? mostraMensagem)
        {
            if (mostraMensagem.HasValue)
            {
                ViewBag.mensagem = "Dados salvos com sucesso!";
                ViewBag.classe = "alert-success";
            }
            return View(new UsuarioModel());
        }
        [HttpPost]
        public IActionResult salvar(UsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UsuarioModel catmodel = new UsuarioModel();
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
            UsuarioModel catModel = new UsuarioModel();
            List<UsuarioModel> lista = catModel.listar();
            return View(lista);//lista por parametro para a view
        }


        public IActionResult prealterar(int id)
        {
            UsuarioModel model = new UsuarioModel();
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            UsuarioModel model = new UsuarioModel();
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

        [HttpPost]
        public IActionResult logar(String txtemail,
            String txtsenha)
        {
            UsuarioModel model =
                (new UsuarioModel()).validarLogin(txtemail, txtsenha);
            if (model == null)
            {
                //não encontrou 
                ViewBag.mensagem = "Dados inválidos";
                ViewBag.classe = "alert-danger";
                return View("login");
            }
            else
            {
                //encontrou
                //inseriu na sessão
                HttpContext.Session.SetInt32("idUsuario", model.id);
                HttpContext.Session.SetString("nomeUsuario", model.nome);
                if (Convert.ToString(model.PerfilEnum) == "admin")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "HomeSite");
                }
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult sair()
        {
            //limpar a sessão
            HttpContext.Session.Remove("nomeUsuario");
            HttpContext.Session.Remove("idUsuario");
            HttpContext.Session.Clear();

            //redirecionar para login
            return RedirectToAction("login", "Usuario");
        }

    }


}