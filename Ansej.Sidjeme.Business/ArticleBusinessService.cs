using Ansej.Business.Core;
using Ansej.Data.Abstractions;
using Ansej.Sidjeme.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ansej.Sidjeme.Business
{
    public class ArticleBusinessService : GenericBusinessService<Article, int>
    {
        public ArticleBusinessService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override void OnAdding(Article entity)
        {
            if (_repository.Count(x => x.Slug == entity.Slug) > 0)
                throw new BusinessException("Un article ayant le même lien existe déja!");
            //if (_repository.Count(x => x.IsPinned) > 3)
            //    throw new BusinessException("Un article ayant le même lien existe déja!");
            base.OnAdding(entity);
        }

        protected override void OnUpdating(Article entity)
        {
            if (_repository.Count(x =>x.Id != entity.Id && x.Slug == entity.Slug) > 0)
                throw new BusinessException("Un article ayant le même lien existe déja!");
            //if (_repository.Count(x => x.IsPinned) > 3)
            //    throw new BusinessException("Un article ayant le même lien existe déja!");
            base.OnUpdating(entity);
        }
    }
}
