using AeCTesteSeth.BLL.Entities;
using AeCTesteSeth.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeCTesteSeth.DAL.Configuration
{
    public class EnderecoConfiguration :  IEntityTypeConfiguration<Endereco>
    {
        // usando Fluent Api pra mostrar que também sei essa abordagem
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).UseIdentityColumn(1, 1).IsRequired();
            

            builder.Property(p => p.Logradouro).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Numero).HasMaxLength(10).IsRequired();
            builder.Property(p => p.Complemento).HasMaxLength(50);
            builder.Property(p => p.Cep).HasMaxLength(10).IsRequired();
            builder.Property(p => p.Bairro).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Cidade).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Uf).HasMaxLength(2).IsRequired();




            builder.HasOne(e => e.Usuario).WithMany(e => e.Enderecos)
               .HasForeignKey(e => e.UsuarioId);
        }
    }

    
}
