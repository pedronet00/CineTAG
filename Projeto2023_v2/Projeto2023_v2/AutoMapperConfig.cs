using AutoMapper;
using Projeto2023_v2.Models;
using Repositorio.entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto2023_v2
{
    public class AutoMapperConfig : Profile
    {
        public static MapperConfiguration RegisterMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Categoria, CategoriaModel>();
                cfg.CreateMap<CategoriaModel, Categoria>();
                cfg.CreateMap<Produto, ProdutoModel>();
                cfg.CreateMap<ProdutoModel, Produto>();
                cfg.CreateMap<Usuario, UsuarioModel>();
                cfg.CreateMap<UsuarioModel, Usuario>();
                cfg.CreateMap<Compras, ComprasModel>();
                cfg.CreateMap<ComprasModel, Compras>();
                cfg.CreateMap<ComprasProdutoModel, ComprasProdutos>();
                cfg.CreateMap<ComprasProdutos, ComprasProdutoModel>();

            });

            return config;
        }
    }

}
