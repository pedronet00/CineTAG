
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositorio.entidades;

namespace Repositorio.contexto
{
    public class EmpresaContexto:DbContext
    {
        public EmpresaContexto() {
            //criar ou atualizar o banco de dados
            this.Database.EnsureCreated();
        }
        public DbSet<Produto> produto { get; set; }
        public DbSet<Categoria> categoria { get; set; }

        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Status> status { get; set; }
        public DbSet<ComprasProdutos> comprasProdutos { get; set; }
        public DbSet<Compras> compras { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           base.OnConfiguring(optionsBuilder);
            var stringConexao = @"Server=DESKTOP-HMSLU0T;DataBase=Empresa2023New6;integrated security=true;";

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(stringConexao);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Categoria>(entidade=> {
                entidade.HasKey(e=>e.id);//chave primaria
                //qtde max caracteres
                entidade.Property(e=>e.descricao).HasMaxLength(150);

            });

            modelBuilder.Entity<Status>(entidade => {
                entidade.HasKey(e => e.id);//chave primaria
                //qtde max caracteres
                entidade.Property(e => e.descricao).HasMaxLength(150);

            });

            modelBuilder.Entity<Compras>(entidade => {
                entidade.HasKey(e => e.id);//chave primaria
                                   
                entidade.Property(e => e.valor).HasPrecision(8, 2);
                //relacionamento
                entidade.HasOne(e => e.status) //prop lado um
                .WithMany(c => c.compras) //prop lado Muitos
                .HasForeignKey(e => e.idStatus) //prop chave estrangeira
                .HasConstraintName("FK_Compras_Status") //nome do relacionamento
                .OnDelete(DeleteBehavior.NoAction); //configuração da exclusao
                entidade.Property(e => e.idPreferencia).HasMaxLength(500);
                entidade.Property(e => e.url).HasMaxLength(500);
            });

            modelBuilder.Entity<ComprasProdutos>(entidade => {
                entidade.HasKey(e => e.id);//chave primaria

                entidade.Property(e => e.valor).HasPrecision(8, 2);

                //relacionamento
                entidade.HasOne(e => e.produto) //prop lado um
                .WithMany(c => c.comprasprodutos) //prop lado Muitos
                .HasForeignKey(e => e.idProduto) //prop chave estrangeira
                .HasConstraintName("FK_Produto_ComprasProduto") //nome do relacionamento
                .OnDelete(DeleteBehavior.NoAction); //configuração da exclusao

                //relacionamento
                entidade.HasOne(e => e.compras) //prop lado um
                .WithMany(c => c.comprasprodutos) //prop lado Muitos
                .HasForeignKey(e => e.idCompra) //prop chave estrangeira
                .HasConstraintName("FK_Compras_ComprasProduto") //nome do relacionamento
                .OnDelete(DeleteBehavior.NoAction); //configuração da exclusao

            });

            modelBuilder.Entity<Usuario>(entidade => {
                entidade.HasKey(e => e.id);//chave primaria
                //qtde max caracteres
                entidade.Property(e => e.nome).HasMaxLength(150);
                entidade.Property(e => e.email).HasMaxLength(150);
                entidade.Property(e => e.senha).HasMaxLength(20);

            });

            modelBuilder.Entity<Produto>(entidade => {
                entidade.HasKey(e => e.id);
                entidade.Property(e=>e.descricao).HasMaxLength(150);
                //precisão do decimal
                entidade.Property(e=>e.valor).HasPrecision(8,2);
                //relacionamento
                entidade.HasOne(e => e.categoria) //prop lado um
                .WithMany(c => c.produtos) //prop lado Muitos
                .HasForeignKey(e => e.idCategoria) //prop chave estrangeira
                .HasConstraintName("FK_Produto_Categoria") //nome do relacionamento
                .OnDelete(DeleteBehavior.NoAction); //configuração da exclusao

            });

        }
    }
}
