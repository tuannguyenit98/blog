using Abstractions.Interfaces;
using Blog.Controllers;
using Common.Helpers;
using DTOs.Blog.Category;
using DTOs.Share;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Blog.UnitTest
{
    public class CategoriesControllerTest
    {
        // This class is intended for unit tests related to the CategoryController.
        // It will contain methods to test various functionalities of the controller,
        // such as creating, updating, deleting, and retrieving categories.

        // Example test method:
        // [Fact]
        // public void CreateCategory_ShouldReturnCreatedCategory_WhenValidDataProvided()
        // {
        //     // Arrange
        //
        //     // Act
        //
        //     // Assert
        // }
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly CategoriesController _categoryController;
        public CategoriesControllerTest()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _categoryController = new CategoriesController(_categoryServiceMock.Object);
        }

        [Fact]
        public async void Get_Category_By_Id_Test()
        {
            // Arrange
            int categoryId = 1;
            var mockCategory = new CategoryDto
            {
                Id = categoryId,
                CreatedAt = DateTime.UtcNow,
                Title = "Test Category",
                MetaTitle = "test-category",
                Slug = "test-category",
                CreatedBy = 1,
                UpdatedBy = 1,
                ParentId = null
            };
            // Set up the mock service to return the mock category when GetCategoryByIdAsync is called
            _categoryServiceMock.Setup(service => service.GetCategoryByIdAsync(categoryId))
                .ReturnsAsync(mockCategory);

            // Act
            var result = await _categoryController.GetById(categoryId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponseSucess<CategoryDto>>(okResult.Value);
            // Verify the expected outcome
            Assert.Equal("success", apiResponse.status);
            Assert.NotNull(apiResponse.data);
            Assert.Equal(categoryId, apiResponse.data.Id);
            Assert.Equal("Test Category", apiResponse.data.Title); // Placeholder assertion, replace with actual test logic
        }

        [Fact]
        public async void Create_Category_Test()
        {
            // Arrange
            var createCategoryDto = new CreateCategoryDto
            {
                Title = "New Category",
                MetaTitle = "new-category",
                ParentId = null
            };

            // Set up the mock service to return a successful response when CreateCategoryAsync is called
            _categoryServiceMock.Setup(service => service.CreateCategoryAsync(createCategoryDto))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _categoryController.CreateCategory(createCategoryDto);

            // Assert
            var createdResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async void Update_Category_Test()
        {
            // Arrange
            int categoryId = 1;
            var updateCategoryDto = new UpdateCategoryDto
            {
                Title = "Updated Category",
                MetaTitle = "updated-category",
                ParentId = null
            };

            // Set up the mock service to return a successful response when UpdateCategoryAsync is called
            _categoryServiceMock.Setup(service => service.UpdateCategoryAsync(categoryId, updateCategoryDto))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _categoryController.UpdateCategory(updateCategoryDto, categoryId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async void Delete_Category_Test()
        {
            // Arrange
            int categoryId = 1;

            // Set up the mock service to return a successful response when DeleteCategoryAsync is called
            _categoryServiceMock.Setup(service => service.DeleteCategoryAsync(categoryId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _categoryController.DeleteCategory(categoryId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async void Get_All_Categories_Test()
        {
            // Arrange
            var mockCategories = new List<CategoryDto>
            {
                new CategoryDto { Id = 1, Title = "Category 1", Slug = "category-1" },
                new CategoryDto { Id = 2, Title = "Category 2", Slug = "category-2" }
            };

            var pagedResultRequestDto = new PagedResultRequestDto
            {
                Page = 0
            };

            // Set up the mock service to return the mock categories when GetAll is called
            _categoryServiceMock.Setup(service => service.GetAll())
                .ReturnsAsync(mockCategories);

            // Act
            var result = await _categoryController.GetCategories(pagedResultRequestDto, null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponseSucess<List<CategoryDto>>>(okResult.Value);
            Assert.Equal("success", apiResponse.status);
            Assert.NotNull(apiResponse.data);
            Assert.Equal(2, apiResponse.data.Count); // Placeholder assertion, replace with actual test logic
        }

        [Fact]
        public async void Get_Categories_With_Pagination_Test()
        {
            // Arrange
            var mockCategories = new List<CategoryDto>
            {
                new CategoryDto { Id = 1, Title = "Category 1", Slug = "category-1" },
                new CategoryDto { Id = 2, Title = "Category 2", Slug = "category-2" }
            };

            var pagedResultRequestDto = new PagedResultRequestDto
            {
                Page = 1,
                PageSize = 10
            };

            var mockPagedResult = new PagedResultDto<CategoryDto>
            {
                PageIndex = pagedResultRequestDto.Page,
                PageSize = pagedResultRequestDto.PageSize,
                Items = mockCategories.ToArray(),
                TotalCount = mockCategories.Count
            };

            // Set up the mock service to return the mock categories when GetCategories is called
            _categoryServiceMock.Setup(service => service.GetCategories(pagedResultRequestDto, null))
                .ReturnsAsync(mockPagedResult);

            // Act
            var result = await _categoryController.GetCategories(pagedResultRequestDto, null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponseSucess<IPagedResultDto<CategoryDto>>>(okResult.Value);
            Assert.Equal("success", apiResponse.status);
            Assert.NotNull(apiResponse.data);
            Assert.Equal(2, apiResponse.data.TotalCount); // Placeholder assertion, replace with actual test logic
        }
    }
}
