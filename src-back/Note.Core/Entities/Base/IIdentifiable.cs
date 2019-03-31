using System;

namespace Note.Core.Entities.Base
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}
