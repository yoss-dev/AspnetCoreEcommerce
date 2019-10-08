using AspnetCoreEcommerce.Core.Domain.Messages;
using AspnetCoreEcommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace AspnetCoreEcommerce.xUnitTest.Services.Messages
{
    public class MessageService_Test
    {
        [Fact]
        public void MessageService_Test_GetAllMessages()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MessageService_Test_GetAllMessages")
                .Options;

            var contactUsMessageEntities = new List<ContactUsMessage>()
            {
                new ContactUsMessage { Email = "email@email.com", Title = "Title", Message = "Message", Read = false, SendDate = DateTime.Now },
                new ContactUsMessage { Email = "email@email.com", Title = "Title", Message = "Message", Read = false, SendDate = DateTime.Now }
            };

            using (var context = new ApplicationDbContext(options))
            {
                foreach (var message in contactUsMessageEntities)
                    context.ContactUsMessage.Add(message);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);
                // assert
                Assert.Equal(contactUsMessageEntities.Count, service.ContactUsService.GetAllMessages().Count);
            }
        }

        [Fact]
        public void MessageService_Test_GetMessageById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MessageService_Test_GetMessageById")
                .Options;

            var messageEntity = new ContactUsMessage() { Id = Guid.NewGuid(), Name = "Message 1" };

            using (var context = new ApplicationDbContext(options))
            {
                context.ContactUsMessage.Add(messageEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //assert
                Assert.NotNull(service.ContactUsService.GetMessageById(messageEntity.Id));
            }
        }

        [Fact]
        public void MessageService_Test_InsertMessage()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MessageService_Test_InsertMessage")
                .Options;

            var messageEntity = new ContactUsMessage()
            {
                Id = Guid.NewGuid(),
                Email = "email@email.com",
                Title = "Title",
                Message = "Message",
                Read = false,
                SendDate = DateTime.Now
            };

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                service.ContactUsService.InsertMessage(messageEntity);

                //assert
                Assert.Equal(
                    1,
                    service.ContactUsService.GetAllMessages().Count);
            }

        }

        [Fact]
        public void MessageService_Test_UpdateMessage()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MessageService_Test_UpdateMessage")
                .Options;

            var testId = Guid.NewGuid();
            var messageEntity = new ContactUsMessage()
            {
                Id = testId,
                Email = "email@email.com",
                Title = "Title",
                Message = "Message",
                Read = false,
                SendDate = DateTime.Now
            };

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);
                service.ContactUsService.InsertMessage(messageEntity);

                //act
                messageEntity.Message = "Message Updated";
                service.ContactUsService.UpdateMessage(messageEntity);

                //assert
                Assert.Equal(
                    "Message Updated",
                    service.ContactUsService.GetMessageById(testId).Message);
            }

        }

        [Fact]
        public void MessageService_Test_DeleteMessages()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MessageService_Test_DeleteMessages")
                .Options;

            var testId = Guid.NewGuid();
            var messageEntity = new ContactUsMessage()
            {
                Id = testId,
                Email = "email@email.com",
                Title = "Title",
                Message = "Message",
                Read = false,
                SendDate = DateTime.Now
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.ContactUsMessage.Add(messageEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                service.ContactUsService.DeleteMessages(new List<Guid>() { testId });

                //assert
                Assert.Empty(service.ContactUsService.GetAllMessages());
            }
        }

        [Fact]
        public void MessageService_Test_MarkAsRead()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MessageService_Test_MarkAsRead")
                .Options;

            var testId = Guid.NewGuid();
            var messageEntity = new ContactUsMessage()
            {
                Id = testId,
                Email = "email@email.com",
                Title = "Title",
                Message = "Message",
                Read = false,
                SendDate = DateTime.Now
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.ContactUsMessage.Add(messageEntity);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new Service(context);

                //act
                service.ContactUsService.MarkAsRead(testId);

                //assert
                Assert.True(service.ContactUsService.GetMessageById(testId).Read);
            }
        }
    }
}
