using Moq;
using System;
using Xunit;
using crossapp.unitOfWork;
using Microsoft.EntityFrameworkCore;
using entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace canalonline.data.test.repositories
{

    public class GenericRepositoryTests
    {

        private (GenericRepository<Offer> repository,MockRepository mockContainer) CreateGenericRepository(Action<Mock<IEntityUnitOfWork>> unitOfWork = null)
        {
            
            //Creamos objetos mock para evitar dependencias en nuestra pruebas

            Mock<IEntityUnitOfWork> mockEntityUnitOfWork;

            //Creamos el container Moq
            var mockRepository = new MockRepository(MockBehavior.Strict);

            //Crea un Moq de IEntityUnitOfWork
            mockEntityUnitOfWork = mockRepository.Create<IEntityUnitOfWork>();

            unitOfWork?.Invoke(mockEntityUnitOfWork);

            return (new GenericRepository<Offer>(
                mockEntityUnitOfWork.Object,
                new movistarContext()),
                mockRepository);
        }

        [Fact]
        public async Task Update()
        {
            // Arrange
            (var repo, var mockContainer) = this.CreateGenericRepository(
                x => x.Setup(mehotd => 
                                
                            //Compobamos que el método se usa

                             mehotd.Update(It.IsAny<Offer>()))
                                   .Returns(Task.Run(()=> { }))
                                   .Verifiable()
            );
                

            // Act
            var offer = (await repo.Get()).First();
            offer.Name = "New Name";

            await repo.Update(offer);

            // Assert
            mockContainer.VerifyAll();
        }

        [Fact]
        public async Task Insert()
        {
            // Arrange
            (var repo, var mockContainer) = this.CreateGenericRepository(
                x => x.Setup(mehotd =>

                             //Compobamos que el método se usa

                             mehotd.Add(It.IsAny<Offer>()))
                                   .Returns(Task.Run(()=> { }))
                                   .Verifiable()

            );


            // Act
            var offer = new Offer();
            await repo.Add(offer);

            // Assert
            mockContainer.VerifyAll();
        }

        [Fact]
        public async Task Remove()
        {
            // Arrange
            (var repo, var mockContainer) = this.CreateGenericRepository(
                x => x.Setup(mehotd =>

                             //Compobamos que el método se usa

                             mehotd.Remove(It.IsAny<Offer>()))
                                   .Returns(Task.Run(() => { }))
                                   .Verifiable()

            );


            // Act
            var offer = (await repo.Get()).First();
            await repo.Remove(offer);

            // Assert
            mockContainer.VerifyAll();
        }
    }
}
