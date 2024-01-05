namespace backend.controllers;

using backend.models;
using backend.services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    private IHomeService homeService;
    public HomeController(IHomeService homeService)
    {
        this.homeService = homeService;
    }

    [HttpGet]
    [Produces(typeof(BlogJsonModel[]))]
    public ObjectResult GetBlogs()
    {
        return this.Ok(this.homeService.GetBlogs());
    }

    [HttpPost]
    [Consumes("application/json")]
    public string CreateBlog(BlogJsonModel blog)
    {
        this.homeService.CreateBlog(blog);
        return "Successfully created";
    }
}
