using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbBetSystem
{
    class FileUtils
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(FileUtils));

        public static bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // Open file for reading
                FileStream fs = new FileStream(_FileName, FileMode.Create, FileAccess.Write);
                // Writes a block of bytes to this stream using data froma byte array.
                fs.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                fs.Close();
                logger.Debug("Bytes saved");
                return true;
            }
            catch (Exception e)
            {
                logger.Error("Exception caught in write", e);
            }

            // error occured, return false
            return false;
        }

        public static byte[] FileToByteArray(string _FileName)
        {
            try
            {
                return File.ReadAllBytes(_FileName);
            }
            catch (Exception e)
            {
                logger.Error("Exception caught in read", e);
            }

            // error occured, return false
            return null;
        }
    }
}
