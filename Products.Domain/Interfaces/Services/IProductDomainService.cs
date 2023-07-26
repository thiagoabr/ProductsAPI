using ProductsAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.Domain.Interfaces.Services
{
    public interface IProductDomainService : IBaseDomainService<Product, Guid>
    {

    }
}
