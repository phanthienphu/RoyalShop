using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoyalShop.Data.Infrastructure;
using RoyalShop.Data.Repositories;
using RoyalShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalShop.UnitTest.RepositoryTest
{
    [TestClass]
    public class PostCategoryRepositoryTest
    {
        IDbFactory dbFactory;
        IPostCategoryRepository objRepository;
        IUnitOfWork unitOfWork;

        //khởi tạo phương thức chạy đầu tiên dùng để khởi tạo, thêm dữ liệu
        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new PostCategoryRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        [TestMethod]
        public void PostCategory_Repository_GetAll()
        {
            var list = objRepository.GetAll().ToList();

            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void PostCategory_Repository_Create()
        {
            PostCategory category = new PostCategory();
            category.Name = "Test Category";
            category.Alias = "TestCategory";
            category.Status = true;

            var resuilt = objRepository.Add(category);
            unitOfWork.Commit();

            Assert.IsNotNull(resuilt.ID);
            Assert.AreEqual(3, resuilt.ID);
        }
    }
}
