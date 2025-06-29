using Ansej.Business.Core;
using Ansej.Data.Abstractions;
using System;

namespace Ansej.Sidjeme.Identity
{
    public class UserRoleService
    {
        protected readonly IUnitOfWork<IdentityContext> _unitOfWork;
        protected readonly IRepository<UserRole, int> _repository;

        public UserRoleService(IUnitOfWork<IdentityContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<UserRole, int>();
        }
        public BusinessResult Add(UserRole entity)
        {
            BusinessResult businessResult;

            try
            {
                OnAdding(entity);
                _repository.Add(entity);
                if (_unitOfWork.SaveChanges() == 0)
                    throw new DataNotUpdatedException("Opération non enregsitrée !");
                OnAdded(entity);
                businessResult = BusinessResult.Success;
            }
            catch (DataNotUpdatedException ex)
            {
                businessResult = new BusinessResult();
                businessResult.Messages.Add(new MessageResult { Message = ex.Message, MessageType = MessageType.Warning });
            }
            catch (BusinessException ex)
            {
                businessResult = new BusinessResult();
                businessResult.Messages.Add(new MessageResult { Message = ex.Message, MessageType = MessageType.Warning });
            }
            catch (Exception)
            {
                businessResult = new BusinessResult();
                businessResult.Messages.Add(new MessageResult { Message = "Un erreur s'est produite réessayez, si cela persiste contactez votre administrateur ", MessageType = MessageType.Warning });
            }

            return businessResult;
        }

        protected virtual void OnAdded(UserRole entity)
        {

        }

        protected virtual void OnAdding(UserRole entity)
        {

        }



    }
}

