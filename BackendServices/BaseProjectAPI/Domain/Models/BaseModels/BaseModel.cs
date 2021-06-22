using System;

namespace BaseProjectAPI.Domain.Models.BaseModels
{
    public class BaseModel
    {
        public bool IsEnabled { get; set; } = true;
        public DateTimeOffset CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? DisabledOn { get; set; }
    }
}
