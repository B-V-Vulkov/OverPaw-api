namespace OverPaw.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        [Key]
        public Guid PostId { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }

        public string Name { get; set; }
    }
}
