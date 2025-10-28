using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Utilities.Security
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // PBKDF2 algoritmasını kullanıyoruz.

            // 1. Salt (Tuz) Oluşturma
            // Güvenli, rastgele 16 byte'lık bir 'tuz' oluştur.
            const int saltSize = 16;
            passwordSalt = RandomNumberGenerator.GetBytes(saltSize);

            // 2. Hash (Özet) Oluşturma
            const int keySize = 64; // 64 byte = 512 bit (SHA512 ile uyumlu)
            const int iterations = 350000; // Tekrar sayısı (güvenlik seviyesi)
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512; // Kullandığımız algoritma

            // Rfc2898DeriveBytes, parolayı, salt'ı ve tekrar sayısını kullanarak
            // güvenli bir anahtar (hash) türetir.
            using (var pbkdf2 = new Rfc2898DeriveBytes(
                Encoding.UTF8.GetBytes(password), // Parolayı byte dizisine çevir
                passwordSalt,                     // Oluşturulan salt'ı kullan
                iterations,
                hashAlgorithm))
            {
                // İstenen boyutta (keySize) hash'i oluştur
                passwordHash = pbkdf2.GetBytes(keySize);
            }
        }
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            // Hash'i doğrulamak için, kayıt olurken kullandığımız 
            // BİREBİR AYNI parametreleri kullanmalıyız.
            const int keySize = 64;
            const int iterations = 350000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                Encoding.UTF8.GetBytes(password), // Giriş yapılan parolayı kullan
                storedSalt,                       // Veritabanındaki salt'ı kullan
                iterations,
                hashAlgorithm))
            {
                // Parola ve salt'tan yeni bir hash hesapla
                byte[] computedHash = pbkdf2.GetBytes(keySize);

                // 'Zamanlama saldırılarına' (timing attacks) karşı KORUMALI
                // güvenli karşılaştırma yap. 
                // Asla 'if (computedHash == storedHash)' gibi bir karşılaştırma yapma!
                return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
            }
        }
    }
}
