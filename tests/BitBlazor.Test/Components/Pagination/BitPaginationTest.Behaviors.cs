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

    [Fact]
    public void BitPagination_GetPageSequence_Returns_All_Pages_When_PageRangeSize_Is_Null()
    {
        using var ctx = new BunitContext();

        var component = ctx.Render<BitPagination>(
            p => p.Add(x => x.NumberOfPages, 10)
                  .Add(x => x.Description, "pagination")
                  .Add(x => x.Page, 5));

        var sequence = component.Instance.GetPageSequence().ToList();

        Assert.Equal(10, sequence.Count);
        Assert.DoesNotContain(null, sequence);
    }

    [Fact]
    public void BitPagination_GetPageSequence_Returns_Ellipsis_For_Middle_Page()
    {
        using var ctx = new BunitContext();

        var component = ctx.Render<BitPagination>(
            p => p.Add(x => x.NumberOfPages, 50)
                  .Add(x => x.Description, "pagination")
                  .Add(x => x.Page, 26)
                  .Add(x => x.PageRangeSize, 2));

        var sequence = component.Instance.GetPageSequence().ToList();

        // 1, null, 24, 25, 26, 27, 28, null, 50
        Assert.Equal(9, sequence.Count);
        Assert.Equal(1,sequence[0]);
        Assert.Null(sequence[1]);
        Assert.Equal(24, sequence[2]);
        Assert.Equal(26, sequence[4]);
        Assert.Null(sequence[7]);
        Assert.Equal(50, sequence[8]);
    }

    [Fact]
    public void BitPagination_GetPageSequence_No_Leading_Ellipsis_When_Near_Start()
    {
        using var ctx = new BunitContext();

        var component = ctx.Render<BitPagination>(
            p => p.Add(x => x.NumberOfPages, 50)
                  .Add(x => x.Description, "pagination")
                  .Add(x => x.Page, 2)
                  .Add(x => x.PageRangeSize, 2));

        var sequence = component.Instance.GetPageSequence().ToList();

        Assert.Equal(1, sequence[0]);
        Assert.Equal(2, sequence[1]); // no ellipsis after page 1
        Assert.NotNull(sequence[1]);
    }

    [Fact]
    public void BitPagination_GetPageSequence_No_Trailing_Ellipsis_When_Near_End()
    {
        using var ctx = new BunitContext();

        var component = ctx.Render<BitPagination>(
            p => p.Add(x => x.NumberOfPages, 50)
                  .Add(x => x.Description, "pagination")
                  .Add(x => x.Page, 49)
                  .Add(x => x.PageRangeSize, 2));

        var sequence = component.Instance.GetPageSequence().ToList();

        Assert.Equal(50, sequence[^1]);
        Assert.Equal(49, sequence[^2]); // no ellipsis before last page
        Assert.NotNull(sequence[^2]);
    }
}
