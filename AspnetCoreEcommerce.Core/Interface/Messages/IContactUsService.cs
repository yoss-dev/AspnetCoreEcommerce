using AspnetCoreEcommerce.Core.Domain.Messages;
using System;
using System.Collections.Generic;

namespace AspnetCoreEcommerce.Core.Interface.Messages
{
    public interface IContactUsService
    {
        /// <summary>
        /// Get all ContactUsMessages
        /// </summary>
        /// <returns>List of ContactUsMessage entities</returns>
        IList<ContactUsMessage> GetAllMessages();

        /// <summary>
        /// Get ContactUsMessage by id
        /// </summary>
        /// <param name="id">ContactUsMessage id</param>
        /// <returns>ContactUsMessage entity</returns>
        ContactUsMessage GetMessageById(Guid id);

        /// <summary>
        /// Insert a ContactUsMessage
        /// </summary>
        /// <param name="message">ContactUsMessage Entity</param>
        void InsertMessage(ContactUsMessage message);

        /// <summary>
        /// Update ContactUsMessage
        /// </summary>
        /// <param name="message">ContactUsMessage entity</param>
        void UpdateMessage(ContactUsMessage message);

        /// <summary>
        /// Delete ContactUsMessage
        /// </summary>
        /// <param name="messagesIds">List of ContactUsMessage ids</param>
        void DeleteMessages(IList<Guid> messagesIds);

        /// <summary>
        /// Mark the ContactUsMessage as read
        /// </summary>
        /// <param name="messageId">ContactUsMessage id</param>
        void MarkAsRead(Guid messageId);
    }
}
