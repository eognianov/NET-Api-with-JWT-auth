using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Domain;

namespace WebApplication.Services
{
    class PostService : IPostService
    {
        private readonly List<Post> _posts;

        public PostService()
        {
            _posts = new List<Post>();
            for (int i = 0; i < 5; i++)
            {
                _posts.Add(new Post(){Id = Guid.NewGuid(), Name = $"Post name {i}"});
            }
        }

        public List<Post> GetPosts()
        {
            return _posts;
        }

        public Post GetPostById(Guid id)
        {
            return _posts.SingleOrDefault(p => p.Id == id);
        }

        public void Add(Post post)
        {
            _posts.Add(post);
        }
    }
}