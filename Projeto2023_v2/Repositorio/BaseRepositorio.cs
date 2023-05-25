using Repositorio.entidades;
using Repositorio.contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repositorio
{


    public abstract class BaseRepositorio<T> where T : class
    {

        protected EmpresaContexto _contexto;

        private DbSet<T> _tabela;

        public BaseRepositorio(EmpresaContexto contexto)
        {

            this._contexto = contexto;

            this._tabela = this._contexto.Set<T>();

        }

        public virtual void Inserir(T entidade)
        {

            this._tabela.Add(entidade);

        }

        public virtual void Alterar(T entidade)
        {

            this._contexto.Entry(entidade).State = EntityState.Modified;

        }

        public virtual void Excluir(T entidade)
        {
            this._contexto.Entry(entidade).State = EntityState.Deleted;
            this._tabela.Remove(entidade);

        }
        
        public virtual List<T> Listar(Expression<Func<T, bool>> expressao)
        {

            return this._tabela
                .Where(expressao)
                .ToList();

        }

        public virtual List<T> ListarTodos()
        {

            return this._tabela
                .ToList();

        }

        public virtual T Recuperar(Expression<Func<T, bool>> expressao)
        {

            return this._tabela
                .Where(expressao)
                .SingleOrDefault();

        }


    }
}
