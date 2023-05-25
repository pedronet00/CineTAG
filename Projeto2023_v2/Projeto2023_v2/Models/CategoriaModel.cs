using AutoMapper;
using Repositorio;
using Repositorio.contexto;
using Repositorio.entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto2023_v2.Models
{
    public class CategoriaModel
    {
        [Display(Name = "Código")]
        public int id { get; set; }

        [Required(ErrorMessage ="Campo obrigatório!")]
        [MaxLength(150,
            ErrorMessage ="Descrição deve ter no máximo 150 caracteres")]
        [MinLength(3,
            ErrorMessage ="Descrição deve ter no mínimo 3 caracteres")]
        [Display(Name ="Descrição")]
        public String descricao { get; set; }



        public CategoriaModel salvar(CategoriaModel model) {

            //Categoria cat = new Categoria();
            //cat.id = model.id;
            //cat.descricao = model.descricao;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Categoria cat = mapper.Map<Categoria>(model);

            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                CategoriaRepositorio repositorio =
                new CategoriaRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;
            
        
        }


        public List<CategoriaModel> listar() {
            List<CategoriaModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                CategoriaRepositorio repositorio =
                    new CategoriaRepositorio(contexto);
                List<Categoria> lista= repositorio.ListarTodos();
                listamodel = mapper.Map<List<CategoriaModel>>(lista);
            }
            
            return listamodel;
        }

        public CategoriaModel selecionar(int id) {
            CategoriaModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                CategoriaRepositorio repositorio =
                    new CategoriaRepositorio(contexto);
                //select * from categoria c where c.id = id
                Categoria cat= repositorio.Recuperar(c=>c.id==id);
                model = mapper.Map<CategoriaModel>(cat);
            }
            return model;
        }

        public void excluir(int id) {

            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                CategoriaRepositorio repositorio = 
                    new CategoriaRepositorio(contexto);
                Categoria cat = repositorio.Recuperar(c => c.id == id);
                repositorio.Excluir(cat);
                contexto.SaveChanges();
            }
        }
    }
}
