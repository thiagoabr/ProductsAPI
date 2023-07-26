using MediatR;
using ProductsAPI.Application.Interfaces.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.Application.Handlers.Notifications
{
    /// <summary>
    /// Componente para 'escutar' o resultado do processamento dos RequestHandlers
    /// e executar as operações de CREATE, UPDATE ou DELETE no banco de cache
    /// </summary>
    public class ProductsNotificationHandler : INotificationHandler<ProductsNotification>
    {
        private readonly IProductStore? _productStore;

        public ProductsNotificationHandler(IProductStore? productStore)
        {
            _productStore = productStore;
        }

        /// <summary>
        /// Método para ouvir e processar as notificações no banco de dados de leitura
        /// Chamado após um REQUEST HANDLER executar o PUBLISH NOTIFICATION
        /// </summary>
        public Task Handle(ProductsNotification notification, CancellationToken cancellationToken)
        {
            //verificando o tipo de notificação recebida
            switch (notification.Action)
            {
                case ActionNotification.Created:
                    _productStore?.Add(notification.ProductsDTO);
                    break;

                case ActionNotification.Updated:
                    _productStore?.Update(notification.ProductsDTO);
                    break;

                case ActionNotification.Deleted:
                    _productStore?.Delete(notification.ProductsDTO.Id.Value);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
