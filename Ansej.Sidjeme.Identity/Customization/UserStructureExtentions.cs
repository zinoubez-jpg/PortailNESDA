using System.ComponentModel.DataAnnotations.Schema;

namespace Ansej.Sidjeme.Identity
{
    public partial class User
    {
        [NotMapped]
        public string FullName { get => $"{Nom} {Prenom}"; }

    }
}
