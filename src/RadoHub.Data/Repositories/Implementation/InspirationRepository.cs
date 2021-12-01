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
            var currentInspirationPeriod = GetCurrentHolidayPeriod();
            if (currentInspirationPeriod == null)
            {
                currentInspirationPeriod = GetMonthPeriod();
            }
            if (currentInspirationPeriod == null)
            {
                currentInspirationPeriod = GetSeasonPeriod();
            }
            if (currentInspirationPeriod == null)
            {
                return null;
            }

            return currentInspirationPeriod.ImageFileName;
        }

        private InspirationPeriod GetSeasonPeriod()
        {
            return GetActiveInspirationPeriods()
                .FirstOrDefault(period => period.Type == (int)InspirationPeriodTypes.Seasonal);
        }

        private InspirationPeriod GetMonthPeriod()
        {
            return GetActiveInspirationPeriods()
                .FirstOrDefault(period => period.Type == (int)InspirationPeriodTypes.Monthly);
        }

        private InspirationPeriod GetCurrentHolidayPeriod()
        {
            return GetActiveInspirationPeriods()
                .FirstOrDefault(period => period.Type == (int)InspirationPeriodTypes.Holiday);
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
                if (DateTime.UtcNow.Month >= inspirationPeriod.StartDate.Month && DateTime.UtcNow.Month <= inspirationPeriod.EndDate.Month)
                {
                    result = true;
                }
            }
            else if (inspirationPeriod.StartDate.Month <= inspirationPeriod.StartDate.Month)
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
