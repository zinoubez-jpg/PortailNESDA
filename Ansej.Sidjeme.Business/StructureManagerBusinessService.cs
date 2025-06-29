using Ansej.Business.Core;
using Ansej.Data.Abstractions;
using Ansej.Sidjeme.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ansej.Sidjeme.Business
{
    public class StructureManagerBusinessService : GenericBusinessService<StructureManager, int>
    {

        public StructureManagerBusinessService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        protected override void OnAdding(StructureManager entity)
        {
            base.OnAdding(entity);
        }

        protected override void OnUpdating(StructureManager entity)
        {
            base.OnUpdating(entity);
        }

        public override Expression<Func<StructureManager, object>>[] GetDefaultLoadProperties()
        {
            Expression<Func<StructureManager, object>> loadConsultations = x => x.Consultations;
            Expression<Func<StructureManager, object>> subStructures = x => x.SubStructures;

            return new Expression<Func<StructureManager, object>>[] { loadConsultations, subStructures };
        }

    }
}