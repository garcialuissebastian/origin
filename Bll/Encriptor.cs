using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Bll
{
    public class Encriptor
    {

        private Encriptor()
        {
        }

        private static Encriptor instancia;

        public static Encriptor DameInstancia()
        {
            if ((instancia == null))
            {
                return new Encriptor();
            }
            else
            {
                return instancia;
            }

        }


        string saltValue = "hytbdgdej";
        string hashAlgorithm = "MD5";
        int passwordIterations = 1;
        string initVector = "@1B2c3D4e5F6g7H8";
        static int keySize = 192;
        public string EncryptString2020(string name)
        {

            string nm = String.Empty;
            string val = String.Empty;

            nm = name;
            val = Encrypt(nm, "%jetsbdh9");


            return val;
        }

        public string DecryptString2020(string name)
        {

            string nm;
            nm = DecryptString(name, "%jetsbdh9");


            return nm;
        }
        public string DecryptString(string cipherText, string passPhrase)
        {
            try
            {

                if ((cipherText == String.Empty))
                {
                    return "";
                }
                else
                {
                    byte[] initVectorBytes;
                    initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                    byte[] saltValueBytes;
                    saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                    byte[] cipherTextBytes = new byte[Convert.ToInt32(cipherText.Length / 2)];

                    int X;
                    for (X = 0; (X <= (cipherTextBytes.Length - 1)); X++)
                    {
                        Int32 IJ = Convert.ToInt32(cipherText.Substring((X * 2), 2), 16);
                        System.ComponentModel.ByteConverter BT = new System.ComponentModel.ByteConverter();
                        cipherTextBytes[X] = new byte();
                        cipherTextBytes[X] = ((byte)(BT.ConvertTo(IJ, typeof(byte))));
                    }

                    PasswordDeriveBytes password;
                    password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);


                    byte[] keyBytes;
                    keyBytes = password.GetBytes((keySize / 8));

                    RijndaelManaged symmetricKey;
                    symmetricKey = new RijndaelManaged();
                    symmetricKey.Padding = PaddingMode.Zeros;

                    symmetricKey.Mode = CipherMode.CBC;

                    ICryptoTransform decryptor;
                    decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

                    MemoryStream memoryStream;
                    memoryStream = new MemoryStream(cipherTextBytes);

                    CryptoStream cryptoStream;
                    cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                    byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                    int decryptedByteCount;
                    decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);


                    StringBuilder ret = new StringBuilder();
                    byte[] B = memoryStream.ToArray();
                    //  Close both streams.
                    memoryStream.Close();
                    cryptoStream.Close();

                    string plainText;
                    plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

                    return plainText;


                }


            }
            catch (Exception)
            {

                throw;
            }
        }


        public string Encrypt(string plainText, string passPhrase)
        {
            byte[] initVectorBytes;
            initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes;
            saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] plainTextBytes;
            plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password;
            password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes;
            keyBytes = password.GetBytes((keySize / 8));
            RijndaelManaged symmetricKey;
            symmetricKey = new RijndaelManaged();
            symmetricKey.Padding = PaddingMode.Zeros;
            //  It is reasonable to set encryption mode to Cipher Block Chaining
            //  (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor;
            encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream;
            memoryStream = new MemoryStream();
            CryptoStream cryptoStream;
            cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            //  Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            //  Finish encrypting.
            cryptoStream.FlushFinalBlock();


            byte[] cipherTextBytes;
            cipherTextBytes = memoryStream.ToArray();

            //  Close both streams.
            cryptoStream.Close();
            memoryStream.Close();

            // Get the data back from the memory stream, and into a string
            StringBuilder ret = new StringBuilder();

            byte[] b = cipherTextBytes;

            int I;
            for (I = 0; (I <= b.Length - 1); I++)
            {
                // Format as hex
                ret.AppendFormat("{0:X2}", b[I]);
            }
            return ret.ToString();

        }


        public string GetMD5(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString().ToUpper();
        }
        public string GetHashMD5(string value)
        {
            UnicodeEncoding Ue = new UnicodeEncoding();
            byte[] ByteSourceText = Ue.GetBytes(value);
            MD5CryptoServiceProvider Md5 = new MD5CryptoServiceProvider();
            // //obtener el valor del hash value desde el origen (byteSurceText)
            byte[] ByteHash = Md5.ComputeHash(ByteSourceText);
            Md5.Clear();
            // //retornarlo como un string
            // return Convert.ToBase64String(ByteHash);


            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < ByteHash.Length; i++)

            {

                sb.Append(ByteHash[i].ToString("X2"));

            }

            return sb.ToString();
        }

        // /// <summary>
        // /// Compares a Hash agains a non hashed value
        // /// </summary>
        // /// <param name="value">value to evaluate</param>
        // /// <param name="hash">hash to evaluate against</param>
        // /// <returns>True if the hash matches</returns>
        public bool CompareHashMD5(string value, string hash)
        {
            return hash.Equals(this.GetHashMD5(value));
        }

        // /// <summary>
        // /// Cypher a text with TripleDES algorithm
        // /// </summary>
        // /// <param name="toEncrypt">text to cypher</param>
        // /// <param name="key">key to cypher</param>
        // /// <param name="useHashing">true if the key must be hashed (more secure)</param>
        // /// <returns>cypher text</returns>
        public string Encriptar(string toEncrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            AppSettingsReader settingsReader = new AppSettingsReader();
            // //System.Windows.Forms.MessageBox.Show(key);
            // //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                // //Always release the resources and flush data
                // // of the Cryptographic service provide. Best Practice
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            // //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            // //mode of operation. there are other 4 modes.
            // //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            // //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            // //transform the specified region of bytes array to resultArray
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            // //Release resources held by TripleDes Encryptor
            tdes.Clear();
            // //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        // /// <summary>
        // /// Decypher a text using the TripleDES algorithm
        // /// </summary>
        // /// <param name="toEncrypt">text to decypher</param>
        // /// <param name="key">key to decypher</param>
        // /// <param name="useHashing">true if the key must be hashed (more secure)</param>
        // /// <returns>clear text</returns>
        public string Desencriptar(string cipherString, string key, bool useHashing)
        {
            string strResult = cipherString;

            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);
                if (useHashing)
                {
                    // //if hashing was used get the hash code with regards to your key
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    // //release any resource held by the MD5CryptoServiceProvider
                    hashmd5.Clear();
                }
                else
                {
                    // //if hashing was not implemented get the byte code of the key
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                // //set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;
                // //mode of operation. there are other 4 modes. 
                // //We choose ECB(Electronic code Book)
                tdes.Mode = CipherMode.ECB;
                // //padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                // //Release resources held by TripleDes Encryptor                
                tdes.Clear();
                // //return the Clear decrypted TEXT
                strResult = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (System.Exception)
            {


            }
            return strResult;
            //  Encriptor
        }
    }
}
