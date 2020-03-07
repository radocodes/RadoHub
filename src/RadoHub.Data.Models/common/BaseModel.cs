using System;
using System.Collections.Generic;
using System.Text;

namespace RadoHub.Data.Models
{
    public class BaseModel<T>
    {
        public T Id { get; set; }
    }
}
