using AngleSharp.Dom;
using BitBlazor.Components;
using Bunit;
using Newtonsoft.Json.Linq;

namespace BitBlazor.Test.Components.Pagination;

public class BitPaginationTest
{
    [Fact]
    public void BitPagination_Should_Change_Current_Page_When_Page_Link_Is_Clicked()
    {
        using var ctx = new BunitContext();

        int page = 1;

        var component = ctx.Render<BitPagination>(
            parameters => parameters
                .Add(p => p.NumberOfPages, 3)
                .Add(p => p.Description, "pagination")
                .Bind(p => p.Page, page, v => page = v));

        var pageItem = component.FindComponents<BitPageItem>().First(p => p.Instance.Page == 2);
        var pageLink = pageItem.Find(".page-item > .page-link");

        pageLink.Click();

        Assert.Equal(2, page);
        Assert.Equal("page", pageLink.GetAttribute("aria-current"));
    }

    [Fact]
    public void BitPagination_Should_Move_To_Previous_Page_When_PreviousPage_Link_Is_Clicked()
    {
        using var ctx = new BunitContext();

        int page = 2;

        var component = ctx.Render<BitPagination>(
            parameters => parameters
                .Add(p => p.NumberOfPages, 3)
                .Add(p => p.Description, "pagination")
                .Bind(p => p.Page, page, v => page = v));

        var pageLink = component.FindAll(".page-item > .page-link").First();
        pageLink.Click();

        Assert.Equal(1, page);
    }

    [Fact]
    public void BitPagination_Should_Move_To_Next_Page_When_NextPage_Link_Is_Clicked()
    {
        using var ctx = new BunitContext();

        int page = 2;

        var component = ctx.Render<BitPagination>(
            parameters => parameters
                .Add(p => p.NumberOfPages, 3)
                .Add(p => p.Description, "pagination")
                .Bind(p => p.Page, page, v => page = v));

        var pageLink = component.FindAll(".page-item > .page-link").Last();
        pageLink.Click();

        Assert.Equal(3, page);
    }

    [Fact]
    public void BitPagination_Should_Not_Change_Page_When_PreviousPage_Link_Is_Clicked_And_Current_Page_Is_First_Page()
    {
        using var ctx = new BunitContext();

        int page = 1;

        var component = ctx.Render<BitPagination>(
            parameters => parameters
                .Add(p => p.NumberOfPages, 3)
                .Add(p => p.Description, "pagination")
                .Bind(p => p.Page, page, v => page = v));

        var pageLink = component.FindAll(".page-item > .page-link").First();
        pageLink.Click();

        Assert.Equal(1, page);
    }

    [Fact]
    public void BitPagination_Should_Not_Change_Page_When_NextPage_Link_Is_Clicked_And_Current_Page_Is_Last_Page()
    {
        using var ctx = new BunitContext();

        int page = 3;

        var component = ctx.Render<BitPagination>(
            parameters => parameters
                .Add(p => p.NumberOfPages, 3)
                .Add(p => p.Description, "pagination")
                .Bind(p => p.Page, page, v => page = v));

        var pageLink = component.FindAll(".page-item > .page-link").Last();
        pageLink.Click();

        Assert.Equal(3, page);
    }
}
