using System;

namespace Entities.Interfaces
{
    public interface IFullEntity : IEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        int? CreatedBy { get; set; }
        int? UpdatedBy { get; set; }
        DateTime? DeleteAt { get; set; } 
    }
}
