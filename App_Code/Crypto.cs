using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Web.UI;


public class Crypto
{
    //public static Logger logCrypto = LogManager.GetLogger("Crypto");
    static private Byte[] m_Key = new Byte[8];
    static private Byte[] m_IV = new Byte[8];

    static public string KeyCrypto = "1^gS~(7V$p@d*";

    #region Encrypt Inner Data
    /// <summary>
    /// EncryptData
    /// Metod for Internal data Encryption
    /// </summary>
    /// <param name="strKey"></param>
    /// <param name="strData"></param>
    /// <returns>string</returns>
    static public string EncryptData(string strKey, string strData)
    {
        string strResult;		//Return Result

        //1. String Length cannot exceed 90Kb. Otherwise, buffer will overflow. See point 3 for reasons
        if (strData.Length > 92160)
        {
            strResult = "Error. Data String too large. Keep within 90Kb.";
            return strResult;
        }

        //2. Generate the Keys
        if (!InitKey(strKey))
        {
            strResult = "Error. Fail to generate key for encryption";
            return strResult;
        }

        //3. Prepare the String
        //	The first 5 character of the string is formatted to store the actual length of the data.
        //	This is the simplest way to remember to original length of the data, without resorting to complicated computations.
        //	If anyone figure a good way to 'remember' the original length to facilite the decryption without having to use additional function parameters, pls let me know.
        strData = String.Format("{0,5:00000}" + strData, strData.Length);


        //4. Encrypt the Data
        byte[] rbData = new byte[strData.Length];
        ASCIIEncoding aEnc = new ASCIIEncoding();
        aEnc.GetBytes(strData, 0, strData.Length, rbData, 0);

        DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();

        ICryptoTransform desEncrypt = descsp.CreateEncryptor(m_Key, m_IV);


        //5. Perpare the streams:
        //	mOut is the output stream. 
        //	mStream is the input stream.
        //	cs is the transformation stream.
        MemoryStream mStream = new MemoryStream(rbData);
        CryptoStream cs = new CryptoStream(mStream, desEncrypt, CryptoStreamMode.Read);
        MemoryStream mOut = new MemoryStream();

        //6. Start performing the encryption
        int bytesRead;
        byte[] output = new byte[1024];
        do
        {
            bytesRead = cs.Read(output, 0, 1024);
            if (bytesRead != 0)
                mOut.Write(output, 0, bytesRead);
        } while (bytesRead > 0);

        //7. Returns the encrypted result after it is base64 encoded
        //	In this case, the actual result is converted to base64 so that it can be transported over the HTTP protocol without deformation.
        if (mOut.Length.Equals(0))
            strResult = "";
        else
            strResult = Convert.ToBase64String(mOut.GetBuffer(), 0, (int)mOut.Length);

        return strResult;

    }

    #endregion

    #region Decrypt Inner Data
    /// <summary>
    /// DecryptData
    /// Metod for Internal data Decryption
    /// </summary>
    /// param name="strKey"></param>
    /// <param name="strData"></param>
    /// <returns>string</returns>
    static public string DecryptData(String strKey, String strData)
    {
        string strResult;

        //1. Generate the Key used for decrypting
        if (!InitKey(strKey))
        {
            strResult = "Error. Fail to generate key for decryption";
            return strResult;
        }

        if (string.IsNullOrEmpty(strData.Trim()))
        {
            return "";
        }

        //2. Initialize the service provider
        int nReturn = 0;
        DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();
        ICryptoTransform desDecrypt = descsp.CreateDecryptor(m_Key, m_IV);

        //3. Prepare the streams:
        //	mOut is the output stream. 
        //	cs is the transformation stream.
        MemoryStream mOut = new MemoryStream();
        CryptoStream cs = new CryptoStream(mOut, desDecrypt, CryptoStreamMode.Write);

        //4. Remember to revert the base64 encoding into a byte array to restore the original encrypted data stream
        byte[] bPlain = new byte[strData.Length];
        try
        {
            bPlain = Convert.FromBase64CharArray(strData.ToCharArray(), 0, strData.Length);
        }
        catch (Exception)
        {
            strResult = "Error. Input Data is not base64 encoded.";
            return strResult;
        }

        long lRead = 0;
        long lTotal = strData.Length;

        try
        {
            //5. Perform the actual decryption
            while (lTotal >= lRead)
            {
                cs.Write(bPlain, 0, (int)bPlain.Length);
                //descsp.BlockSize=64
                lRead = mOut.Length + Convert.ToUInt32(((bPlain.Length / descsp.BlockSize) * descsp.BlockSize));
            };

            ASCIIEncoding aEnc = new ASCIIEncoding();
            strResult = aEnc.GetString(mOut.GetBuffer(), 0, (int)mOut.Length);

            //6. Trim the string to return only the meaningful data
            //	Remember that in the encrypt function, the first 5 character holds the length of the actual data
            //	This is the simplest way to remember to original length of the data, without resorting to complicated computations.
            String strLen = strResult.Substring(0, 5);
            int nLen = Convert.ToInt32(strLen);
            strResult = strResult.Substring(5, nLen);
            nReturn = (int)mOut.Length;

            return strResult;
        }
        catch (Exception exceptionCrypto)
        {
            throw exceptionCrypto;
            return strResult;
        }

    }

