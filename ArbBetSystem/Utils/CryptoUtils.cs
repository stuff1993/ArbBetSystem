using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ArbBetSystem
{
    class CryptoUtils
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CryptoUtils));

        public static byte[] Protect(byte[] data, byte[] s_aditionalEntropy)
        {
            try
            {
                // Encrypt the data using DataProtectionScope.CurrentUser. The result can be decrypted
                //  only by the same current user.
                return ProtectedData.Protect(data, s_aditionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                logger.Error("Data was not encrypted. An error occurred.", e);
                return null;
            }
        }

        public static byte[] Unprotect(byte[] data, byte[] s_aditionalEntropy)
        {
            try
            {
                //Decrypt the data using DataProtectionScope.CurrentUser.
                return ProtectedData.Unprotect(data, s_aditionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                logger.Error("Data was not decrypted. An error occurred.", e);
                return null;
            }
        }
    }
}
