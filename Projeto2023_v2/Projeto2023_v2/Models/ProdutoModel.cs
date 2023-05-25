using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Repositorio;
using Repositorio.contexto;
using Repositorio.entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto2023_v2.Models
{
    public class ProdutoModel
    {
        [Display(Name = "Código")]
        public int id { get; set; }
        public String descricao { get; set; }

        public String sinopse { get; set; }
        public DateTime dataCadastro { get; set; }
        public Decimal valor { get; set; }
        public bool ehDestaque { get; set; }
        public int idCategoria { get; set; }

        public String imagem { get; set; }

        [Required(ErrorMessage = "Imagem Obrigatória")]
        public IFormFile arquivoImagem { get; set; }

        public ProdutoModel salvar(ProdutoModel model, 
            IWebHostEnvironment webHostEnvironment)
        {

            //Categoria cat = new Categoria();
            //cat.id = model.id;
            //cat.descricao = model.descricao;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Produto cat = mapper.Map<Produto>(model);

            cat.imagem = Upload(model.arquivoImagem, 
                webHostEnvironment);

            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                ProdutoRepositorio repositorio =
                new ProdutoRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;


        }


        public List<ProdutoModel> listar()
        {
            List<ProdutoModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                ProdutoRepositorio repositorio =
                    new ProdutoRepositorio(contexto);
                List<Produto> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<ProdutoModel>>(lista);
            }

            return listamodel;
        }

        public ProdutoModel selecionar(int id)
        {
            ProdutoModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                ProdutoRepositorio repositorio =
                    new ProdutoRepositorio(contexto);
                //select * from categoria c where c.id = id
                Produto cat = repositorio.Recuperar(c => c.id == id);
                model = mapper.Map<ProdutoModel>(cat);
            }
            return model;
        }

        public void excluir(int id)
        {

            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                ProdutoRepositorio repositorio =
                    new ProdutoRepositorio(contexto);
                Produto cat = repositorio.Recuperar(c => c.id == id);
                repositorio.Excluir(cat);
                contexto.SaveChanges();
            }
        }

        private string Upload(IFormFile arquivoImagem, IWebHostEnvironment webHostEnvironment)
        {
            string nomeUnicoArquivo = null;
            if (arquivoImagem != null)
            {
                string pastaFotos = Path.Combine(webHostEnvironment.WebRootPath, "Imagens"); 
                nomeUnicoArquivo = Guid.NewGuid().ToString() + "_" + arquivoImagem.FileName; 
                string caminhoArquivo = Path.Combine(pastaFotos, nomeUnicoArquivo); 
                using (var fileStream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    arquivoImagem.CopyTo(fileStream);
                }
            }
            return nomeUnicoArquivo;
        }

    
    }
}
