using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using SocialMedia.Services.Interfaces;
using SocialMedia.ViewModels.Email;
using System.Security.Claims;
using HomeController = SocialMedia.Controllers.HomeController;

namespace SocialMedia.Tests.Controllers
{
    public class HomeControllerTests : BaseTest
    {
        private HomeController homeController;
        public HomeControllerTests()
        {
            var userMock = new Mock<ClaimsPrincipal>();
            var controllerContext = new ControllerContext();
            userMock.Setup(um => um.Identity!.IsAuthenticated).Returns(true);
            userMock.Setup(i => i.FindFirst(ClaimTypes.Email)).Returns(new Claim(ClaimTypes.Email, "john@yahoo.com"));
            controllerContext.HttpContext = new DefaultHttpContext()
            {
                User = userMock.Object,
            };

            var loggerMock = new Mock<ILogger<HomeController>>();
            var customEmailSenderMock = new Mock<ICustomEmailSender>();
            customEmailSenderMock
                .Setup(ce => ce.SendEmailAsync
                (
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>())
                )
                .ReturnsAsync(true);

            homeController = new HomeController(loggerMock.Object, customEmailSenderMock.Object);
            homeController.ControllerContext = controllerContext;
            homeController.TempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>());
        }

        [Fact]
        public void GetIndexAction_ShouldReturnViewResult()
        {
            var result = this.homeController.Index();

            Assert.IsType<ViewResult>(result);
        }

        //Privacy is commented out because later it will be deleted from the app
        //[Fact]
        //public void GetPrivacyAction_ShouldReturnViewResult()
        //{
        //    var result = this.homeController.Privacy();

        //    Assert.IsType<ViewResult>(result);
        //}

        [Fact]
        public void GetContactAction_ShouldReturnViewResultWithContactModel()
        {
            var result = this.homeController.Contact() as ViewResult;
            var model = (ContactFormModel)result.Model;

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.IsType<ContactFormModel>(model);
            Assert.Equal("john@yahoo.com", model.FromEmail);
        }

        [Fact]
        public async Task PostContactAction_ShouldReturnViewResultWhenModelStateInvalid()
        {
            this.homeController.ModelState.AddModelError("", "Error");
            var contactFormModel = new ContactFormModel()
            {
                FromEmail = "john@yahoo.com"
            };

            var result = await this.homeController.Contact(contactFormModel) as ViewResult;
            var model = (ContactFormModel)result.Model;

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.IsType<ContactFormModel>(model);
            Assert.Equal("john@yahoo.com", model.FromEmail);
        }

        [Fact]
        public async Task PostContactAction_ShouldRedirectWhenModelStateIsValid()
        {
            var contactFormModel = new ContactFormModel()
            {
                FromEmail = "john@yahoo.com"
            };

            var result = await this.homeController.Contact(contactFormModel) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void GetErrorAction_ShouldReturnBadRequestCustomErrorPageWithStatusCode400()
        {
            var result = this.homeController.Error(400) as ViewResult;

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal("Error400", result.ViewName);
        }

        [Fact]
        public void GetErrorAction_ShouldReturnUnauthorizedCustomErrorPageWithStatusCode401()
        {
            var result = this.homeController.Error(401) as ViewResult;

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal("Error401", result.ViewName);
        }

        [Fact]
        public void GetErrorAction_ShouldReturnPageNotFoundCustomErrorPageWithStatusCode404()
        {
            var result = this.homeController.Error(404) as ViewResult;

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal("Error404", result.ViewName);
        }

        [Fact]
        public void GetErrorAction_ShouldReturnInternalServerErrorCustomErrorPageWithStatusCode500()
        {
            var result = this.homeController.Error(500) as ViewResult;

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal("Error500", result.ViewName);
        }
    }
}
