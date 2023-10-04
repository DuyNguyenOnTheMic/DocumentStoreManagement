using DocumentStoreManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DocumentStoreManagement.Controllers
{
    /// <summary>
    /// Base controller for databases which need UnitOfWork
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Base unit of work
        /// </summary>
        protected IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for unit of work
        /// </summary>
        /// <param name="unitOfWork"></param>
        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
