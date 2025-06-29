using System;
using System.Collections.Generic;
using System.Text;

namespace Ansej.Domain.Core
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }

    public interface IEntity : IEntity<Guid>
    {

    }
}
