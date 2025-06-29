using Ansej.Business.Core;
using Ansej.Data.Abstractions;
using System;
using System.Linq.Expressions;

namespace Ansej.Sidjeme.Identity
{
    public class StructureService : GenericBusinessService<Structure, int>
    {
        public StructureService(IUnitOfWork<IdentityContext> unitOfWork) : base(unitOfWork)
        {

        }
        public override Expression<Func<Structure, object>>[] GetDefaultLoadProperties()
        {

            Expression<Func<Structure, object>> loadNiveau = x => x.Niveau;


            return new Expression<Func<Structure, object>>[] { loadNiveau };
        }
    }
}
