using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace NusFileGrabber
{
    class NusFileGrabber
    {
        const string version = "1.1";
        const string nusUrl = "http://nus.cdn.shop.wii.com/ccs/download";

        static void PrintInstructions()
        {
            Console.WriteLine("Usage: nusfilegrabber.exe XX");
            Console.WriteLine(" ");
            Console.WriteLine("     Where XX can be:");
            Console.WriteLine("                     0e_old => 0000000e.app from IOS60 v6174 (SNEEK)");
            Console.WriteLine(" ");
            Console.WriteLine("                     0e => 0000000e.app from IOS70 v6687 (SNEEK)");
            Console.WriteLine("                     01 => 00000001.app from IOS60 v6174 (SNEEK)");
            Console.WriteLine(" ");
            Console.WriteLine("                     40 => 00000040.app from System Menu 3.2J");
            Console.WriteLine("                     42 => 00000042.app from System Menu 3.2U");
            Console.WriteLine("                     45 => 00000045.app from System Menu 3.2E");
            Console.WriteLine(" ");
            Console.WriteLine("                     70 => 00000070.app from System Menu 4.0J");
            Console.WriteLine("                     72 => 00000072.app from System Menu 4.0U");
            Console.WriteLine("                     75 => 00000075.app from System Menu 4.0E");
            Console.WriteLine(" ");
            Console.WriteLine("                     78 => 00000078.app from System Menu 4.1J");
            Console.WriteLine("                     7b => 0000007b.app from System Menu 4.1U");
            Console.WriteLine("                     7e => 0000007e.app from System Menu 4.1E");
            Console.WriteLine(" ");
            Console.WriteLine("                     84 => 00000084.app from System Menu 4.2J");
            Console.WriteLine("                     87 => 00000087.app from System Menu 4.2U");
            Console.WriteLine("                     8a => 0000008a.app from System Menu 4.2E");
        }

        static void Main(string[] args)
        {
            Console.Write("NusFileGrabber {0} by Leathl...\n", version);
            if (args.Length < 1) { PrintInstructions(); return; }

            string tempDir = Path.GetTempPath() + "\\" + Guid.NewGuid().ToString() + "\\";
            string titleID = "0000000100000002";
            string fileName = "000000" + args[0];
            string titleVersion = string.Empty;

            //Parse args
            switch (args[0].ToLower())
            {
                case "0e_old":
                    titleID = "000000010000003C";
                    titleVersion = "6174";
                    fileName = fileName.Remove(fileName.Length - 4);
                    break;
                case "0e":
                    titleID = "0000000100000046";
                    titleVersion = "6687";
                    break;
                case "01":
                    titleID = "000000010000003C";
                    titleVersion = "6174";
                    break;
                case "40":
                    titleVersion = "288";
                    break;
                case "42":
                    titleVersion = "289";
                    break;
                case "45":
                    titleVersion = "290";
                    break;
                case "70":
                    titleVersion = "416";
                    break;
                case "72":
                    titleVersion = "417";
                    break;
                case "75":
                    titleVersion = "418";
                    break;
                case "78":
                    titleVersion = "448";
                    break;
                case "7b":
                    titleVersion = "449";
                    break;
                case "7e":
                    titleVersion = "450";
                    break;
                case "84":
                    titleVersion = "480";
                    break;
                case "87":
                    titleVersion = "481";
                    break;
                case "8a":
                    titleVersion = "482";
                    break;
                default:
                    PrintInstructions();
                    return;
            }

            //Grab cetk + appfile
            Directory.CreateDirectory(tempDir);
            WebClient wcDownload = new WebClient();
            
            Console.WriteLine(" ");
            Console.Write("Grabbing Ticket...");
            try { wcDownload.DownloadFile(string.Format("{0}/{1}/{2}", nusUrl, titleID, "cetk"), tempDir + "cetk"); }
            catch (Exception ex)
            {
                Console.WriteLine(" ");
                Console.Write("Downloading Failed...\n" + ex.Message);
                Directory.Delete(tempDir, true);
                return;
            }

            Console.WriteLine(" ");
            Console.Write("Grabbing Tmd...");
            try { wcDownload.DownloadFile(string.Format("{0}/{1}/{2}{3}", nusUrl, titleID, "tmd.", titleVersion), tempDir + "tmd"); }
            catch (Exception ex)
            {
                Console.WriteLine(" ");
                Console.Write("Downloading Failed...\n" + ex.Message);
                Directory.Delete(tempDir, true);
                return;
            }

            string[,] contInfo = GetContentInfo(File.ReadAllBytes(tempDir + "tmd"));
            int contentIndex = -1;
            for (int i = 0; i < contInfo.GetLength(0); i++)
                if (contInfo[i, 0] == fileName) { contentIndex = i; break; }

            Console.WriteLine(" ");
            Console.Write("Grabbing Content... ({0} bytes)", contInfo[contentIndex, 3]);
            try { wcDownload.DownloadFile(string.Format("{0}/{1}/{2}", nusUrl, titleID, fileName), tempDir + fileName); }
            catch (Exception ex)
            {
                Console.WriteLine(" ");
                Console.Write("Downloading Failed...\n" + ex.Message);
                Directory.Delete(tempDir, true);
                return;
            }

            //Gather information
            byte[] encTitleKey = GetPartOfByteArray(File.ReadAllBytes(tempDir + "cetk"), 447, 16);
            byte[] commonkey = new byte[] { 0xeb, 0xe4, 0x2a, 0x22, 0x5e, 0x85, 0x93, 0xe4, 0x48, 0xd9, 0xc5, 0x45, 0x73, 0x81, 0xaa, 0xf7 };
            byte[] decTitleKey = GetTitleKey(encTitleKey, HexStringToByteArray(titleID), commonkey);

            byte[] tmdHash = HexStringToByteArray(contInfo[contentIndex, 4]);

            //Decrypt appfile
            Console.WriteLine(" ");
            Console.WriteLine("Decrypting Content...");
            byte[] decContent = DecryptContent(
                File.ReadAllBytes(tempDir + fileName), File.ReadAllBytes(tempDir + "tmd"), contentIndex, decTitleKey);

            //Check SHA1
            Console.WriteLine(" ");
            Console.Write("Hash Check: ");

            if (HashCheck(decContent, tmdHash)) { Console.Write("OK!"); }
            else { Console.Write("Failed!"); Directory.Delete(tempDir, true); return; }

            File.WriteAllBytes(fileName + ".app", decContent);

            //Delete Temps
            Directory.Delete(tempDir, true);

            Console.WriteLine(" ");
            Console.WriteLine("Finished!");
        }

        public static byte[] GetPartOfByteArray(byte[] array, int offset, int length)
        {
            byte[] ret = new byte[length];
            for (int i = 0; i < length; i++)
                ret[i] = array[offset + i];
            return ret;
        }

        public static byte[] HexStringToByteArray(string hexstring)
        {
            byte[] ba = new byte[hexstring.Length / 2];

            for (int i = 0; i < hexstring.Length / 2; i++)
            {
                ba[i] = byte.Parse(hexstring.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
            }

            return ba;
        }

        public static byte[] GetTitleKey(byte[] encryptedkey, byte[] titleid, byte[] commonkey)
        {
            Array.Resize(ref titleid, 16);

            RijndaelManaged decrypt = new RijndaelManaged();
            decrypt.Mode = CipherMode.CBC;
            decrypt.Padding = PaddingMode.None;
            decrypt.KeySize = 128;
            decrypt.BlockSize = 128;
            decrypt.Key = commonkey;
            decrypt.IV = titleid;

            ICryptoTransform cryptor = decrypt.CreateDecryptor();

            MemoryStream memory = new MemoryStream(encryptedkey);
            CryptoStream crypto = new CryptoStream(memory, cryptor, CryptoStreamMode.Read);

            byte[] decryptedkey = new byte[16];
            crypto.Read(decryptedkey, 0, decryptedkey.Length);

            crypto.Close();
            memory.Close();

            return decryptedkey;
        }

        public static int GetContentNum(byte[] tmd)
        {
            int contents = int.Parse(tmd[0x1de].ToString("x2") + tmd[0x1df].ToString("x2"), System.Globalization.NumberStyles.HexNumber);

            return contents;
        }

        /// <summary>
        /// [x, 0] = Content ID, [x, 1] = Index, [x, 2] = Type, [x, 3] = Size, [x, 4] = Sha1
        /// </summary>
        /// <param name="tmd"></param>
        /// <returns></returns>
        public static string[,] GetContentInfo(byte[] tmd)
        {
            int tmdpos = 0;

            int contentcount = GetContentNum(tmd);
            string[,] contentinfo = new string[contentcount, 5];

            for (int i = 0; i < contentcount; i++)
            {
                contentinfo[i, 0] = tmd[tmdpos + 0x1e4 + (36 * i)].ToString("x2") +
                    tmd[tmdpos + 0x1e5 + (36 * i)].ToString("x2") +
                    tmd[tmdpos + 0x1e6 + (36 * i)].ToString("x2") +
                    tmd[tmdpos + 0x1e7 + (36 * i)].ToString("x2");
                contentinfo[i, 1] = "0000" +
                    tmd[tmdpos + 0x1e8 + (36 * i)].ToString("x2") +
                    tmd[tmdpos + 0x1e9 + (36 * i)].ToString("x2");
                contentinfo[i, 2] = tmd[tmdpos + 0x1ea + (36 * i)].ToString("x2") +
                    tmd[tmdpos + 0x1eb + (36 * i)].ToString("x2");
                contentinfo[i, 3] = int.Parse(
                    tmd[tmdpos + 0x1ec + (36 * i)].ToString("x2") +
                    tmd[tmdpos + 0x1ed + (36 * i)].ToString("x2") +
                    tmd[tmdpos + 0x1ee + (36 * i)].ToString("x2") +
                    tmd[tmdpos + 0x1ef + (36 * i)].ToString("x2") +
                    tmd[tmdpos + 0x1f0 + (36 * i)].ToString("x2") +
                    tmd[tmdpos + 0x1f1 + (36 * i)].ToString("x2") +
                    tmd[tmdpos + 0x1f2 + (36 * i)].ToString("x2") +
                    tmd[tmdpos + 0x1f3 + (36 * i)].ToString("x2"), System.Globalization.NumberStyles.HexNumber).ToString();

                for (int j = 0; j < 20; j++)
                {
                    contentinfo[i, 4] += tmd[tmdpos + 0x1f4 + (36 * i) + j].ToString("x2");
                }
            }

            return contentinfo;
        }

        public static byte[] DecryptContent(byte[] content, byte[] tmd, int contentcount, byte[] titlekey)
        {
            byte[] iv = new byte[16];
            string[,] continfo = GetContentInfo(tmd);
            int contentsize = content.Length;

            iv[0] = tmd[0x1e8 + (0x24 * contentcount)];
            iv[1] = tmd[0x1e9 + (0x24 * contentcount)];

            RijndaelManaged decrypt = new RijndaelManaged();
            decrypt.Mode = CipherMode.CBC;
            decrypt.Padding = PaddingMode.None;
            decrypt.KeySize = 128;
            decrypt.BlockSize = 128;
            decrypt.Key = titlekey;
            decrypt.IV = iv;

            ICryptoTransform cryptor = decrypt.CreateDecryptor();

            MemoryStream memory = new MemoryStream(content);
            CryptoStream crypto = new CryptoStream(memory, cryptor, CryptoStreamMode.Read);

            bool fullread = false;
            byte[] buffer = new byte[memory.Length];
            byte[] cont = new byte[1];

            using (MemoryStream ms = new MemoryStream())
            {
                while (fullread == false)
                {
                    int len = 0;
                    if ((len = crypto.Read(buffer, 0, buffer.Length)) <= 0)
                    {
                        fullread = true;
                        cont = ms.ToArray();
                    }
                    ms.Write(buffer, 0, len);
                }
            }

            memory.Close();
            crypto.Close();

            Array.Resize(ref cont, int.Parse(continfo[contentcount, 3]));

            return cont;
        }

        public static bool CompareByteArrays(byte[] first, byte[] second)
        {
            if (first.Length != second.Length) return false;
            else
            {
                for (int i = 0; i < first.Length; i++)
                    if (first[i] != second[i]) return false;

                return true;
            }
        }

        static bool HashCheck(byte[] newFile, byte[] tmdHash)
        {
            SHA1 sha = SHA1.Create();
            byte[] fileHash = sha.ComputeHash(newFile);

            return CompareByteArrays(fileHash, tmdHash);
        }
    }
}
