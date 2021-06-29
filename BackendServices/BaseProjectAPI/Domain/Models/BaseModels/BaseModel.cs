using System;

namespace BaseProjectAPI.Domain.Models.BaseModels
{
    /// <summary>
    /// Basic common fields (IsEnabled, CreatedOn, DisabledOn)
    /// </summary>
    public class BaseModelLevel1
    {
        public bool IsEnabled { get; set; } = true;
        public DateTimeOffset CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? DisabledOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
    }

    /// <summary>
    /// Strict audit common field (CreatedBy, DisabledBy)
    /// </summary>
    public class BaseModelLeve2 : BaseModelLevel1
    {
        public User CreatedBy { get; set; }
        public User DisabledBy { get; set; }
        public User UpdatedBy { get; set; }
    }
}
