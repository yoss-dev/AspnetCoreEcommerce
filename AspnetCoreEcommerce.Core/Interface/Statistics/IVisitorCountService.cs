using AspnetCoreEcommerce.Core.Domain.Statistics;
using System;
using System.Collections.Generic;

namespace AspnetCoreEcommerce.Core.Interface.Statistics
{
    public interface IVisitorCountService
    {
        /// <summary>
        /// Get all VisitorCount
        /// </summary>
        /// <returns>VisitorCount entity list</returns>
        IList<VisitorCount> GetAllVisitorCount();

        /// <summary>
        /// Get all visitor count
        /// </summary>
        /// <param name="take">Number of date to return</param>
        /// <returns>Visitor count entity list</returns>
        IList<VisitorCount> GetAllVisitorCount(int take);

        /// <summary>
        /// Get VisitorCountByDate
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>VisitorCount entity</returns>
        VisitorCount GetVisitorCountByDate(DateTime date);

        /// <summary>
        /// Insert VisitorCount
        /// </summary>
        /// <param name="visitorCount">VisitorCount entity</param>
        void InsertVisitorCount(VisitorCount visitorCount);

        /// <summary>
        /// Update VisitorCount
        /// </summary>
        /// <param name="visitorCount">VisitorCount entity</param>
        void UpdateVisitorCount(VisitorCount visitorCount);
    }
}
