namespace OverPaw.Data.Models
{
    using System;

    public class User
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string HashedPassword { get; set; }

        public string FirstName { get; set; }

        public string FamilyName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
