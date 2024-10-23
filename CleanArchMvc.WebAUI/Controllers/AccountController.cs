using CleanArchMvc.Communication.Request;
using CleanArchMvc.Domain.Account;

using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebAUI.Controllers;

public class AccountController(IAuthenticate authenticate) : Controller
{
    private readonly IAuthenticate _authenticate = authenticate;

    [HttpGet]
    public IActionResult Login(string returnUrl = "Index")
    {
        return View(new LoginRequest()
        {
            ReturnUrl = returnUrl
        }); 
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _authenticate.Authenticate(request.Email, request.Password);

        if (result)
        {
            if (string.IsNullOrWhiteSpace(request.ReturnUrl))
                return RedirectToAction("Index", "Home");

            return Redirect(request.ReturnUrl);
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt. (Password must be strong).");
        return View(request);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _authenticate.RegisterUser(request.Email, request.Password);

        if (result)
            return Redirect("/");

        ModelState.AddModelError(string.Empty, "Invalid register attempt. (Password must be strong).");
        return View(request);
    }

    public async Task<IActionResult> Logout()
    {
        await _authenticate.Logout();
        return Redirect("/Account/Login");
    }
}
