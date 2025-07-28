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
            if (_repository.Count(x =>
                                 x.NomFr == entity.NomFr &&
                                 x.PrenomFr == entity.PrenomFr &&
                                 x.NumActNais == entity.NumActNais) > 0)
                throw new BusinessException($"يوجد شخص آخر مسجل بالاسم: {entity.PrenomFr}، اللقب: {entity.NomFr}، رقم ملف: {entity.NumActNais}.");


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
