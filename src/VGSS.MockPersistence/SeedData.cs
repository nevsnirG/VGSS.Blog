using VGSS.Domain;

namespace VGSS.MockPersistence;
public static class SeedData
{
    public static List<User> Users { get; } = new();
    public static List<BlogPost> BlogPosts { get; } = new();

    static SeedData()
    {
        for (var i = 0; i < 10; i++)
        {
            var user = User.New("user" + i);
            var blogPost = user.PostNewBlogPost("Title" + i, "Content" + i);

            Users.Add(user);
            BlogPosts.Add(blogPost);
        }
    }
}
