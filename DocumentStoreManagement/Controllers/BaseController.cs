using DocumentStoreManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DocumentStoreManagement.Controllers
{
    /// <summary>
    /// Base controller for databases which need UnitOfWork
    /// </summary>
    /// <remarks>
    /// Constructor for unit of work
    /// </remarks>
    /// <param name="unitOfWork"></param>
    public class BaseController(IUnitOfWork unitOfWork) : ControllerBase
    {
        /// <summary>
        /// Base unit of work
        /// </summary>
        protected IUnitOfWork _unitOfWork = unitOfWork;
    }
}
