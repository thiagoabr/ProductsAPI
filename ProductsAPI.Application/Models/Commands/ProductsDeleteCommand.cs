using MediatR;
using ProductsAPI.Application.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.Application.Models.Commands
{
    /// <summary>
    /// Modelo de dados para o serviço de exclusão de produto
    /// </summary>
    public class ProductsDeleteCommand : IRequest<ProductsDTO>
    {
        public Guid? Id { get; set; }
    }
}
