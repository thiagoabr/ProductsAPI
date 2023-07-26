using Bogus;
using FluentAssertions;
using ProductsAPI.Application.Models.Commands;
using ProductsAPI.Application.Models.Queries;
using ProductsAPI.Tests.Helpers;
using System.Net;
using Xunit;

namespace ProductsAPI.Tests
{
    public class ProductsTest
    {
        //atributos
        private readonly string? _endpoint;
        private readonly Faker? _faker;

        //construtor
        public ProductsTest()
        {
            _endpoint = "/api/products";
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public async Task<ProductsDTO> Test_Products_Post_Returns_Created()
        {
            //Definindo os dados da requisição (cadastro)
            var command = new ProductsCreateCommand
            {
                Name = _faker?.Commerce.ProductName(),
                Price = decimal.Parse(_faker?.Commerce.Price(2)),
                Quantity = _faker.Random.Number(1, 100)
            };

            //Criando um objeto HTTP CLIENT para executar chamadas para a API
            var client = TestHelper.CreateClient;

            //executando a chamada para o serviço da API, enviando o command e capturando a resposta
            var result = await client.PostAsync(_endpoint, TestHelper.CreateContent(command));

            //verificando se a resposta é 201 (CREATED)
            result.StatusCode.Should().Be(HttpStatusCode.Created);

            //deserializando os dados retornados pela API
            return TestHelper.ReadResponse<ProductsDTO>(result);
        }

        [Fact]
        public async Task Test_Products_Put_Returns_Ok()
        {
            var dto = await Test_Products_Post_Returns_Created();

            //Definindo os dados da requisição (cadastro)
            var command = new ProductsUpdateCommand
            {
                Id = dto.Id,
                Name = _faker?.Commerce.ProductName(),
                Price = decimal.Parse(_faker?.Commerce.Price(2)),
                Quantity = _faker.Random.Number(1, 100)
            };

            //Criando um objeto HTTP CLIENT para executar chamadas para a API
            var client = TestHelper.CreateClient;

            //executando a chamada para o serviço da API, enviando o command e capturando a resposta
            var result = await client.PutAsync(_endpoint, TestHelper.CreateContent(command));

            //verificando se a resposta é 200 (OK)
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Test_Products_Delete_Returns_Ok()
        {
            //executando o teste para cadastro de produto
            var dto = await Test_Products_Post_Returns_Created();

            //Criando um objeto HTTP CLIENT para executar chamadas para a API
            var client = TestHelper.CreateClient;

            //executando a chamada para o serviço da API, enviando o command e capturando a resposta
            var result = await client.DeleteAsync($"{_endpoint}/{dto.Id}");

            //verificando se a resposta é 200 (OK)
            result.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public async Task Test_Products_GetAll_Returns_Ok()
        {
            //executando o teste para cadastro de produto
            var dto = await Test_Products_Post_Returns_Created();

            //Criando um objeto HTTP CLIENT para executar chamadas para a API
            var client = TestHelper.CreateClient;

            //executando a chamada para o serviço da API e capturando a resposta
            var result = await client.GetAsync(_endpoint);

            //verificando se a resposta é 200 (OK)
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            //deserializando a resposta
            var list = TestHelper.ReadResponse<List<ProductsDTO>>(result);

            //verificando se o produto cadastrado está nesta lista
            list.FirstOrDefault(p => p.Id.Equals(dto.Id)).Should().NotBeNull();
        }

        [Fact]
        public async Task Test_Products_GetById_Returns_Ok()
        {
            //executando o teste para cadastro de produto
            var dto = await Test_Products_Post_Returns_Created();

            //Criando um objeto HTTP CLIENT para executar chamadas para a API
            var client = TestHelper.CreateClient;

            //executando a chamada para o serviço da API e capturando a resposta
            var result = await client.GetAsync($"{_endpoint}/{dto.Id}");

            //verificando se a resposta é 200 (OK)
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            //deserializando a resposta
            var item = TestHelper.ReadResponse<ProductsDTO>(result);

            //comparando se o produto é o mesmo que foi cadastrado
            item.Id.Should().Be(dto.Id);
            item.Name.Should().Be(dto.Name);
            item.Price.Should().Be(dto.Price);
            item.Quantity.Should().Be(dto.Quantity);
        }

    }
}
