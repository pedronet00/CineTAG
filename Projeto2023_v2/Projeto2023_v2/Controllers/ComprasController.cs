using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto2023_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto2023_v2.Controllers
{
    public class ComprasController : Controller
    {
        public IActionResult Index()
        {
            List<ComprasProdutoModel> lista = new List<ComprasProdutoModel>();
            int? idCompra=      
            HttpContext.Session.GetInt32("idCompra");
            if(idCompra.HasValue)
              lista = new ComprasProdutoModel().listar(idCompra.Value);
            return View(lista);//lista por parametro para a view
   
        }

        public IActionResult Finalizar()
        {
            //retornar uma view com os dados da compra depois criar outro método para finalizar com geração de pagamento


            //alterar o status da venda para aguardando pagamento

            int idCompra = HttpContext.Session.GetInt32("idCompra").Value;

            ComprasModel compras = new ComprasModel().selecionar(idCompra);
            compras.idStatus = 3; // aguardando pagamento
            var produtos = (new ComprasProdutoModel()).listar(idCompra);
            decimal total = 0;
            foreach(var item in produtos)
            {
                total += item.valor;
            }

            compras.valor = total;
            //compras.idcliente = HttpContext.Session.GetInt32("idCliente").Value;
            


            //gerar o pagamento

           /* ClienteModel cliente = new ClienteModel().selecionar(HttpContext.Session.GetInt32("idCliente").Value);

            new ComprasModel().gerarPagamentoMercadoPago(new MercadoPagoModel() {
            
                email = cliente.email,
                cidade = cliente.cidade,
                cep = cliente.cep,
                estado = cliente.estado,
                idPagamento = idCompra,
                logradouro = cliente.logradouro,
                nome = cliente.nome,
                nomePlano = "Venda Ingresso CineTAG",
                numero = cliente.numero,
                telefone = cliente.telefone,
                valor = total
            
            });*/

            Task<RetornoMercadoPago> retorno = new ComprasModel().gerarPagamentoMercadoPago(new MercadoPagoModel()
            {

                email = "teste@gmail.com",
                cidade = "Presidente Prudente",
                cep = "19023060",
                estado = "SP",
                idPagamento = idCompra,
                logradouro = "Rua Garcia Paes",
                nome = "Cliente Teste",
                nomePlano = "Venda Ingresso CineTAG",
                numero = "20",
                telefone = "18991667210",
                valor = total

            });

            if(retorno.Result.status == "SUCESSO")
            {
                compras.idPreferencia = retorno.Result.idPreferencia;
                compras.url = retorno.Result.url;
            }
            else
            {
                compras.idStatus = 4; //cancelada
            }

            new ComprasModel().salvar(compras);

            //redirecionar o usuário
            return Redirect(retorno.Result.url);


        }

        public virtual JsonResult alterarQtde(int id, int qtde)
        {
            ComprasProdutoModel prod = (new ComprasProdutoModel()).selecionar(id);
            prod.qtde = qtde;
            prod.valor = (new ProdutoModel()).selecionar(prod.idProduto).valor * prod.qtde;
            (new ComprasProdutoModel()).salvar(prod);
            prod = (new ComprasProdutoModel()).selecionar(id);
            return new JsonResult(prod);
        }


        //
        public IActionResult excluirProduto(int id)
        {
            ComprasProdutoModel model = new ComprasProdutoModel();
            try
            {

                model.excluir(id);
               
            }
            catch (Exception ex)
            {

                ViewBag.mensagem = "Ops... Não foi possível excluir!" + ex.Message;
                ViewBag.classe = "alert-danger";
            }

            return RedirectToAction("Index");
        }

        public IActionResult adiciona(int id) {

            int? idCompra =
            HttpContext.Session.GetInt32("idCompra");
            if (!idCompra.HasValue)
            {
                ComprasModel commpras = new ComprasModel() {
                    idStatus = 1,
                    dataCadastro=DateTime.Now
                };
                commpras = (new ComprasModel()).salvar(commpras);
                idCompra = commpras.id;
                HttpContext.Session.SetInt32("idCompra",idCompra.Value);
            }
            ProdutoModel prod = new ProdutoModel().selecionar(id);
            ComprasProdutoModel comprasProd =   new ComprasProdutoModel() { 
            idCompra = idCompra.Value,
            idProduto = id,
            qtde = 1,
            valor = prod.valor
            };
            (new ComprasProdutoModel()).salvar(comprasProd);
            return RedirectToAction("Index");
        }
    }
}
