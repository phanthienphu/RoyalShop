using RoyalShop.Data.Infrastructure;
using RoyalShop.Data.Repositories;
using RoyalShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalShop.Service
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory productCategory);

        void Update(ProductCategory productCategory);

        ProductCategory Delete(int id);

        IEnumerable<ProductCategory> GetAll();
        IEnumerable<ProductCategory> GetAll(string keyword);

        IEnumerable<ProductCategory> GetAllByParentId(int parentId);

        ProductCategory GetById(int id);

        void Save();
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository _ProductCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._ProductCategoryRepository = productCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public ProductCategory Add(ProductCategory productCategory)
        {
            return _ProductCategoryRepository.Add(productCategory);
        }

        public ProductCategory Delete(int id)
        {
            return _ProductCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _ProductCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _ProductCategoryRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _ProductCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAllByParentId(int parentId)
        {
            return _ProductCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
        }

        public ProductCategory GetById(int id)
        {
            return _ProductCategoryRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategory productCategory)
        {
            _ProductCategoryRepository.Update(productCategory);
        }
    }
}
