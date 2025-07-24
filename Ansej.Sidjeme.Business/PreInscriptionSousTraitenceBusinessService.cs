using Ansej.Business.Core;
using Ansej.Data.Abstractions;
using Ansej.Sidjeme.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ansej.Sidjeme.Business
{
    public class PreInscriptionSousTraitenceBusinessService : GenericBusinessService<PreInscriptionSousTraitence, int>
    {
        public PreInscriptionSousTraitenceBusinessService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        protected override void OnAdding(PreInscriptionSousTraitence entity)
        {
            base.OnAdding(entity);
        }

        protected override void OnUpdating(PreInscriptionSousTraitence entity)
        {
            base.OnUpdating(entity);
        }

        public override Expression<Func<PreInscriptionSousTraitence, object>>[] GetDefaultLoadProperties()
        {
            Expression<Func<PreInscriptionSousTraitence, object>> loadWilaya = x => x.Wilaya;

            return new Expression<Func<PreInscriptionSousTraitence, object>>[] { loadWilaya };
        }
    }
}
