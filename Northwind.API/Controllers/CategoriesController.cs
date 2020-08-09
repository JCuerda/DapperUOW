using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.DAL.Core;
using Northwind.DAL.Core.Domain;


namespace Northwind.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            var categories = new List<Category>();

            using(_unitOfWork) 
            {
                _unitOfWork.Begin();
                categories = _unitOfWork.CategoryRepository.GetAll().ToList();
                _unitOfWork.Commit();
            }

            return categories;
        }
    }
}