    #endregion

    #region Init Inner Key
    /// <summary>
    /// InitKey is used for initialization of Key
    /// Internal use ONLY
    /// Not used by methods outside class 'Crypto'
    /// </summary>
    /// <param name="strKey"></param>
    /// <returns></returns>
    static private bool InitKey(String strKey)
    {
        try
        {
            // Convert Key to byte array
            byte[] bp = new byte[strKey.Length];
            ASCIIEncoding aEnc = new ASCIIEncoding();
            aEnc.GetBytes(strKey, 0, strKey.Length, bp, 0);

            //Hash the key using SHA1
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            byte[] bpHash = sha.ComputeHash(bp);

            int i;
            // use the low 64-bits for the key value
            for (i = 0; i < 8; i++)
                m_Key[i] = bpHash[i];

            for (i = 8; i < 16; i++)
                m_IV[i - 8] = bpHash[i];

            return true;
        }
        catch (Exception exceptionCrypto)
        {
            //logCrypto.ErrorException(exceptionCrypto.Message, exceptionCrypto);
            //Error Performing Operations
            throw exceptionCrypto;
            return false;
        }

    }
    #endregion

    #region Encrypt Data for Application

    /// <summary>
    /// Encrypt Application Data with optionally passing PlayerID
    /// If PlayerID not passed, then the PlayerID would be taken from Session  
    /// </summary>
    /// <param name="strData"></param>
    /// <param name="OptionalPlayerID"></param>
    /// <returns>Encrypted Data</returns>
    public string Encrypt(string strData, string OptionalPlayerID)
    {
        strData = strData.ToLower();
        strData = strData.Trim();
        if (OptionalPlayerID.Trim().Length == 0)
        {
            Page pageobj = new Page();
            OptionalPlayerID = pageobj.Session["PlayerID"].ToString();
        }
        string strResult = EncryptData(GetUserKey(OptionalPlayerID), strData);
        return strResult;
    }

    /// <summary>
    /// Encryption method used for encrypting cookie info.
    /// </summary>
    /// <param name="strData"></param>
    /// <param name="cookieEncryption"></param>
    /// <returns></returns>

    public string Encrypt(string strData, bool cookieEncryption)
    {
        strData = strData.ToLower();
        strData = strData.Trim();
        PublicKeyClass pKey = new PublicKeyClass();
        string strResult = EncryptData(pKey.PublicKey, strData);
        return strResult;
    }

    #endregion

    #region Decrypt Data for Application

    /// <summary>
    /// Decrypt Application Data with optionally passing PlayerID
    /// If PlayerID not passed, then the PlayerID would be taken from Session  
    /// </summary>
    /// <param name="strData"></param>
    /// <param name="OptionalPlayerID"></param>
    /// <returns>Decrypted Data</returns>

