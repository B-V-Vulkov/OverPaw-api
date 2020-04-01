namespace OverPaw.Data.Models.ModelsValidations
{
    internal static class UserValidations
    {
        public const string USER_NAME_EXPRESSION =
            @"^[a-zA-Z0-9]+([a-zA-Z0-9](_|-)[a-zA-Z0-9])*[a-zA-Z0-9]*$";

        public const string EMAIL_EXPRESSION =
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public const int FIRST_NAME_MAX_LENGTH = 80;

        public const int FIRST_NAME_MIN_LENGTH = 1;

        public const int FAMILY_NAME_MAX_LENGTH = 80;

        public const int FAMILY_NAME_MIN_LENGTH = 1;

    }
}
