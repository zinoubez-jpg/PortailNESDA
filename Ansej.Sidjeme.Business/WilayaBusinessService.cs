using Ansej.Business.Core;
using Ansej.Data.Abstractions;
using Ansej.Sidjeme.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ansej.Sidjeme.Business
{
    public class WilayaBusinessService : GenericBusinessService<Wilaya, int>
    {

        public WilayaBusinessService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        protected override void OnAdding(Wilaya entity)
        {
            base.OnAdding(entity);
        }

        protected override void OnUpdating(Wilaya entity)
        {
            base.OnUpdating(entity);
        }

        public override Expression<Func<Wilaya, object>>[] GetDefaultLoadProperties()
        {
            Expression<Func<Wilaya, object>> loadContactUs = x => x.ContactUs;

            return new Expression<Func<Wilaya, object>>[] { loadContactUs };
        }
    }
}
