using AspnetCoreEcommerce.Core.Domain.Messages;
using AspnetCoreEcommerce.Core.Interface.Messages;
using AspnetCoreEcommerce.Infrastructure.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspnetCoreEcommerce.Infrastructure.Services.Messages
{
    public class ContactUsService : IContactUsService
    {
        #region Fields

        private readonly IRepository<ContactUsMessage> _contactUsRepository;

        #endregion

        #region Constructor

        public ContactUsService(IRepository<ContactUsMessage> contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }

        #endregion

        #region Methods

        public IList<ContactUsMessage> GetAllMessages()
        {
            var entities = _contactUsRepository.GetAll()
                .OrderByDescending(x => x.SendDate)
                .ToList();

            return entities;
        }

        public ContactUsMessage GetMessageById(Guid id)
        {
            if (id.Equals(Guid.Empty))
                return null;


            var entity = _contactUsRepository
                .FindByExpression(x => x.Id == id);

            return entity;
        }

        public void InsertMessage(ContactUsMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            _contactUsRepository.Insert(message);
            _contactUsRepository.SaveChanges();
        }

        public void UpdateMessage(ContactUsMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            _contactUsRepository.Update(message);
            _contactUsRepository.SaveChanges();
        }

        public void DeleteMessages(IList<Guid> messagesIds)
        {
            if (messagesIds == null)
                throw new ArgumentNullException(nameof(messagesIds));

            foreach (var id in messagesIds)
            {
                _contactUsRepository.Delete(GetMessageById(id));
            }

            _contactUsRepository.SaveChanges();
        }

        public void MarkAsRead(Guid messageId)
        {
            if (messageId.Equals(Guid.Empty))
                throw new ArgumentNullException(nameof(messageId));

            var entity = GetMessageById(messageId);
            entity.Read = true;

            _contactUsRepository.Update(entity);
            _contactUsRepository.SaveChanges();
        }

        #endregion
    }
}
