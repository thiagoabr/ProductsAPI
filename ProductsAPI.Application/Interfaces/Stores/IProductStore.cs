using ProductsAPI.Application.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.Application.Interfaces.Stores
{
    public interface IProductStore
    {
        void Add(ProductsDTO item);
        void Update(ProductsDTO item);
        void Delete(Guid id);
        List<ProductsDTO> GetAll();
        ProductsDTO GetById(Guid id);
    }
}

