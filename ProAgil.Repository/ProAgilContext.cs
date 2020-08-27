using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilContext : DbContext
    {
        public ProAgilContext(DbContextOptions<ProAgilContext> options) : base (options)
        { 
        }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>().HasKey(PE => new { PE.EventoID, PE.PalestranteID });
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //            "data source=DESKTOP-T7HPOF5\\SQLEXPRESS;" + //Server name
        //            "initial catalog=Kelvin;" + //Banco de dados já criado (se não existir irá criar um com esse nome)
        //            "persist security info=True;" +
        //            "user id=Kelvin;" + //Login
        //            "password=kelvin;" //Password
        //        );
        //}
    }
}