    public string Decrypt(string strData, string OptionalPlayerID)
    {
        if (OptionalPlayerID.Trim().Length == 0)
        {
            Page pageobj = new Page();
            OptionalPlayerID = pageobj.Session["PlayerID"].ToString();
        }
        string strResult = DecryptData(GetUserKey(OptionalPlayerID), strData);
        strResult = strResult.ToLower();
        strResult = strResult.Trim();
        return strResult;
    }

    /// <summary>
    /// Decryption method used for decrypting cookie info.
    /// </summary>
    /// <param name="strData"></param>
    /// <param name="cookieDecryption"></param>
    /// <returns></returns>

    public string Decrypt(string strData, bool cookieDecryption)
    {
        PublicKeyClass pKey = new PublicKeyClass();
        string strResult = DecryptData(pKey.PublicKey, strData);
        strResult = strResult.ToLower();
        strResult = strResult.Trim();
        return strResult;
    }
    #endregion

    #region Get personalized Key for Application

    /// <summary>
    /// GetUserKey is used internally to generate personalized keys for each Player
    /// </summary>
    /// <param name="uid"></param>
    /// <returns>string</returns>

    private string GetUserKey(string uid)
    {
        //1.Get UserID to personalize the key

        //2.Get fixed Public Key
        PublicKeyClass keyObj = new PublicKeyClass();
        string Key = keyObj.PublicKey;

        //3.Encrypt PlayerID using Public Key
        StringBuilder strBuild = new StringBuilder();
        strBuild.Length = 0;
        strBuild.Append(EncryptData(keyObj.PublicKey, uid));

        //4.Concatenate Public Key with Encrypted PlayerID
        strBuild.Append(Key);

        //5.Return Personalized key
        return strBuild.ToString();
    }

    #endregion
}
public class PublicKeyClass
{
    #region PUBLIC KEY Property

    private static string key = "H!y64W%fw_2ZS7e=p&8K";

    /// <summary>
    /// 'PublicKey' property for obtaining Personalized key
    /// </summary>
    public string PublicKey
    {
        get
        {
            return key;
        }
    }

    #endregion

    #region Generate Random Characters for PUBLIC KEY

    /// <summary>
    /// Class Used to generate random characters to be used as PublicKey
    /// </summary>
    public class RandomPassword
    {
        // Define default min and max password lengths.
        private static int DEFAULT_MIN_PASSWORD_LENGTH = 20;
        private static int DEFAULT_MAX_PASSWORD_LENGTH = 21;

        // Define supported password characters divided into groups.
        private static string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz";
        private static string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
        private static string PASSWORD_CHARS_NUMERIC = "23456789";
        private static string PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/";

        /// <summary>
        /// Generates a random password.
        /// </summary>
        /// <returns>
        /// Randomly generated password.
        /// </returns>
        /// <remarks>
        /// The length of the generated password will be determined at
        /// random. It will be no shorter than the minimum default and
        /// no longer than maximum default.
        /// </remarks>
        public static string Generate()
        {
            return Generate(DEFAULT_MIN_PASSWORD_LENGTH,
                DEFAULT_MAX_PASSWORD_LENGTH);
        }

        /// <summary>
        /// Generates a random password of the exact length.
        /// </summary>
        /// <param name="length">
        /// Exact password length.
        /// </param>
        /// <returns>
        /// Randomly generated password.
        /// </returns>
        public static string Generate(int length)
        {
            return Generate(length, length);
        }

