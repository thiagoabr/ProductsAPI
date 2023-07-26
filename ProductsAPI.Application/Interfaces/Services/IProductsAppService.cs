using ProductsAPI.Application.Models.Commands;
using ProductsAPI.Application.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.Application.Interfaces.Services
{
    /// <summary>
    /// Interface para os serviços da aplicação
    /// </summary>
    public interface IProductsAppService
    {
        Task<ProductsDTO> Create(ProductsCreateCommand command);
        Task<ProductsDTO> Update(ProductsUpdateCommand command);
        Task<ProductsDTO> Delete(ProductsDeleteCommand command);

        List<ProductsDTO> GetAll();
        ProductsDTO GetById(Guid id);
    }
}
