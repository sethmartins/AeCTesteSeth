using AeCTesteSeth.BLL.Models;
using AeCTesteSeth.API.Controllers;
using AeCTesteSeth.DOMAIN.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeCTesteSeth.DAL.Repositorios;
using AeCTesteSeth.DAL.Context;
using EntityFrameworkCore.Testing.Moq;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace AeCTesteSeth.Test.Repository
{
    public class UsuarioRepositoryTeste
    {
 

   

        [Fact]
        public void UsuariosGetAllTest()
        {
            //Arrange
            var _dbContext = new DbContextOptionsBuilder<MyContext>()
            .UseInMemoryDatabase(databaseName: "AeCDataBaseTest")
            .Options;
            int count = 0;
            
            using (var context = new MyContext(_dbContext))
            {
                count = context.Usuarios.Count();

                //Act                
                GetTestUsers()
                    .ForEach(u =>
                    {
                        context.Usuarios.Add(u);
                        // await _usuarioscontroller.PostUsuario(u); 
                    });
                context.SaveChanges();                
            
           
                UsuarioRepository usuarioRepository = new UsuarioRepository(context);
                var usuarios = usuarioRepository.GetAll().Result.ToList();                
                
                //Assert
                Assert.Equal(count + 2, usuarios.Count);


            }
        }
        [Fact]
        public void UsuariosGetTest()
        {
            //Arrange
            var _dbContext = new DbContextOptionsBuilder<MyContext>()
            .UseInMemoryDatabase(databaseName: "AeCDataBaseTest")
            .Options;


            using (var context = new MyContext(_dbContext))
            {
                GetTestUsers()
                    .ForEach(u =>
                    {
                        context.Usuarios.Add(u);
                        // await _usuarioscontroller.PostUsuario(u); 
                    });
                context.SaveChanges();
                //Act
                UsuarioRepository usuarioRepository = new UsuarioRepository(context);
                var usuario = usuarioRepository.Get(1);
                //Assert
                Assert.Equal("User1", usuario.Result.Nome);
                //return  _context.Set<T>().FindAsync(id);
            }
            
        }

        [Fact]
        public  void UsuariosAddTest()
        {
            //Arrange
            var _dbContext = new DbContextOptionsBuilder<MyContext>()
            .UseInMemoryDatabase(databaseName: "AeCDataBaseTest")
            .Options;


            using (var context = new MyContext(_dbContext))
            {
                GetTestUsers()
                    .ForEach(u =>
                    {
                        context.Usuarios.Add(u);
                        // await _usuarioscontroller.PostUsuario(u); 
                    });
                context.SaveChanges();
                //Act
                UsuarioRepository usuarioRepository = new UsuarioRepository(context);
                usuarioRepository.Add(new Usuario() { Id = 3, Nome = "User3", Senha = "654321", Usuario_ = "seth", Enderecos=null });
                var usuario = usuarioRepository.Get(3);
                //Assert
                Assert.Equal("User3", usuario.Result.Nome);

            }
        }
        [Fact]
        public void UsuariosDeleteTest()
        {
                //Arrange
                var _dbContext = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(databaseName: "AeCDataBaseTest")
                .Options;
                
                using (var context = new MyContext(_dbContext))
                {

                    
                    if (context.Usuarios.Count() == 0)
                    {
                        GetTestUsers()
                            .ForEach(u =>
                            {
                                context.Usuarios.Add(u);
                                // await _usuarioscontroller.PostUsuario(u); 
                            });
                        
                        context.SaveChanges();
                    }
                    //Act
                    UsuarioRepository usuarioRepository = new UsuarioRepository(context);
                    int lastuser = context.Usuarios.Last().Id;
                    
                    var userdeleted = usuarioRepository.Get(lastuser).Result;
                    usuarioRepository.Delete(userdeleted);
                    context.SaveChanges();
                    var usuario = usuarioRepository.GetAll().Result.Last();

                    //Assert
                   Assert.False(lastuser == usuario.Id); ;
                } 
         }
        [Fact]
        public void UsuariosUpdateTest()
        {
            //Arrange
            var _dbContext = new DbContextOptionsBuilder<MyContext>()
            .UseInMemoryDatabase(databaseName: "AeCDataBaseTest")
            .Options;

            using (var context = new MyContext(_dbContext))
            {
                GetTestUsers()
                    .ForEach(u =>
                    {
                        context.Usuarios.Add(u);
                        // await _usuarioscontroller.PostUsuario(u); 
                    });
                context.SaveChanges();
                //Act
                UsuarioRepository usuarioRepository = new UsuarioRepository(context);

                var userupdated = new Usuario() { Nome = "User3", Senha = "654321", Usuario_ = "seth" };
                usuarioRepository.Update(userupdated);
                context.SaveChanges();
                var usuario = usuarioRepository.GetAll().Result.Last();
                //Assert
                Assert.Equal("User3", usuario.Nome);
            } 
        }
        private List<Usuario> GetTestUsers()
        {
            return new List<Usuario>
            {
                new Usuario {  Nome = "User1", Senha = "123456", Usuario_ = "seth" },
                new Usuario {  Nome = "User2", Senha = "12345678", Usuario_ = "sethmartins"  }
            };
        }
    }
}
