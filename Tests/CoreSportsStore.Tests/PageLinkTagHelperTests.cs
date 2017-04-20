﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CoreSportsStore.Infrastructure;
using CoreSportsStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CoreSportsStore.Tests
{
    [TestClass]
    public class PageLinkTagHelperTests
    {
        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Page1")
                .Returns("Test/Page2")
                .Returns("Test/Page3");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);

            var helper = new PageLinkTagHelper(urlHelperFactory.Object)
            {
                PageModel = new PagingInfo
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    ItemsPerPage = 10
                },
                PageAction = "Test"
            };

            var ctx = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                "");

            var content = new Mock<TagHelperContent>();

            var output = new TagHelperOutput(
                "div",
                new TagHelperAttributeList(),
                (cache, encoder) => Task.FromResult(content.Object));

            helper.Process(ctx, output);

            Assert.AreEqual(
                  @"<a href=""Test/Page1"">1</a>"
                + @"<a href=""Test/Page2"">2</a>"
                + @"<a href=""Test/Page3"">3</a>",
                output.Content.GetContent());
        }
    }
}
