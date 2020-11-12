using System;

namespace RadoHub.Data.Models
{
    public class InspirationPeriod : BaseModel<int>
    {
        public string Name { get; set; }

        public int Type { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ImageFileName { get; set; }
    }
}
