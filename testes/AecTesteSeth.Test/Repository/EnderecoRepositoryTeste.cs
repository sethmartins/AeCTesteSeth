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
    public class EnderecoRepositoryTeste
    {
    

   

        [Fact]
        public void EnderecosGetAllTest()
        {
            //Arrange
            var _dbContext = new DbContextOptionsBuilder<MyContext>()
            .UseInMemoryDatabase(databaseName: "AeCDataBaseTest")
            .Options;
            int count = 0;
            
            using (var context = new MyContext(_dbContext))
            {
                count = context.Enderecos.Count();

                //Act                
                GetTestAddress()
                    .ForEach(u =>
                    {
                        context.Enderecos.Add(u);
                        
                    });
                context.SaveChanges();                
            
           
                EnderecoRepository enderecoRepository = new EnderecoRepository(context);
                var enderecos = enderecoRepository.GetAll().Result.ToList();                
                
                //Assert
                Assert.Equal(count + 2, enderecos.Count);

            }
        }
        [Fact]
        public void EnderecosGetTest()
        {
            //Arrange
            var _dbContext = new DbContextOptionsBuilder<MyContext>()
            .UseInMemoryDatabase(databaseName: "AeCDataBaseTest")
            .Options;


            using (var context = new MyContext(_dbContext))
            {
                if (context.Enderecos.Count() == 0)
                {
                    GetTestAddress()
                    .ForEach(u =>
                    {
                        context.Enderecos.Add(u);
                        // await _usuarioscontroller.PostUsuario(u); 
                    });
                }
                    
                context.SaveChanges();
                //Act
                EnderecoRepository enderecoRepository = new EnderecoRepository(context);
                var addresslast = enderecoRepository.GetAll().Result.Last();
                var endereco = enderecoRepository.Get(addresslast.Id);
                //Assert
                Assert.Equal(addresslast.Id, endereco.Result.Id);
                //return  _context.Set<T>().FindAsync(id);
            }
            
        }

        [Fact]
        public  void EnderecosAddTest()
        {
            //Arrange
            var _dbContext = new DbContextOptionsBuilder<MyContext>()
            .UseInMemoryDatabase(databaseName: "AeCDataBaseTest")
            .Options;


            using (var context = new MyContext(_dbContext))
            {
                if (context.Enderecos.Count() == 0)
                {
                    GetTestAddress()
                    .ForEach(u =>
                    {
                        context.Enderecos.Add(u);
                         
                    });
                }
                context.SaveChanges();
                //Act
                EnderecoRepository enderecoRepository =
                    new EnderecoRepository(context);
                Endereco endereco1 = new Endereco
                {
                    Logradouro = "rua 1",
                    Bairro = "Cuia",
                    Cep = "58000000",
                    UsuarioId = 1,
                    Cidade = "João Pessoa",
                    Uf = "PB",
                    Usuario = new Usuario { Nome = "User1", Senha = "123456", Usuario_ = "seth" },
                    Numero = "123",
                    Complemento = "condominio irmã dulce"
                };
                 enderecoRepository.Add(endereco1);
                context.SaveChanges();
                var endereco = enderecoRepository.GetAll().Result.Last();
                //Assert
                Assert.Equal("rua 1", endereco.Logradouro);

            }
        }
        [Fact]
        public void EnderecosDeleteTest()
        {
                //Arrange
                var _dbContext = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(databaseName: "AeCDataBaseTest")
                .Options;
                
                using (var context = new MyContext(_dbContext))
                {                    
                    if (context.Enderecos.Count() == 0)
                    {
                         GetTestAddress()
                            .ForEach(u =>
                            {
                                context.Enderecos.Add(u);                                 
                            });
                        
                        context.SaveChanges();
                    }
                    //Act
                    EnderecoRepository enderecoRepository = new EnderecoRepository(context);
                    int countendereco = enderecoRepository.GetAll().Result.Count();
                   Endereco lastendereco = enderecoRepository.GetAll().Result.Last();
                //var addressdeleted = enderecoRepository.Get(lastendereco).Result;
                    enderecoRepository.Delete(lastendereco);
                    
                    context.SaveChanges();
                    var endereco = enderecoRepository.GetAll().Result.Count();

                    //Assert
                   Assert.False(countendereco == endereco); ;
                } 
         }
        [Fact]
        public async void EnderecosUpdateTest()
        {
            //Arrange
            var _dbContext = new DbContextOptionsBuilder<MyContext>()
            .UseInMemoryDatabase(databaseName: "AeCDataBaseTest")
            .Options;

            using (var context = new MyContext(_dbContext))
            {
                if( context.Enderecos.Count() == 0)
                {
                    GetTestAddress()
                    .ForEach(u =>
                    {
                        context.Enderecos.Add(u);
                    });
                }
                 
                context.SaveChanges();
                //Act
                
                EnderecoRepository enderecoRepository = new EnderecoRepository(context);
                var result =  enderecoRepository.GetAll().Result.ToList();
                var firstAdress = result.First();
                firstAdress.Bairro =  "Bessa";
                
                enderecoRepository.Update(firstAdress);
                context.SaveChanges();
                var endereco = enderecoRepository.Get(firstAdress.Id).Result;
                //Assert
                Assert.Equal("Bessa", endereco.Bairro);
            } 
        }
        private List<Endereco> GetTestAddress()
        {
            return new List<Endereco>
            {
                new Endereco 
                { 
                    Logradouro = "Rua da agua",
                    Bairro = "Cuia", 
                    Cep = "58000000",
                    UsuarioId = 1,
                    Cidade = "João Pessoa",
                    Uf= "PB",
                    Usuario = new Usuario {  Nome = "User1", Senha = "123456", Usuario_ = "seth" },
                    Numero = "123",
                    Complemento= "condominio irmã dulce"
                },
                new Endereco 
                {  
                    Logradouro = "1",
                    Bairro = "Cuia",
                    Cep = "58000000",
                    UsuarioId = 1,
                    Cidade = "João Pessoa",
                    Uf= "PB",
                    Usuario = new Usuario {  Nome = "User1", Senha = "123456", Usuario_ = "seth" },
                    Numero = "123",
                    Complemento= "condominio irmã dulce"  }
            };
        }
    }
}
