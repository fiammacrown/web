using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Abeslamidze_Web.Controllers;
using Abeslamidze_Web.DAL.Entities;
using Xunit;

namespace Abeslamidze_Web.Tests;

public class ProductControllerTests
{
    [Theory]
    [MemberData(nameof(TestData.Params), MemberType =
typeof(TestData))]
    public void ControllerGetsProperPage(int page, int qty, int id)
    {
        // Arrange
        var controller = new ProductController();
        controller._dishes = TestData.GetDishesList();
        // Act
        var result = controller.Index(pageNo: page, group: null) as ViewResult;
        var model = result?.Model as List<Dish>;
        // Assert
        Assert.NotNull(model);
        Assert.Equal(qty, model.Count);
        Assert.Equal(id, model[0].DishId);
    }

    [Fact]
    public void ControllerSelectsGroup()
    {
        // arrange
        var controller = new ProductController();
        var data = TestData.GetDishesList();
        controller._dishes = data;
        var comparer = Comparer<Dish>
            .GetComparer((d1, d2) => d1.DishId.Equals(d2.DishId));

        // act
        var result = controller.Index(2) as ViewResult;

        var model = result.Model as List<Dish>;
        // assert
        Assert.Equal(2, model.Count);
        Assert.Equal(data[2], model[0], comparer);
    }
}