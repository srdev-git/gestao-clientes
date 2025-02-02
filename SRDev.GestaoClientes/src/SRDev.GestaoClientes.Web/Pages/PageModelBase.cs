using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

public abstract class PageModelBase : PageModel
{
    public string Token { get; private set; }

    public override void OnPageHandlerExecuting(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext context)
    {
        Token = HttpContext.Session.GetString("AuthToken");

        if (string.IsNullOrEmpty(Token))
        {
            context.Result = new RedirectToPageResult("/Autenticacao/Login");
        }

        base.OnPageHandlerExecuting(context);
    }
}