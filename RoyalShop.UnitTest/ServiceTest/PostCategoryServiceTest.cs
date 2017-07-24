using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RoyalShop.Data.Infrastructure;
using RoyalShop.Data.Repositories;
using RoyalShop.Model.Models;
using RoyalShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalShop.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostCategoryService _postCategoryService;
        private List<PostCategory> _listPostCategory;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IPostCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _postCategoryService = new PostCategoryService(_mockRepository.Object, _mockUnitOfWork.Object);
            _listPostCategory = new List<PostCategory>()
            {
                new PostCategory() {ID = 1, Name="Test1",Status=true },
                new PostCategory() {ID = 2, Name="Test2",Status=true },
                new PostCategory() {ID = 3, Name="Test3",Status=true }
            };
        }

        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            //setup method
            _mockRepository.Setup(m => m.GetAll(null)).Returns(_listPostCategory);

            //call action
            var resuilt = _postCategoryService.GetAll() as List<PostCategory>;

            //compare
            Assert.IsNotNull(resuilt);
            Assert.AreEqual(3, resuilt.Count);
        }

        [TestMethod]
        public void PostCategory_Service_Create()
        {
            PostCategory postCategory = new PostCategory();
            postCategory.Name = "Test";
            postCategory.Alias = "Test";
            postCategory.Status = true;

            _mockRepository.Setup(m => m.Add(postCategory)).Returns((PostCategory p) =>
              {
                  p.ID = 1;
                  return p;
              });

            var resuilt = _postCategoryService.Add(postCategory);

            Assert.IsNotNull(resuilt);
            Assert.AreEqual(1,resuilt.ID);
        }
    }
}
