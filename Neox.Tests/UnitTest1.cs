using Microsoft.Extensions.Logging;
using Moq;
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



    }
}