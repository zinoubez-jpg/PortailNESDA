using Ansej.Business.Core;
using Ansej.Data.Abstractions;
using Ansej.Sidjeme.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ansej.Sidjeme.Business
{
    public class JournalBusinessService : GenericBusinessService<Journal, int>
    {
        public JournalBusinessService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { 

        }

         protected override void OnAdding(Journal entity)
        {
            base.OnAdding(entity);
        }

        protected override void OnUpdating(Journal entity)
        {
            base.OnUpdating(entity);
        }

        public override Expression<Func<Journal, object>>[] GetDefaultLoadProperties()
        {
            return new Expression<Func<Journal, object>>[] {  };
        }
    }
    }

