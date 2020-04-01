namespace OverPaw.Services.Models
{
    using System;
    using System.Collections.Generic;

    public class TestUserServiceModel
    {
        public TestUserServiceModel()
        {
            this.Posts = new HashSet<PostServiseModel>();
        }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public IEnumerable<PostServiseModel> Posts { get; set; }
    }
}
