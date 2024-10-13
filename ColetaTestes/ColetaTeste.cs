using ColetaApi.Controllers;
using ColetaApi.Data.Contexts;
using ColetaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net.Sockets;

namespace Fiap.Web.Alunos.Tests
{
    public class ColetaTeste
    {
        private readonly Mock<DatabaseContext> _mockContext;
        private readonly ColetaController _controller;
        private readonly DbSet<ColetaModel> _mockSet;

        public ColetaTeste()
        {
            _mockContext = new Mock<DatabaseContext>();
            _mockSet = MockDbSet();
            _mockContext.Setup(m => m.Coleta).Returns(_mockSet);
            _controller = new ColetaController(_mockContext.Object);
        }        

        private DbSet<ColetaModel> MockDbSet()
        {
            var data  = new List<ColetaModel>
            {
                new ColetaModel { Pk_id = 100, Ds_coleta = "teste[unitest100]", Ds_tipocoleta = "teste[unitest100]", Dt_coleta = DateTime.Now },
                new ColetaModel { Pk_id = 101, Ds_coleta = "teste[unitest101]", Ds_tipocoleta = "teste[unitest101]", Dt_coleta = DateTime.Now }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ColetaModel>>();
            mockSet.As<IQueryable<ColetaModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ColetaModel>>().Setup(m => m.Expression).Returns(data.Expression); 
            mockSet.As<IQueryable<ColetaModel>>().Setup(m => m.ElementType).Returns(data.ElementType); 
            mockSet.As<IQueryable<ColetaModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet.Object;
        }

        [Fact]
        public void Index_RetornarListaDeClientes()
        {
            var result = _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ColetaModel>>(viewResult.Model);

            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Index_NaoDeveRetornaListaVazia()
        {
            _mockSet.RemoveRange(_mockSet.ToList());
            _mockContext.Setup(m => m.Coleta).Returns(_mockSet);
            var result = _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ColetaModel>>(viewResult.Model);
            Assert.NotEmpty(model);
        }
        [Fact]
        public void Index_RetornaExcessaoQuandoBancoDeDadosFalha()
        {
            _mockContext.Setup(m => m.Coleta).Throws(new System.Exception("Database error"));
            Assert.Throws<System.Exception>(() => _controller.Index());
        }
    }
}