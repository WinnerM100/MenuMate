using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
 
namespace MenuMate.Utilities
{
    public class SaltHash
    {
        public static string ComputeHash(string plainText, string hashAlgorithm, byte[] saltBytes)
        {
            // If salt is not specified, generate it.
            if (saltBytes == null)
            {
                // Define min and max salt sizes.
                int minSaltSize = 8;
                int maxSaltSize = 16;
 
                // Generate a random number for the size of the salt.
                Random random = new Random();
                int saltSize = random.Next(minSaltSize, maxSaltSize);
 
                // Allocate a byte array, which will hold the salt.
                saltBytes = new byte[saltSize];
 
                // Fill the salt with cryptographically strong byte values.
                
                RandomNumberGenerator.Fill(saltBytes);
            }
 
            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
 
            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes =
            new byte[plainTextBytes.Length + saltBytes.Length];
 
            // Copy plain text bytes into resulting array.
            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];
 
            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];
 
            HashAlgorithm hash;
 
            // Initialize appropriate hashing algorithm class.
            hash = HashAlgorithm.Create(string.IsNullOrEmpty(hashAlgorithm.ToUpper())?"MD5":hashAlgorithm.ToUpper());
            
            if(hash == null)
            {
                throw new Exception($"Hash algorithm incorrect! The input was: {hashAlgorithm}");
            }

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);
 
            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
            saltBytes.Length];
 
            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];
 
            // Append salt bytes to the result.
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];
 
            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);
 
            // Return the result.
            return hashValue;
        }
 
        public static bool VerifyHash(string plainText, string hashAlgorithm, string hashValue)
        {
 
            // Convert base64-encoded hash value into a byte array.
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);
 
            // We must know size of hash (without salt).
            int hashSizeInBits, hashSizeInBytes;
 
            // Make sure that hashing algorithm name is specified.
            if (hashAlgorithm == null)
                hashAlgorithm = "";
 
            // Size of hash is based on the specified algorithm.
            switch (hashAlgorithm.ToUpper())
            {
 
                case "SHA384":
                    hashSizeInBits = 384;
                    break;
 
                case "SHA512":
                    hashSizeInBits = 512;
                    break;
 
                default: // Must be MD5
                    hashSizeInBits = 128;
                    break;
            }
 
            // Convert size of hash from bits to bytes.
            hashSizeInBytes = hashSizeInBits / 8;
 
            // Make sure that the specified hash value is long enough.
            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;
 
            // Allocate array to hold original salt bytes retrieved from hash.
            byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];
 
            // Copy salt from the end of the hash to the new array.
            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];
 
            // Compute a new hash string.
            string expectedHashString = ComputeHash(plainText, hashAlgorithm, saltBytes);
 
            // If the computed hash matches the specified hash,
            // the plain text value must be correct.
            return (hashValue == expectedHashString);
        }
    }
}