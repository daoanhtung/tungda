using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyWebsite.Utils
{
    public class Encryption
    {
        private static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public static string EncryptText(string input)
        {
            try
            {
                if (String.IsNullOrEmpty(input))
                {
                    return input;
                }
                // Get the bytes of the string
                byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(Constants.EncryptPassword);

                // Hash the password with SHA256
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

                string result = Convert.ToBase64String(bytesEncrypted);

                return result;
            }
            catch (Exception)
            {
                return input;
            }
        }

        public static object DecryptText(string input)
        {
            try
            {
                // Get the bytes of the string
                byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(Constants.EncryptPassword);
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

                string result = Encoding.UTF8.GetString(bytesDecrypted);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message,false);
                return input;
            }
        }

        public static T Encrypt<T>(object fromSource) where T : class, new()
        {
            var ret = new T();
            if (fromSource == null)
            {
                return ret;
            }
            var sourceProps = fromSource.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.GetCustomAttributes(typeof(EncryptAttribute), true).Length > 0);
            var destinationProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var desProp in destinationProps)
            {
                var sourceProp = sourceProps.FirstOrDefault(m => m.Name == desProp.Name);

                if (sourceProp != null && desProp.SetMethod != null)
                {
                    var sourceValue = sourceProp.GetValue(fromSource, null);
                    if (sourceValue != null)
                    {
                        desProp.SetValue(ret, EncryptText(sourceProp.GetValue(fromSource, null).ToString()), null);
                    }
                }
            }

            sourceProps = fromSource.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.GetCustomAttributes(typeof(EncryptAttribute), true).Length == 0);

            foreach (var desProp in destinationProps)
            {
                var sourceProp = sourceProps.FirstOrDefault(m => m.Name == desProp.Name);

                if (sourceProp != null && desProp.SetMethod != null)
                {
                    var sourceValue = sourceProp.GetValue(fromSource, null);
                    if (sourceValue != null)
                    {
                        desProp.SetValue(ret, sourceProp.GetValue(fromSource, null), null);
                    }
                }
            }

            return ret;
        }

        public static T Decrypt<T>(object fromSource) where T : class, new()
        {
            var ret = new T();
            if (fromSource == null)
            {
                return ret;
            }
            var sourceProps = fromSource.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.GetCustomAttributes(typeof(EncryptAttribute), true).Length > 0);
            var destinationProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var desProp in destinationProps)
            {
                var sourceProp = sourceProps.FirstOrDefault(m => m.Name == desProp.Name);
                
                if (sourceProp != null && desProp.SetMethod != null)
                {
                    var sourceValue = sourceProp.GetValue(fromSource, null);
                    if (sourceValue != null)
                    {
                        desProp.SetValue(ret, DecryptText(sourceProp.GetValue(fromSource, null).ToString()), null);
                    }
                }
            }

            sourceProps = fromSource.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.GetCustomAttributes(typeof(EncryptAttribute), true).Length == 0);

            foreach (var desProp in destinationProps)
            {
                var sourceProp = sourceProps.FirstOrDefault(m => m.Name == desProp.Name);

                if (sourceProp != null && desProp.SetMethod != null)
                {
                    var sourceValue = sourceProp.GetValue(fromSource, null);
                    if (sourceValue != null)
                    {
                        desProp.SetValue(ret, sourceProp.GetValue(fromSource, null), null);
                    }
                }
            }
            return ret;
        }
    }
}
