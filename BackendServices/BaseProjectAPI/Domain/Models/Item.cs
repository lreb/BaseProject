using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectAPI.Domain.Models
{
    public class Item
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool IsEnabled { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset DisabledOn { get; set; }
    }
}
