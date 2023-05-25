using AutoMapper;
using Repositorio;
using Repositorio.contexto;
using Repositorio.entidades;
using Repositorio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto2023_v2.Models
{
    public class UsuarioModel
    {
        public int id { get; set; }
        public String nome { get; set; }
        public String email { get; set; }
        public String senha { get; set; }
        //0: adm e 1: normal

        public PerfilEnum PerfilEnum { get; set; }


        public UsuarioModel validarLogin(String email,
            String senha)
        {
            UsuarioModel model = null;
            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                UsuarioRepositorio repositorio =
                    new UsuarioRepositorio(contexto);
                Usuario usu = repositorio.Recuperar
                   (u => u.email == email && u.senha == senha);
                var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
                model = mapper.Map<UsuarioModel>(usu);
                //select *from usuario where email=@email and senha=@senha
            }
            return model;

        }
        public UsuarioModel salvar(UsuarioModel model)
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Usuario cat = mapper.Map<Usuario>(model);

            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                UsuarioRepositorio repositorio =
                new UsuarioRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;

        }
        public List<UsuarioModel> listar()
        {
            List<UsuarioModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                UsuarioRepositorio repositorio =
                    new UsuarioRepositorio(contexto);
                List<Usuario> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<UsuarioModel>>(lista);
            }

            return listamodel;
        }
        public UsuarioModel selecionar(int id)
        {
            UsuarioModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                UsuarioRepositorio repositorio =
                    new UsuarioRepositorio(contexto);
                Usuario cat = repositorio.Recuperar(c => c.id == id);
                model = mapper.Map<UsuarioModel>(cat);
            }
            return model;
        }
        public void excluir(int id)
        {
            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                UsuarioRepositorio repositorio =
                    new UsuarioRepositorio(contexto);
                Usuario cat = repositorio.Recuperar(c => c.id == id);
                repositorio.Excluir(cat);
                contexto.SaveChanges();
            }
        }
    }
}