//#define Debug

using System;
using System.IO;
using System.Windows.Forms;
using libWiiSharp;

namespace patchIOS
{
    class Program
    {
        private const string version = "1.1";

        static void Main(string[] args)
        {
            printInfo();
            if (!File.Exists("libWiiSharp.dll")) { Console.WriteLine("libWiiSharp.dll wasn't found..."); exit(); }
            if (args.Length < 2) { printInstructions(); exit(); }

            patchIos(args);
        }

        private static void patchIos(string[] args)
        {
            //Parse args, first arg to be file
            string inputFile = args[0];
            if (!File.Exists(inputFile)) { Console.WriteLine("{0} wasn't found...", inputFile); exit(); }

            bool[] patches = new bool[3];
            int newSlot = -1;
            int newVersion = -1;

            for (int i = 1; i < args.Length; i++)
            {
                switch (args[i].ToUpper())
                {
                    case "-FS":
                        patches[0] = true;
                        break;
                    case "-ES":
                        patches[1] = true;
                        break;
                    case "-NP":
                        patches[2] = true;
                        break;
                    case "-SLOT":
                        if (!int.TryParse(args[i + 1], out newSlot))
                        { Console.WriteLine("Invalid slot {0}...", args[i + 1]); exit(); }
                        if (newSlot < 3 || newSlot > 0xFF)
                        { Console.WriteLine("Invalid slot {0}...", newSlot); exit(); }
                        break;
                    case "-V":
                        if (!int.TryParse(args[i + 1], out newVersion))
                        { Console.WriteLine("Invalid version {0}...", args[i + 1]); exit(); }
                        if (newVersion < 0 || newVersion > 0xFFFF)
                        { Console.WriteLine("Invalid version {0}...", newVersion); exit(); }
                        break;
                }
            }

            if (!patches[0] && !patches[1] && !patches[2] && newSlot == -1 && newVersion == -1)
            { Console.WriteLine("No action specified..."); exit(); }

            //Load WAD
            Console.WriteLine("Loading WAD...");

            WAD w = new WAD();
            w.KeepOriginalFooter = true;

            w.Warning += new EventHandler<MessageEventArgs>(w_Warning);
            w.LoadFile(inputFile);

            //Check if WAD is an IOS
            if ((w.TitleID >> 32) != 1 || (w.TitleID & 0xffffffff) > 255 || (w.TitleID & 0xffffffff) < 3)
            { Console.WriteLine("Only IOS WADs can be patched..."); exit(); }

            //Patch
            int patchesApplied = 0;

            if (patches[0] || patches[1] || patches[2])
            {
                IosPatcher patcher = new IosPatcher();
                patcher.Debug += new EventHandler<MessageEventArgs>(patcher_Debug);

                patcher.LoadIOS(ref w);

                if (patches[0] && patches[1] && patches[2])
                    patchesApplied = patcher.PatchAll();
                else
                {
                    if (patches[0])
                        patchesApplied += patcher.PatchFakeSigning();
                    if (patches[1])
                        patchesApplied += patcher.PatchEsIdentify();
                    if (patches[2])
                        patchesApplied += patcher.PatchNandPermissions();
                }
            }

            //Change slot / version if requested
            if (newVersion > -1 || newSlot > -1)
            {
                if (newVersion > -1)
                { Console.WriteLine("Changing Title Version to: {0}", newVersion); w.TitleVersion = (ushort)newVersion; }

                if (newSlot > -1)
                { Console.WriteLine("Changing IOS Slot to: {0}", newSlot); w.TitleID = (ulong)((1UL << 32) | (uint)newSlot); }

                w.FakeSign = true;
            }

            //Save patched WAD
            Console.WriteLine("");
            if (patchesApplied == 0 && newSlot == -1 && newVersion == -1) { Console.WriteLine("No patches applied..."); exit(); }

            Console.WriteLine("{0} patches applied, saving WAD...", patchesApplied);
            w.Save(inputFile);
            Console.WriteLine("Finished...");
            exit();
        }

        static void w_Warning(object sender, MessageEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(e.Message);
        }

        static void patcher_Debug(object sender, MessageEventArgs e)
        {
            if (!e.Message.ToLower().Contains("finished"))
                Console.WriteLine(e.Message);
        }

        private static void printInfo()
        {
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-");
            Console.WriteLine(" patchIOS {0} by Leathl  ", version);
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-");
            Console.WriteLine("");
        }

        private static void printInstructions()
        {
            Console.WriteLine("Usage: {0}.exe path_to_ios.wad [-FS] [-ES] [-NP] [-slot 70] [-v 65535]", Path.GetFileNameWithoutExtension(Application.ExecutablePath));
            Console.WriteLine("     -FS => Patch Fakesigning (optional)");
            Console.WriteLine("     -ES => Patch ES_Identify (optional)");
            Console.WriteLine("     -NP => Patch NAND Permissions (optional)");
            Console.WriteLine("     -slot 70 => Changes the slot the IOS installs to (optional)");
            Console.WriteLine("     -v 65535 => Changes the version of the IOS (optional)");
        }

        private static void exit()
        {
            //Wait if debugging
#if Debug
            Console.ReadLine();
#endif

            //Exit application
            Environment.Exit(0);
        }
    }
}
