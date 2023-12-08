using Microsoft.Extensions.Logging;
using Moq;
using Neox.Common;
using Neox.Model;
using Neox.Repositories;
using Neox.Services;

namespace Neox.Tests
{
    public class ClientServiceTests
    {

        private ClientService _service;
        private Mock<IClientRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IClientRepository>();
            _service = new ClientService(new Mock<ILogger<ClientService>>().Object, _repository.Object);
        }

        [Test]
        public async Task Get_all_client_with_empty_list()
        {
            _repository.Setup(x => x.GetAll()).ReturnsAsync(new List<Client>());
            var clients = await _service.GetAll();

            Assert.That(clients.Count(), Is.EqualTo(0));

            _repository.Verify(x => x.GetAll(), Times.Once());
            _repository.VerifyNoOtherCalls();
        }

        [Test]
        public async Task Get_all_client_with_not_empty_list()
        {
            var clientList = new List<Client>() { new Client { Name = "name" }, new Client { Name = "name2" } };
            _repository.Setup(x => x.GetAll()).ReturnsAsync(clientList);
            var clients = await _service.GetAll();

            Assert.That(clients.Count(), Is.EqualTo(2));

            _repository.Verify(x => x.GetAll(), Times.Once());
            _repository.VerifyNoOtherCalls();
        }

        [Test]
        public async Task Create_client_with_empty_name_should_throw_exception()
        {
            var client = new Client() { Email = "email@domain.com" };

            var ex = Assert.ThrowsAsync<BusinessException>(code: () => _service.Create(client));

            _repository.Verify(x => x.Create(It.IsAny<Client>()), Times.Never());
            _repository.VerifyNoOtherCalls();
            Assert.That(ex.Message, Is.EqualTo("El Nombre no puede ser vacio"));
        }


        [Test]
        public async Task Create_client_with_empty_email_should_throw_exception()
        {
            var client = new Client() { Name = "name" };
            var ex = Assert.ThrowsAsync<BusinessException>(code: () => _service.Create(client));

            _repository.Verify(x => x.Create(It.IsAny<Client>()), Times.Never());
            _repository.VerifyNoOtherCalls();
            Assert.That(ex.Message, Is.EqualTo("El Email no puede ser vacio"));
        }

        [Test]
        public async Task Create_client_with_existing_email()
        {
            var client = new Client() { Name = "name", Email = "email@domain.com" };
            _repository.Setup(x => x.GetByEmail(client.Email)).ReturnsAsync(client);

            var ex = Assert.ThrowsAsync<BusinessException>(code: () => _service.Create(client));

            _repository.Verify(x => x.GetByEmail(It.IsAny<string>()), Times.Once());
            _repository.Verify(x => x.Create(It.IsAny<Client>()), Times.Never());
            _repository.VerifyNoOtherCalls();
            Assert.That(ex.Message, Is.EqualTo("Ya existe un cliente con ese correo"));
        }


        [Test]
        public async Task Create_client_successful()
        {
            int expectedId = 10;
            var client = new Client() { Name = "name", Email = "email@domain.com" };
            _repository.Setup(x => x.GetByEmail(It.IsAny<string>())).ReturnsAsync((Client)null);
            _repository.Setup(x => x.Create(It.IsAny<Client>())).ReturnsAsync(10);

            var id = await _service.Create(client);

            Assert.AreEqual(expectedId, id);
            _repository.Verify(x => x.GetByEmail(It.IsAny<string>()), Times.Once());
            _repository.Verify(x => x.Create(It.IsAny<Client>()), Times.Once());
            _repository.VerifyNoOtherCalls();

        }



    }
}