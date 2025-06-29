using Ansej.Domain.Core;
using System;

namespace Ansej.Sidjeme.Identity
{
    public partial class UserStructure : IEntity<int>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int StructureId { get; set; }
        public DateTime DateAffectation { get; set; }
        public DateTime? DateSortie { get; set; }
        public bool Actuelle { get; set; }
        public virtual User User { get; set; }
        public virtual Structure Structure { get; set; }
    }
}
