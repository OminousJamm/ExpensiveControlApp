using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize]
public class HomeModel : PageModel
{
    public void OnGet()
    {
    }
}
