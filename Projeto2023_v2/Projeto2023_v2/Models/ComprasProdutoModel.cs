using AutoMapper;
using Repositorio;
using Repositorio.contexto;
using Repositorio.entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto2023_v2.Models
{
    public class ComprasProdutoModel
    {
        public int id { get; set; }
        public int qtde { get; set; }
        public Decimal valor { get; set; }

        public int idCompra { get; set; }
        public int idProduto { get; set; }


  
        public virtual ProdutoModel produto { get; set; }


        public ComprasProdutoModel salvar(ComprasProdutoModel model)
        {

            //Categoria cat = new Categoria();
            //cat.id = model.id;
            //cat.descricao = model.descricao;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            ComprasProdutos cat = mapper.Map<ComprasProdutos>(model);

            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                ProdutoComprasRepositorio repositorio =
                new ProdutoComprasRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;


        }

        public ComprasProdutoModel selecionar(int id)
        {
            ComprasProdutoModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                ProdutoComprasRepositorio repositorio =
                    new ProdutoComprasRepositorio(contexto);
                //select * from categoria c where c.id = id
                ComprasProdutos cat = repositorio.Recuperar(c => c.id == id);
                model = mapper.Map<ComprasProdutoModel>(cat);
                model.produto = new ProdutoModel().selecionar(cat.idProduto);
            }
            return model;
        }

        public void excluir(int id)
        {

            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                ProdutoComprasRepositorio repositorio =
                    new ProdutoComprasRepositorio(contexto);
                ComprasProdutos cat = repositorio.Recuperar(c => c.id == id);
                repositorio.Excluir(cat);
                contexto.SaveChanges();
            }
        }


        public List<ComprasProdutoModel> listar(int idcompras)
        {
            List<ComprasProdutoModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                ProdutoComprasRepositorio repositorio =
                    new ProdutoComprasRepositorio(contexto);
                List<ComprasProdutos> lista = repositorio.Listar(c=>c.idCompra==idcompras);
                listamodel = mapper.Map<List<ComprasProdutoModel>>(lista);
                foreach (var item in listamodel)
                {
                    item.produto = new ProdutoModel().selecionar(item.idProduto);
                }
            }

            return listamodel;
        }

    }
}
