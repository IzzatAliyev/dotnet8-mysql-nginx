namespace backend.services;

using backend.models;

public interface IHomeService
{
    public void CreateBlog(BlogJsonModel blog);
    public BlogJsonModel[] GetBlogs();
}
