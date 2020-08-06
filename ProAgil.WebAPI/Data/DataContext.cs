using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProAgil.WebAPI.Model;

namespace ProAgil.WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        { 
        }
        public DbSet<Evento> Eventos { get; set; }

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
