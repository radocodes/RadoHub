using RadoHub.Data.Models;
using RadoHub.Data.Repositories.Contracts;
using RadoHub.Data.StaticData.Inspiration;
using RadoHub.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RadoHub.Data.Repositories.Implementation
{
    public class InspirationRepository : IInspirationRepository
    {
        private readonly RadoHubDbContext DbContext;

        public InspirationRepository(RadoHubDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public string GetInspirationImage()
        {
            var currentInspirationPeriod = GetSeasonPeriodByType(InspirationPeriodType.Holiday);
            if (currentInspirationPeriod == null)
            {
                currentInspirationPeriod = GetSeasonPeriodByType(InspirationPeriodType.Monthly);
            }
            if (currentInspirationPeriod == null)
            {
                currentInspirationPeriod = GetSeasonPeriodByType(InspirationPeriodType.Seasonal);
            }
            if (currentInspirationPeriod == null)
            {
                return null;
            }

            return currentInspirationPeriod.ImageFileName;
        }

        private InspirationPeriod GetSeasonPeriodByType(InspirationPeriodType inspirationPeriodType)
        {
            return GetActiveInspirationPeriods()
                .FirstOrDefault(period => period.Type == (int)inspirationPeriodType);
        }

        private IEnumerable<InspirationPeriod> GetActiveInspirationPeriods()
        {
            var allInspirationPeriods = this.DbContext.InspirationPeriods.ToHashSet();

            return allInspirationPeriods.Where(period => IsPeriodActive(period) == true);
        }

        private bool IsPeriodActive(InspirationPeriod inspirationPeriod)
        {
            bool result = false;

            if (inspirationPeriod.EndDate.Month < inspirationPeriod.StartDate.Month)
            {
                if (DateTime.UtcNow.Month > inspirationPeriod.EndDate.Month && DateTime.UtcNow.Month < inspirationPeriod.StartDate.Month)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (inspirationPeriod.StartDate.Month <= inspirationPeriod.EndDate.Month)
            {
                if (DateTime.UtcNow.Month >= inspirationPeriod.StartDate.Month && DateTime.UtcNow.Month <= inspirationPeriod.EndDate.Month)
                {
                    result = true;
                }
            }

            if (DateTime.UtcNow.Month == inspirationPeriod.StartDate.Month && DateTime.UtcNow.Date < inspirationPeriod.StartDate.Date)
            {
                result = false;
            }
            if (DateTime.UtcNow.Month == inspirationPeriod.EndDate.Month && DateTime.UtcNow.Date > inspirationPeriod.EndDate.Date)
            {
                result = false;
            }

            return result;
        }
    }
}
