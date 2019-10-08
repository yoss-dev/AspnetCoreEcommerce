using AspnetCoreEcommerce.Core.Domain.Statistics;
using AspnetCoreEcommerce.Core.Interface.Statistics;
using AspnetCoreEcommerce.Infrastructure.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspnetCoreEcommerce.Infrastructure.Services.Statistics
{
    public class VisitorCountService : IVisitorCountService
    {
        #region Fields

        private readonly IRepository<VisitorCount> _visitorCountRepository;
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor

        public VisitorCountService(IRepository<VisitorCount> visitorCountRepository, ApplicationDbContext context)
        {
            _visitorCountRepository = visitorCountRepository;
            _context = context;
        }

        #endregion

        #region Methods

        public IList<VisitorCount> GetAllVisitorCount()
        {
            return _visitorCountRepository.GetAll().ToList();
        }

        public IList<VisitorCount> GetAllVisitorCount(int take)
        {
            return _visitorCountRepository.GetAll().Take(take).ToList();
        }

        public VisitorCount GetVisitorCountByDate(DateTime date)
        {
            return _context.VisitorCounts.SingleOrDefault(x => x.Date == date.Date);
        }

        public void InsertVisitorCount(VisitorCount visitorCount)
        {
            if (visitorCount == null)
                throw new ArgumentNullException(nameof(visitorCount));

            visitorCount.Date = visitorCount.Date.Date;

            _visitorCountRepository.Insert(visitorCount);
            _visitorCountRepository.SaveChanges();
        }

        public void UpdateVisitorCount(VisitorCount visitorCount)
        {
            if (visitorCount == null)
                throw new ArgumentNullException(nameof(visitorCount));

            visitorCount.Date = visitorCount.Date.Date;
            visitorCount.ViewCount++;

            _visitorCountRepository.Update(visitorCount);
            _visitorCountRepository.SaveChanges();

        }

        #endregion

    }
}
