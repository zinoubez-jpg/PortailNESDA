using Ansej.Business.Core;
using Ansej.Data.Abstractions;
using Ansej.Sidjeme.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ansej.Sidjeme.Business
{
    public class ContactUsBusinessService : GenericBusinessService<ContactUs, int>
    {
        public ContactUsBusinessService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        protected override void OnAdding(ContactUs entity)
        {
            base.OnAdding(entity);
        }

        protected override void OnUpdating(ContactUs entity)
        {
            base.OnUpdating(entity);
        }

        public override Expression<Func<ContactUs, object>>[] GetDefaultLoadProperties()
        {
            Expression<Func<ContactUs, object>> loadWilaya = x => x.Wilaya;

            return new Expression<Func<ContactUs, object>>[] { loadWilaya };
        }
    }
}
