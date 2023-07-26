using MediatR;
using ProductsAPI.Application.Handlers.Notifications;
using ProductsAPI.Application.Models.Commands;
using ProductsAPI.Application.Models.Queries;
using ProductsAPI.Domain.Interfaces.Services;
using ProductsAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.Application.Handlers.Requests
{
    /// <summary>
    /// Componente para 'escutar' as requisições do tipo COMMAND de produtos 
    /// </summary>
    public class ProductsRequestHandler :
        IRequestHandler<ProductsCreateCommand, ProductsDTO>,
        IRequestHandler<ProductsUpdateCommand, ProductsDTO>,
        IRequestHandler<ProductsDeleteCommand, ProductsDTO>
    {
        private readonly IMediator? _mediator;
        private readonly IProductDomainService? _productDomainService;

        public ProductsRequestHandler(IMediator? mediator, IProductDomainService? productDomainService)
        {
            _mediator = mediator;
            _productDomainService = productDomainService;
        }

        /// <summary>
        /// Método para processar o COMMAND CREATE do produto
        /// </summary>
        public async Task<ProductsDTO> Handle(ProductsCreateCommand request, CancellationToken cancellationToken)
        {
            //capturar os dados do request e criar um produto
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity,
                CreationDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };

            //envia o produto para ser cadastrado no domínio
            _productDomainService?.Add(product);

            //copia os dados do produto para um DTO
            var dto = new ProductsDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                CreationDate = product.CreationDate,
                LastModifiedDate = product.LastModifiedDate
            };

            //envia uma notificação para que o DTO seja gravado
            //em um banco de dados para leitura
            await _mediator.Publish(new ProductsNotification
            {
                Action = ActionNotification.Created,
                ProductsDTO = dto
            });

            //retornando os dados do DTO
            return dto;
        }

        public async Task<ProductsDTO> Handle(ProductsUpdateCommand request, CancellationToken cancellationToken)
        {
            //capturar o produto através do ID informado
            var product = _productDomainService.GetById(request.Id.Value);

            //modificando os dados do produto
            product.Name = request.Name;
            product.Price = request.Price;
            product.Quantity = request.Quantity;
            product.LastModifiedDate = DateTime.Now;

            //envia o produto para ser atualizado no domínio
            _productDomainService?.Update(product);

            //copia os dados do produto para um DTO
            var dto = new ProductsDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                CreationDate = product.CreationDate,
                LastModifiedDate = product.LastModifiedDate
            };

            //envia uma notificação para que o DTO seja atualizado
            //em um banco de dados para leitura
            await _mediator.Publish(new ProductsNotification
            {
                Action = ActionNotification.Updated,
                ProductsDTO = dto
            });

            //retornando os dados do DTO
            return dto;
        }

        public async Task<ProductsDTO> Handle(ProductsDeleteCommand request, CancellationToken cancellationToken)
        {
            //capturar o produto através do ID informado
            var product = _productDomainService.GetById(request.Id.Value);

            //excluindo o produto
            _productDomainService.Delete(product);

            //copia os dados do produto para um DTO
            var dto = new ProductsDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                CreationDate = product.CreationDate,
                LastModifiedDate = product.LastModifiedDate
            };

            //envia uma notificação para que o DTO seja excluído
            //em um banco de dados para leitura
            await _mediator.Publish(new ProductsNotification
            {
                Action = ActionNotification.Deleted,
                ProductsDTO = dto
            });

            //retornando os dados do DTO
            return dto;
        }
    }
}
