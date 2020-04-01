namespace OverPaw.Security
{
    using System;
    using System.Security.Cryptography;

    public static class PasswordHasher
    {
        private const int PBKDF2_ITERATION_COUNT = 10000;
        private const int PBKDF2_SUBKEY_LENGTH = 256 / 8;
        private const int SALT_SIZE = 128 / 8;

        public static string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            // Produce a version 0 (see comment above) text hash.
            byte[] salt;
            byte[] subkey;

            using (var deriveBytes = new Rfc2898DeriveBytes(password, SALT_SIZE, PBKDF2_ITERATION_COUNT))
            {
                salt = deriveBytes.Salt;
                subkey = deriveBytes.GetBytes(PBKDF2_SUBKEY_LENGTH);
            }

            var outputBytes = new byte[1 + SALT_SIZE + PBKDF2_SUBKEY_LENGTH];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SALT_SIZE);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SALT_SIZE, PBKDF2_SUBKEY_LENGTH);
            return Convert.ToBase64String(outputBytes);
        }

        // hashedPassword must be of the format of HashWithPassword (salt + Hash(salt+input)
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            // Verify a version 0 (see comment above) text hash.

            if (hashedPasswordBytes.Length != (1 + SALT_SIZE + PBKDF2_SUBKEY_LENGTH) || hashedPasswordBytes[0] != 0x00)
            {
                // Wrong length or version header.
                return false;
            }

            var salt = new byte[SALT_SIZE];
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SALT_SIZE);
            var storedSubkey = new byte[PBKDF2_SUBKEY_LENGTH];
            Buffer.BlockCopy(hashedPasswordBytes, 1 + SALT_SIZE, storedSubkey, 0, PBKDF2_SUBKEY_LENGTH);

            byte[] generatedSubkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, PBKDF2_ITERATION_COUNT))
            {
                generatedSubkey = deriveBytes.GetBytes(PBKDF2_SUBKEY_LENGTH);
            }
            return ByteArraysEqual(storedSubkey, generatedSubkey);
        }

        // Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized.
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }
    }
}
