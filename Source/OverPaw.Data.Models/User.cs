namespace OverPaw.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelsValidations.UserValidations;

    public class User
    {
        public User()
        {
            this.Posts = new HashSet<Post>();
        }

        [Key]
        public Guid UserId { get; set; }

        [Required]
        [RegularExpression(USER_NAME_EXPRESSION)]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(EMAIL_EXPRESSION)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(FIRST_NAME_MAX_LENGTH)]
        [MinLength(FIRST_NAME_MIN_LENGTH)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(FAMILY_NAME_MAX_LENGTH)]
        [MinLength(FAMILY_NAME_MIN_LENGTH)]
        public string FamilyName { get; set; }

        public int Age { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}