        /// <summary>
        /// Generates a random password.
        /// </summary>
        /// <param name="minLength">
        /// Minimum password length.
        /// </param>
        /// <param name="maxLength">
        /// Maximum password length.
        /// </param>
        /// <returns>
        /// Randomly generated password.
        /// </returns>
        /// <remarks>
        /// The length of the generated password will be determined at
        /// random and it will fall with the range determined by the
        /// function parameters.
        /// </remarks>
        public static string Generate(int minLength,
            int maxLength)
        {
            // Make sure that input parameters are valid.
            if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
                return null;

            // Create a local array containing supported password characters
            // grouped by types.
            char[][] charGroups = new char[][] 
		{
			PASSWORD_CHARS_LCASE.ToCharArray(),
			PASSWORD_CHARS_UCASE.ToCharArray(),
			PASSWORD_CHARS_NUMERIC.ToCharArray(),
			PASSWORD_CHARS_SPECIAL.ToCharArray()
		};

            // Use this array to track the number of unused characters in each
            // character group.
            int[] charsLeftInGroup = new int[charGroups.Length];

            // Initially, all characters in each group are not used.
            for (int i = 0; i < charsLeftInGroup.Length; i++)
                charsLeftInGroup[i] = charGroups[i].Length;

            // Use this array to track (iterate through) unused character groups.
            int[] leftGroupsOrder = new int[charGroups.Length];

            // Initially, all character groups are not used.
            for (int i = 0; i < leftGroupsOrder.Length; i++)
                leftGroupsOrder[i] = i;

            // Because we cannot use the default randomizer, which is based on the
            // current time (it will produce the same "random" number within a
            // second), we will use a random number generator to seed the
            // randomizer.

            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            byte[] randomBytes = new byte[4];

            // Generate 4 random bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            int seed = (randomBytes[0] & 0x7f) << 24 |
                randomBytes[1] << 16 |
                randomBytes[2] << 8 |
                randomBytes[3];

            // Now, this is real randomization.
            Random random = new Random(seed);

            // This array will hold password characters.
            char[] password = null;

            // Allocate appropriate memory for the password.
            if (minLength < maxLength)
                password = new char[random.Next(minLength, maxLength + 1)];
            else
                password = new char[minLength];

            // Index of the next character to be added to password.
            int nextCharIdx;

            // Index of the next character group to be processed.
            int nextGroupIdx;

            // Index which will be used to track not processed character groups.
            int nextLeftGroupsOrderIdx;

            // Index of the last non-processed character in a group.
            int lastCharIdx;

            // Index of the last non-processed group. Initially, we will skip
            // special characters.
            int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            int j = 0;
            // Generate password characters one at a time.
            //for (int i=0; i<password.Length; i++)
            foreach (char val in password)
            {
                // If only one character group remained unprocessed, process it;
                // otherwise, pick a random character group from the unprocessed
                // group list.
                if (lastLeftGroupsOrderIdx == 0)
                    nextLeftGroupsOrderIdx = 0;
                else
                    nextLeftGroupsOrderIdx = random.Next(0,
                        lastLeftGroupsOrderIdx);

                // Get the actual index of the character group, from which we will
                // pick the next character.
                nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                // Get the index of the last unprocessed characters in this group.
                lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                // If only one unprocessed character is left, pick it; otherwise,
                // get a random character from the unused character list.
                if (lastCharIdx == 0)
                    nextCharIdx = 0;
                else
                    nextCharIdx = random.Next(0, lastCharIdx + 1);

                // Add this character to the password.
                password[j] = charGroups[nextGroupIdx][nextCharIdx];


                // If we processed the last character in this group, start over.
                if (lastCharIdx == 0)
                    charsLeftInGroup[nextGroupIdx] =
                        charGroups[nextGroupIdx].Length;
                // There are more unprocessed characters left.
                else
                {
                    // Swap processed character with the last unprocessed character
                    // so that we don't pick it until we process all characters in
                    // this group.
                    if (lastCharIdx != nextCharIdx)
                    {
                        char temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] =
                            charGroups[nextGroupIdx][nextCharIdx];
                        val.ToString();
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }
                    // Decrement the number of unprocessed characters in
                    // this group.
                    charsLeftInGroup[nextGroupIdx]--;
                }

                // If we processed the last group, start all over.
                if (lastLeftGroupsOrderIdx == 0)
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                // There are more unprocessed groups left.
                else
                {
                    // Swap processed group with the last unprocessed group
                    // so that we don't pick it until we process all groups.
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] =
                            leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }
                    // Decrement the number of unprocessed groups.
                    lastLeftGroupsOrderIdx--;
                }
                j++;
            }

            // Convert password characters into a string and return the result.
            return new string(password);
        }
    }

    #endregion
}

