/* This file is part of libWiiSharp
 * Copyright (C) 2009 Leathl
 * 
 * libWiiSharp is free software: you can redistribute it and/or
 * modify it under the terms of the GNU General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * libWiiSharp is distributed in the hope that it will be
 * useful, but WITHOUT ANY WARRANTY; without even the implied warranty
 * of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

//TPL conversion based on Wii.py by Xuzz, SquidMan, megazig, Matt_P, Omega and The Lemon Man.
//Zetsubou by SquidMan was also a reference.
//Thanks to the authors!

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace libWiiSharp
{
    public enum TPL_Format
    {
        I4 = 0,
        I8 = 1,
        IA4 = 2,
        IA8 = 3,
        RGB565 = 4,
        RGB5A3 = 5,
        RGBA8 = 6,
        CI4 = 8,
        CI8 = 9,
        CI14X2 = 10,
        CMP = 14,
    }

    public enum TPL_PaletteFormat
    {
        IA8 = 0,
        RGB565 = 1,
        RGB5A3 = 2,
    }

    public class TPL
    {
        private TPL_Header tplHeader;
        private List<TPL_TextureEntry> tplTextureEntries;
        private List<TPL_TextureHeader> tplTextureHeaders;
        private List<TPL_PaletteHeader> tplPaletteHeaders;
        private List<byte[]> textureData;
        private List<byte[]> paletteData;

        /// <summary>
        /// The Number of Textures in the TPL.
        /// </summary>
        public int NumOfTextures { get { return (int)tplHeader.NumOfTextures; } }

        #region Public Functions
        /// <summary>
        /// Loads a TPL file.
        /// </summary>
        /// <param name="pathToTpl"></param>
        /// <returns></returns>
        public static TPL Load(string pathToTpl)
        {
            TPL tmpTpl = new TPL();

            MemoryStream ms = new MemoryStream(File.ReadAllBytes(pathToTpl));

            try { tmpTpl.parseTpl(ms); }
            catch { ms.Dispose(); throw; }

            ms.Dispose();
            return tmpTpl;
        }

        /// <summary>
        /// Loads a TPL file.
        /// </summary>
        /// <param name="tplFile"></param>
        /// <returns></returns>
        public static TPL Load(byte[] tplFile)
        {
            TPL tmpTpl = new TPL();
            MemoryStream ms = new MemoryStream(tplFile);

            try { tmpTpl.parseTpl(ms); }
            catch { ms.Dispose(); throw; }

            ms.Dispose();
            return tmpTpl;
        }

        /// <summary>
        /// Creates a TPL from an image.
        /// </summary>
        /// <param name="pathToImage"></param>
        /// <param name="tplFormat"></param>
        /// <returns></returns>
        public static TPL FromImage(string pathToImage, TPL_Format tplFormat)
        {
            return FromImages(new string[] { pathToImage }, new TPL_Format[] { tplFormat });
        }

        /// <summary>
        /// Creates a TPL from an image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="tplFormat"></param>
        /// <returns></returns>
        public static TPL FromImage(Image img, TPL_Format tplFormat)
        {
            return FromImages(new Image[] { img }, new TPL_Format[] { tplFormat });
        }

        /// <summary>
        /// Creates a TPL from multiple images.
        /// </summary>
        /// <param name="imagePaths"></param>
        /// <param name="tplFormats"></param>
        /// <returns></returns>
        public static TPL FromImages(string[] imagePaths, TPL_Format[] tplFormats)
        {
            if (tplFormats.Length < imagePaths.Length)
                throw new Exception("You must specify a format for each image!");

            List<Image> images = new List<Image>();
            foreach (string imagePath in imagePaths)
                images.Add(Image.FromFile(imagePath));

            TPL tmpTpl = new TPL();
            tmpTpl.createFromImages(images.ToArray(), tplFormats);
            return tmpTpl;
        }

        /// <summary>
        /// Creates a TPL from multiple images.
        /// </summary>
        /// <param name="images"></param>
        /// <param name="tplFormats"></param>
        /// <returns></returns>
        public static TPL FromImages(Image[] images, TPL_Format[] tplFormats)
        {
            if (tplFormats.Length < images.Length)
                throw new Exception("You must specify a format for each image!");

            TPL tmpTpl = new TPL();
            tmpTpl.createFromImages(images, tplFormats);
            return tmpTpl;
        }



        /// <summary>
        /// Loads a TPL file.
        /// </summary>
        /// <param name="pathToTpl"></param>
        public void LoadFile(string pathToTpl)
        {
            MemoryStream ms = new MemoryStream(File.ReadAllBytes(pathToTpl));

            try { parseTpl(ms); }
            catch { ms.Dispose(); throw; }

            ms.Dispose();
        }

        /// <summary>
        /// Loads a TPL file.
        /// </summary>
        /// <param name="tplFile"></param>
        public void LoadFile(byte[] tplFile)
        {
            MemoryStream ms = new MemoryStream(tplFile);

            try { parseTpl(ms); }
            catch { ms.Dispose(); throw; }

            ms.Dispose();
        }

        /// <summary>
        /// Creates a TPL from an image.
        /// </summary>
        /// <param name="pathToImage"></param>
        /// <param name="tplFormat"></param>
        public void CreateFromImage(string pathToImage, TPL_Format tplFormat)
        {
            CreateFromImages(new string[] { pathToImage }, new TPL_Format[] { tplFormat });
        }

        /// <summary>
        /// Creates a TPL from an image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="tplFormat"></param>
        public void CreateFromImage(Image img, TPL_Format tplFormat)
        {
            CreateFromImages(new Image[] { img }, new TPL_Format[] { tplFormat });
        }

        /// <summary>
        /// Creates a TPL from multiple images.
        /// </summary>
        /// <param name="imagePaths"></param>
        /// <param name="tplFormats"></param>
        public void CreateFromImages(string[] imagePaths, TPL_Format[] tplFormats)
        {
            if (tplFormats.Length < imagePaths.Length)
                throw new Exception("You must specify a format for each image!");

            List<Image> images = new List<Image>();
            foreach (string imagePath in imagePaths)
                images.Add(Image.FromFile(imagePath));

            createFromImages(images.ToArray(), tplFormats);
        }

        /// <summary>
        /// Creates a TPL from multiple images.
        /// </summary>
        /// <param name="images"></param>
        /// <param name="tplFormats"></param>
        public void CreateFromImages(Image[] images, TPL_Format[] tplFormats)
        {
            if (tplFormats.Length < images.Length)
                throw new Exception("You must specify a format for each image!");

            createFromImages(images, tplFormats);
        }



        /// <summary>
        /// Saves the TPL.
        /// </summary>
        /// <param name="savePath"></param>
        public void Save(string savePath)
        {
            if (File.Exists(savePath)) File.Delete(savePath);
            FileStream fs = new FileStream(savePath, FileMode.Create);

            try { writeToStream(fs); }
            catch { fs.Dispose(); throw; }

            fs.Dispose();
        }

        /// <summary>
        /// Returns the TPL as a memory stream.
        /// </summary>
        /// <returns></returns>
        public MemoryStream ToMemoryStream()
        {
            MemoryStream ms = new MemoryStream();

            try { writeToStream(ms); }
            catch { ms.Dispose(); throw; }

            return ms;
        }

        /// <summary>
        /// Returns the TPL as a byte array.
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray()
        {
            return ToMemoryStream().ToArray();
        }

        /// <summary>
        /// Extracts the first Texture of the TPL.
        /// </summary>
        /// <returns></returns>
        public Image ExtractTexture()
        {
            return ExtractTexture(0);
        }

        /// <summary>
        /// Extracts the Texture with the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Image ExtractTexture(int index)
        {
            byte[] rgbaData;

            switch ((TPL_Format)tplTextureHeaders[index].TextureFormat)
            {
                case TPL_Format.I4:
                    rgbaData = FromI4(textureData[index], tplTextureHeaders[index].TextureWidth, tplTextureHeaders[index].TextureHeight);
                    break;
                case TPL_Format.I8:
                    rgbaData = FromI8(textureData[index], tplTextureHeaders[index].TextureWidth, tplTextureHeaders[index].TextureHeight);
                    break;
                case TPL_Format.IA4:
                    rgbaData = FromIA4(textureData[index], tplTextureHeaders[index].TextureWidth, tplTextureHeaders[index].TextureHeight);
                    break;
                case TPL_Format.IA8:
                    rgbaData = FromIA8(textureData[index], tplTextureHeaders[index].TextureWidth, tplTextureHeaders[index].TextureHeight);
                    break;
                case TPL_Format.RGB565:
                    rgbaData = FromRGB565(textureData[index], tplTextureHeaders[index].TextureWidth, tplTextureHeaders[index].TextureHeight);
                    break;
                case TPL_Format.RGB5A3:
                    rgbaData = FromRGB5A3(textureData[index], tplTextureHeaders[index].TextureWidth, tplTextureHeaders[index].TextureHeight);
                    break;
                case TPL_Format.RGBA8:
                    rgbaData = FromRGBA8(textureData[index], tplTextureHeaders[index].TextureWidth, tplTextureHeaders[index].TextureHeight);
                    break;
                case TPL_Format.CI4:
                    rgbaData = FromCI4(textureData[index], convertPalette(index), tplTextureHeaders[index].TextureWidth, tplTextureHeaders[index].TextureHeight);
                    break;
                case TPL_Format.CI8:
                    rgbaData = FromCI8(textureData[index], convertPalette(index), tplTextureHeaders[index].TextureWidth, tplTextureHeaders[index].TextureHeight);
                    break;
                case TPL_Format.CI14X2:
                    rgbaData = FromCI14X2(textureData[index], convertPalette(index), tplTextureHeaders[index].TextureWidth, tplTextureHeaders[index].TextureHeight);
                    break;
                case TPL_Format.CMP:
                    rgbaData = FromCMP(textureData[index], tplTextureHeaders[index].TextureWidth, tplTextureHeaders[index].TextureHeight);
                    break;
                default:
                    throw new FormatException("Unsupported Texture Format!");
            }

            return convertTpl(rgbaData, tplTextureHeaders[index].TextureWidth, tplTextureHeaders[index].TextureHeight);
        }

        /// <summary>
        /// Extracts the first Texture of the TPL.
        /// </summary>
        /// <param name="savePath"></param>
        public void ExtractTexture(string savePath)
        {
            ExtractTexture(0, savePath);
        }

        /// <summary>
        /// Extracts the Texture with the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="savePath"></param>
        public void ExtractTexture(int index, string savePath)
        {
            if (File.Exists(savePath)) File.Delete(savePath);
            Image img = ExtractTexture(index);

            switch (Path.GetExtension(savePath).ToLower())
            {
                case ".tif":
                case ".tiff":
                    img.Save(savePath, System.Drawing.Imaging.ImageFormat.Tiff);
                    break;
                case ".bmp":
                    img.Save(savePath, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case ".gif":
                    img.Save(savePath, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                case ".jpg":
                case ".jpeg":
                    img.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                default:
                    img.Save(savePath, System.Drawing.Imaging.ImageFormat.Png);
                    break;
            }
        }

        /// <summary>
        /// Extracts all Textures of the TPL.
        /// </summary>
        /// <returns></returns>
        public Image[] ExtractAllTextures()
        {
            List<Image> imgList = new List<Image>();

            for (int i = 0; i < tplHeader.NumOfTextures; i++)
                imgList.Add(ExtractTexture(i));

            return imgList.ToArray();
        }

        /// <summary>
        /// Extracts all Textures of the TPL.
        /// </summary>
        /// <param name="saveDir"></param>
        public void ExtractAllTextures(string saveDir)
        {
            if (Directory.Exists(saveDir)) Directory.CreateDirectory(saveDir);

            for (int i = 0; i < tplHeader.NumOfTextures; i++)
                ExtractTexture(i, saveDir + Path.DirectorySeparatorChar + "Texture_" + i.ToString("x2") + ".png");
        }

        /// <summary>
        /// Adds a Texture to the TPL.
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="tplFormat"></param>
        public void AddTexture(string imagePath, TPL_Format tplFormat)
        {
            Image img = Image.FromFile(imagePath);
            AddTexture(img, tplFormat);
        }

        /// <summary>
        /// Adds a Texture to the TPL.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="tplFormat"></param>
        public void AddTexture(Image img, TPL_Format tplFormat)
        {
            TPL_TextureEntry tempTexture = new TPL_TextureEntry();
            TPL_TextureHeader tempTextureHeader = new TPL_TextureHeader();
            TPL_PaletteHeader tempPaletteHeader = new TPL_PaletteHeader();
            byte[] tempTextureData = convertImage(img, tplFormat);
            byte[] tempPaletteData = new byte[0];

            tempTextureHeader.TextureHeight = (ushort)img.Height;
            tempTextureHeader.TextureWidth = (ushort)img.Width;
            tempTextureHeader.TextureFormat = (uint)tplFormat;

            tplTextureEntries.Add(tempTexture);
            tplTextureHeaders.Add(tempTextureHeader);
            tplPaletteHeaders.Add(tempPaletteHeader);
            textureData.Add(tempTextureData);
            paletteData.Add(tempPaletteData);

            tplHeader.NumOfTextures++;
        }

        /// <summary>
        /// Removes a Texture from the TPL.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveTexture(int index)
        {
            if (tplHeader.NumOfTextures > index)
            {
                tplTextureEntries.RemoveAt(index);
                tplTextureHeaders.RemoveAt(index);
                tplPaletteHeaders.RemoveAt(index);
                textureData.RemoveAt(index);
                paletteData.RemoveAt(index);

                tplHeader.NumOfTextures--;
            }
        }

        /// <summary>
        /// Gets the format of the Texture with the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TPL_Format GetTextureFormat(int index)
        {
            return (TPL_Format)tplTextureHeaders[index].TextureFormat;
        }

        /// <summary>
        /// Gets the size of the Texture with the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Size GetTextureSize(int index)
        {
            return new Size(tplTextureHeaders[index].TextureWidth, tplTextureHeaders[index].TextureHeight);
        }
        #endregion

        #region Private Functions
        private void writeToStream(Stream writeStream)
        {
            fireDebug("Writing TPL...");

            writeStream.Seek(0, SeekOrigin.Begin);

            fireDebug("   Writing TPL Header... (Offset: 0x{0})", writeStream.Position);
            tplHeader.Write(writeStream);

            int texturePosition = (int)writeStream.Position;
            writeStream.Seek(tplHeader.NumOfTextures * 8, SeekOrigin.Current);

            //Write Palette Headers + Palette Datas
            for (int i = 0; i < tplHeader.NumOfTextures; i++)
            {
                if ((TPL_Format)tplTextureHeaders[i].TextureFormat == TPL_Format.CI4 ||
                    (TPL_Format)tplTextureHeaders[i].TextureFormat == TPL_Format.CI8 ||
                    (TPL_Format)tplTextureHeaders[i].TextureFormat == TPL_Format.CI14X2)
                {
                    fireDebug("   Writing Palette of Texture #{1}... (Offset: 0x{0})", writeStream.Position, i + 1);

                    tplTextureEntries[i].TexturePaletteOffset = (uint)writeStream.Position;
                    tplPaletteHeaders[i].PaletteDataOffset = (uint)(writeStream.Position + 12);
                    tplPaletteHeaders[i].Write(writeStream);
                    writeStream.Write(paletteData[i], 0, paletteData[i].Length);
                }
            }

            int textureHeaderPosition = (int)writeStream.Position;
            writeStream.Seek(tplHeader.NumOfTextures * 36, SeekOrigin.Current);
            writeStream.Seek(Shared.AddPadding((int)writeStream.Position), SeekOrigin.Begin);

            //Write textureData
            for (int i = 0; i < tplHeader.NumOfTextures; i++)
            {
                fireDebug("   Writing Texture #{1} of {2}... (Offset: 0x{0})", writeStream.Position, i + 1, tplHeader.NumOfTextures);

                tplTextureHeaders[i].TextureDataOffset = (uint)writeStream.Position;
                writeStream.Write(textureData[i], 0, textureData[i].Length);

                if (i < textureData.Count - 1)
                    writeStream.Seek(Shared.AddPadding((int)writeStream.Position), SeekOrigin.Begin);
            }

            while (writeStream.Position % 16 != 0)
                writeStream.WriteByte(0x00);

            writeStream.Seek(textureHeaderPosition, SeekOrigin.Begin);

            //Write Texture Headers
            for (int i = 0; i < tplHeader.NumOfTextures; i++)
            {
                fireDebug("   Writing Texture Header #{1} of {2}... (Offset: 0x{0})", writeStream.Position, i + 1, tplHeader.NumOfTextures);

                tplTextureEntries[i].TextureHeaderOffset = (uint)writeStream.Position;
                tplTextureHeaders[i].Write(writeStream);
            }

            writeStream.Seek(texturePosition, SeekOrigin.Begin);

            //Write Texture Entries
            for (int i = 0; i < tplHeader.NumOfTextures; i++)
            {
                fireDebug("   Writing Texture Entry #{1} of {2}... (Offset: 0x{0})", writeStream.Position, i + 1, tplHeader.NumOfTextures);

                tplTextureEntries[i].Write(writeStream);
            }

            fireDebug("Writing TPL Finished...");
        }

        private void parseTpl(MemoryStream tplFile)
        {
            fireDebug("Parsing TPL...");  

            tplHeader = new TPL_Header();
            tplTextureEntries = new List<TPL_TextureEntry>();
            tplTextureHeaders = new List<TPL_TextureHeader>();
            tplPaletteHeaders = new List<TPL_PaletteHeader>();
            textureData = new List<byte[]>();
            paletteData = new List<byte[]>();

            tplFile.Seek(0, SeekOrigin.Begin);
            byte[] temp = new byte[4];

            fireDebug("   Reading TPL Header: Magic... (Offset: 0x{0})", tplFile.Position);
            tplFile.Read(temp, 0, 4);
            if (Shared.Swap(BitConverter.ToUInt32(temp, 0)) != tplHeader.TplMagic)
            {
                fireDebug("    -> Invalid Magic: 0x{0}", Shared.Swap(BitConverter.ToUInt32(temp, 0)));
                throw new Exception("TPL Header: Invalid Magic!");
            }

            fireDebug("   Reading TPL Header: NumOfTextures... (Offset: 0x{0})", tplFile.Position);
            tplFile.Read(temp, 0, 4);
            tplHeader.NumOfTextures = Shared.Swap(BitConverter.ToUInt32(temp, 0));

            fireDebug("   Reading TPL Header: Headersize... (Offset: 0x{0})", tplFile.Position);
            tplFile.Read(temp, 0, 4);
            if (Shared.Swap(BitConverter.ToUInt32(temp, 0)) != tplHeader.HeaderSize)
            {
                fireDebug("    -> Invalid Headersize: 0x{0}", Shared.Swap(BitConverter.ToUInt32(temp, 0)));
                throw new Exception("TPL Header: Invalid Headersize!");
            }

            for (int i = 0; i < tplHeader.NumOfTextures; i++)
            {
                fireDebug("   Reading Texture Entry #{1} of {2}... (Offset: 0x{0})", tplFile.Position, i+1, tplHeader.NumOfTextures);

                TPL_TextureEntry tempTexture = new TPL_TextureEntry();

                tplFile.Read(temp, 0, 4);
                tempTexture.TextureHeaderOffset = Shared.Swap(BitConverter.ToUInt32(temp, 0));

                tplFile.Read(temp, 0, 4);
                tempTexture.TexturePaletteOffset = Shared.Swap(BitConverter.ToUInt32(temp, 0));

                tplTextureEntries.Add(tempTexture);
            }

            for (int i = 0; i < tplHeader.NumOfTextures; i++)
            {
                fireDebug("   Reading Texture Header #{1} of {2}... (Offset: 0x{0})", tplFile.Position, i + 1, tplHeader.NumOfTextures);

                TPL_TextureHeader tempTextureHeader = new TPL_TextureHeader();
                TPL_PaletteHeader tempPaletteHeader = new TPL_PaletteHeader();
                tplFile.Seek(tplTextureEntries[i].TextureHeaderOffset, SeekOrigin.Begin);

                tplFile.Read(temp, 0, 4);
                tempTextureHeader.TextureHeight = Shared.Swap(BitConverter.ToUInt16(temp, 0));
                tempTextureHeader.TextureWidth = Shared.Swap(BitConverter.ToUInt16(temp, 2));

                tplFile.Read(temp, 0, 4);
                tempTextureHeader.TextureFormat = Shared.Swap(BitConverter.ToUInt32(temp, 0));

                tplFile.Read(temp, 0, 4);
                tempTextureHeader.TextureDataOffset = Shared.Swap(BitConverter.ToUInt32(temp, 0));

                tplFile.Read(temp, 0, 4);
                tempTextureHeader.WrapS = Shared.Swap(BitConverter.ToUInt32(temp, 0));

                tplFile.Read(temp, 0, 4);
                tempTextureHeader.WrapT = Shared.Swap(BitConverter.ToUInt32(temp, 0));

                tplFile.Read(temp, 0, 4);
                tempTextureHeader.MinFilter = Shared.Swap(BitConverter.ToUInt32(temp, 0));

                tplFile.Read(temp, 0, 4);
                tempTextureHeader.MagFilter = Shared.Swap(BitConverter.ToUInt32(temp, 0));

                tplFile.Read(temp, 0, 4);
                tempTextureHeader.LodBias = Shared.Swap(BitConverter.ToUInt32(temp, 0));

                tplFile.Read(temp, 0, 4);
                tempTextureHeader.EdgeLod = temp[0];
                tempTextureHeader.MinLod = temp[1];
                tempTextureHeader.MaxLod = temp[2];
                tempTextureHeader.Unpacked = temp[3];

                if (tplTextureEntries[i].TexturePaletteOffset != 0)
                {
                    fireDebug("   Reading Palette Header #{1} of {2}... (Offset: 0x{0})", tplFile.Position, i + 1, tplHeader.NumOfTextures);

                    tplFile.Seek(tplTextureEntries[i].TexturePaletteOffset, SeekOrigin.Begin);

                    tplFile.Read(temp, 0, 4);
                    tempPaletteHeader.NumberOfItems = Shared.Swap(BitConverter.ToUInt16(temp, 0));
                    tempPaletteHeader.Unpacked = temp[2];
                    tempPaletteHeader.Pad = temp[3];

                    tplFile.Read(temp, 0, 4);
                    tempPaletteHeader.PaletteFormat = Shared.Swap(BitConverter.ToUInt32(temp, 0));

                    tplFile.Read(temp, 0, 4);
                    tempPaletteHeader.PaletteDataOffset = Shared.Swap(BitConverter.ToUInt32(temp, 0));
                }

                tplFile.Seek(tempTextureHeader.TextureDataOffset, SeekOrigin.Begin);
                byte[] tempTextureData = new byte[textureByteSize((TPL_Format)tempTextureHeader.TextureFormat, tempTextureHeader.TextureWidth, tempTextureHeader.TextureHeight)];
                byte[] tempPaletteData = new byte[tempPaletteHeader.NumberOfItems * 2];

                fireDebug("   Reading Texture #{1} of {2}... (Offset: 0x{0})", tplFile.Position, i + 1, tplHeader.NumOfTextures);
                tplFile.Read(tempTextureData, 0, tempTextureData.Length);

                if (tplTextureEntries[i].TexturePaletteOffset > 0)
                {
                    fireDebug("   Reading Palette #{1} of {2}... (Offset: 0x{0})", tplFile.Position, i + 1, tplHeader.NumOfTextures);

                    tplFile.Seek(tempPaletteHeader.PaletteDataOffset, SeekOrigin.Begin);
                    tplFile.Read(tempPaletteData, 0, tempPaletteData.Length);
                }
                else tempPaletteData = new byte[0];

                tplTextureHeaders.Add(tempTextureHeader);
                tplPaletteHeaders.Add(tempPaletteHeader);
                textureData.Add(tempTextureData);
                paletteData.Add(tempPaletteData);
            }
        }

        private int textureByteSize(TPL_Format tplFormat, int width, int height)
        {
            switch (tplFormat)
            {
                case TPL_Format.I4:
                    return Shared.AddPadding(width, 8) * Shared.AddPadding(height, 8) / 2;
                case TPL_Format.I8:
                case TPL_Format.IA4:
                    return Shared.AddPadding(width, 8) * Shared.AddPadding(height, 4);
                case TPL_Format.IA8:
                case TPL_Format.RGB565:
                case TPL_Format.RGB5A3:
                    return Shared.AddPadding(width, 4) * Shared.AddPadding(height, 4) * 2;
                case TPL_Format.RGBA8:
                    return Shared.AddPadding(width, 4) * Shared.AddPadding(height, 4) * 4;
                case TPL_Format.CI4:
                    return Shared.AddPadding(width, 8) * Shared.AddPadding(height, 8) / 2;
                case TPL_Format.CI8:
                    return Shared.AddPadding(width, 8) * Shared.AddPadding(height, 4);
                case TPL_Format.CI14X2:
                    return Shared.AddPadding(width, 4) * Shared.AddPadding(height, 4);
                case TPL_Format.CMP:
                    return width * height;
                default:
                    throw new FormatException("Unsupported Texture Format!");
            }
        }

        private void createFromImages(Image[] images, TPL_Format[] tplFormats)
        {
            tplHeader = new TPL_Header();
            tplTextureEntries = new List<TPL_TextureEntry>();
            tplTextureHeaders = new List<TPL_TextureHeader>();
            tplPaletteHeaders = new List<TPL_PaletteHeader>();
            textureData = new List<byte[]>();
            paletteData = new List<byte[]>();

            tplHeader.NumOfTextures = (uint)images.Length;

            for (int i = 0; i < images.Length; i++)
            {
                Image img = images[i];

                TPL_TextureEntry tempTexture = new TPL_TextureEntry();
                TPL_TextureHeader tempTextureHeader = new TPL_TextureHeader();
                TPL_PaletteHeader tempPaletteHeader = new TPL_PaletteHeader();
                byte[] tempTextureData = convertImage(img, tplFormats[i]);
                byte[] tempPaletteData = new byte[0];

                tempTextureHeader.TextureHeight = (ushort)img.Height;
                tempTextureHeader.TextureWidth = (ushort)img.Width;
                tempTextureHeader.TextureFormat = (uint)tplFormats[i];

                tplTextureEntries.Add(tempTexture);
                tplTextureHeaders.Add(tempTextureHeader);
                tplPaletteHeaders.Add(tempPaletteHeader);
                textureData.Add(tempTextureData);
                paletteData.Add(tempPaletteData);
            }
        }

        private byte[] convertImage(Image img, TPL_Format tplFormat)
        {
            switch (tplFormat)
            {
                case TPL_Format.I4:
                    return ToI4((Bitmap)img);
                case TPL_Format.I8:
                    return ToI8((Bitmap)img);
                case TPL_Format.IA4:
                    return ToIA4((Bitmap)img);
                case TPL_Format.IA8:
                    return ToIA8((Bitmap)img);
                case TPL_Format.RGB565:
                    return ToRGB565((Bitmap)img);
                case TPL_Format.RGB5A3:
                    return ToRGB5A3((Bitmap)img);
                case TPL_Format.RGBA8:
                    return ToRGBA8((Bitmap)img);
                case TPL_Format.CI4:
                case TPL_Format.CI8:
                case TPL_Format.CI14X2:
                case TPL_Format.CMP:
                default:
                    throw new FormatException("Format not supported!\nCurrently, images can only be converted to the following formats:\nI4, I8, IA4, IA8, RGB565, RGB5A3, RGBA8.");
            }
        }

        private uint[] imageToRgba(Image img)
        {
            int x = img.Width;
            int y = img.Height;
            uint[] rgba = new uint[x * y];

            for (int i = 0; i < y; i += 4)
            {
                for (int j = 0; j < x; j += 4)
                {
                    for (int y1 = i; y1 < i + 4; y1++)
                    {
                        for (int x1 = j; x1 < j + 4; x1++)
                        {
                            if (y1 >= y || x1 >= x)
                                continue;

                            Color color = ((Bitmap)img).GetPixel(x1, y1);
                            rgba[x1 + (y1 * x)] = (uint)color.ToArgb();
                        }
                    }
                }
            }

            return rgba;
        }

        private Bitmap convertTpl(byte[] data, int width, int height)
        {
            if (width == 0) width = 1;
            if (height == 0) height = 1;

            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(
                                     new Rectangle(0, 0, bmp.Width, bmp.Height),
                                     System.Drawing.Imaging.ImageLockMode.WriteOnly, bmp.PixelFormat);

                System.Runtime.InteropServices.Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
                bmp.UnlockBits(bmpData);
            }
            catch { bmp.Dispose(); throw; }

            return bmp;
        }

        private byte[] convertPalette(int index)
        {
            TPL_PaletteFormat paletteformat = (TPL_PaletteFormat)tplPaletteHeaders[index].PaletteFormat;
            int itemcount = tplPaletteHeaders[index].NumberOfItems;
            int r, g, b, a;

            uint[] output = new uint[itemcount];
            for (int i = 0; i < itemcount; i++)
            {
                if (i >= itemcount) continue;

                ushort pixel = BitConverter.ToUInt16(new byte[] { paletteData[index][i * 2 + 1], paletteData[index][i * 2] }, 0);

                if (paletteformat == TPL_PaletteFormat.IA8) //IA8
                {
                    r = (pixel >> 8);
                    b = r;
                    g = r;
                    a = ((pixel >> 0) & 0xff);
                }
                else if (paletteformat == TPL_PaletteFormat.RGB565) //RGB565
                {
                    b = (((pixel >> 11) & 0x1F) << 3) & 0xff;
                    g = (((pixel >> 5) & 0x3F) << 2) & 0xff;
                    r = (((pixel >> 0) & 0x1F) << 3) & 0xff;
                    a = 255;
                }
                else //RGB5A3
                {
                    if ((pixel & (1 << 15)) != 0) //RGB555
                    {
                        a = 255;
                        b = (((pixel >> 10) & 0x1F) * 255) / 31;
                        g = (((pixel >> 5) & 0x1F) * 255) / 31;
                        r = (((pixel >> 0) & 0x1F) * 255) / 31;
                    }
                    else //RGB4A3
                    {
                        a = (((pixel >> 12) & 0x07) * 255) / 7;
                        b = (((pixel >> 8) & 0x0F) * 255) / 15;
                        g = (((pixel >> 4) & 0x0F) * 255) / 15;
                        r = (((pixel >> 0) & 0x0F) * 255) / 15;
                    }
                }

                output[i] = (uint)((r << 0) | (g << 8) | (b << 16) | (a << 24));
            }

            return Shared.UIntArrayToByteArray(output);
        }

        private int avg(int w0, int w1, int c0, int c1)
        {
            int a0 = c0 >> 11;
            int a1 = c1 >> 11;
            int a = (w0 * a0 + w1 * a1) / (w0 + w1);
            int c = (a << 11) & 0xffff;

            a0 = (c0 >> 5) & 63;
            a1 = (c1 >> 5) & 63;
            a = (w0 * a0 + w1 * a1) / (w0 + w1);
            c = c | ((a << 5) & 0xffff);

            a0 = c0 & 31;
            a1 = c1 & 31;
            a = (w0 * a0 + w1 * a1) / (w0 + w1);
            c = c | a;

            return c;
        }

        #region TplToImage
        private byte[] FromRGBA8(byte[] tpl, int width, int height)
        {
            uint[] output = new uint[width * height];
            int inp = 0;
            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 4)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        for (int y1 = y; y1 < y + 4; y1++)
                        {
                            for (int x1 = x; x1 < x + 4; x1++)
                            {
                                byte[] pixelbytes = new byte[2];
                                pixelbytes[1] = tpl[inp * 2];
                                pixelbytes[0] = tpl[inp * 2 + 1];
                                ushort pixel = BitConverter.ToUInt16(pixelbytes, 0);
                                inp++;

                                if ((x1 >= width) || (y1 >= height))
                                    continue;

                                if (k == 0)
                                {
                                    int a = (pixel >> 8) & 0xff;
                                    int r = (pixel >> 0) & 0xff;
                                    output[x1 + (y1 * width)] |= (uint)((r << 16) | (a << 24));
                                }
                                else
                                {
                                    int g = (pixel >> 8) & 0xff;
                                    int b = (pixel >> 0) & 0xff;
                                    output[x1 + (y1 * width)] |= (uint)((g << 8) | (b << 0));
                                }
                            }
                        }
                    }
                }
            }

            return Shared.UIntArrayToByteArray(output);
        }

        private byte[] FromRGB5A3(byte[] tpl, int width, int height)
        {
            uint[] output = new uint[width * height];
            int inp = 0;
            int r, g, b;
            int a = 0;
            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 4)
                {
                    for (int y1 = y; y1 < y + 4; y1++)
                    {
                        for (int x1 = x; x1 < x + 4; x1++)
                        {
                            byte[] pixelbytes = new byte[2];
                            pixelbytes[1] = tpl[inp * 2];
                            pixelbytes[0] = tpl[inp * 2 + 1];
                            ushort pixel = BitConverter.ToUInt16(pixelbytes, 0);
                            inp++;

                            if (y1 >= height || x1 >= width)
                                continue;

                            if ((pixel & (1 << 15)) != 0)
                            {
                                b = (((pixel >> 10) & 0x1F) * 255) / 31;
                                g = (((pixel >> 5) & 0x1F) * 255) / 31;
                                r = (((pixel >> 0) & 0x1F) * 255) / 31;
                                a = 255;
                            }
                            else
                            {
                                a = (((pixel >> 12) & 0x07) * 255) / 7;
                                b = (((pixel >> 8) & 0x0F) * 255) / 15;
                                g = (((pixel >> 4) & 0x0F) * 255) / 15;
                                r = (((pixel >> 0) & 0x0F) * 255) / 15;
                            }

                            int rgba = (r << 0) | (g << 8) | (b << 16) | (a << 24);
                            output[(y1 * width) + x1] = (uint)rgba;
                        }
                    }
                }
            }

            return Shared.UIntArrayToByteArray(output);
        }

        private byte[] FromRGB565(byte[] tpl, int width, int height)
        {
            uint[] output = new uint[width * height];
            int inp = 0;
            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 4)
                {
                    for (int y1 = y; y1 < y + 4; y1++)
                    {
                        for (int x1 = x; x1 < x + 4; x1++)
                        {
                            byte[] pixelbytes = new byte[2];
                            pixelbytes[1] = tpl[inp * 2];
                            pixelbytes[0] = tpl[inp * 2 + 1];
                            ushort pixel = BitConverter.ToUInt16(pixelbytes, 0);
                            inp++;

                            if (y1 >= height || x1 >= width)
                                continue;

                            int b = (((pixel >> 11) & 0x1F) << 3) & 0xff;
                            int g = (((pixel >> 5) & 0x3F) << 2) & 0xff;
                            int r = (((pixel >> 0) & 0x1F) << 3) & 0xff;
                            int a = 255;

                            int rgba = (r << 0) | (g << 8) | (b << 16) | (a << 24);
                            output[y1 * width + x1] = (uint)rgba;
                        }
                    }
                }
            }

            return Shared.UIntArrayToByteArray(output);
        }

        private byte[] FromI4(byte[] tpl, int width, int height)
        {
            uint[] output = new uint[Shared.AddPadding(width, 8) * Shared.AddPadding(height, 8)];
            int inp = 0;

            for (int y = 0; y < height; y += 8)
            {
                for (int x = 0; x < width; x += 8)
                {
                    for (int y1 = y; y1 < y + 8; y1++)
                    {
                        for (int x1 = x; x1 < x + 8; x1 += 2)
                        {
                            int pixel = tpl[inp++];

                            if (y1 >= height || x1 >= width)
                                continue;

                            int i = (pixel >> 4) * 255 / 15;
                            int a = 255;

                            int rgba = (i << 0) | (i << 8) | (i << 16) | (a << 24);
                            output[y1 * width + x1] = (uint)rgba;

                            i = (pixel & 0x0F) * 255 / 15;
                            a = 255;

                            rgba = (i << 0) | (i << 8) | (i << 16) | (a << 24);
                            output[y1 * width + x1 + 1] = (uint)rgba;
                        }
                    }
                }
            }

            Array.Resize(ref output, width * height);
            return Shared.UIntArrayToByteArray(output);
        }

        private byte[] FromIA4(byte[] tpl, int width, int height)
        {
            uint[] output = new uint[width * height];
            int inp = 0;

            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 8)
                {
                    for (int y1 = y; y1 < y + 4; y1++)
                    {
                        for (int x1 = x; x1 < x + 8; x1++)
                        {
                            int pixel = tpl[inp++];

                            if (y1 >= height || x1 >= width)
                                continue;

                            int i = ((pixel & 0x0F) * 255 / 15) & 0xff;
                            int a = (((pixel >> 4) * 255) / 15) & 0xff;

                            int rgba = (i << 0) | (i << 8) | (i << 16) | (a << 24);
                            output[y1 * width + x1] = (uint)rgba;
                        }
                    }
                }
            }

            return Shared.UIntArrayToByteArray(output);
        }

        private byte[] FromI8(byte[] tpl, int width, int height)
        {
            uint[] output = new uint[width * height];
            int inp = 0;

            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 8)
                {
                    for (int y1 = y; y1 < y + 4; y1++)
                    {
                        for (int x1 = x; x1 < x + 8; x1++)
                        {
                            int pixel = tpl[inp++];

                            if (y1 >= height || x1 >= width)
                                continue;

                            int i = pixel;
                            int a = 255;

                            int rgba = (i << 0) | (i << 8) | (i << 16) | (a << 24);
                            output[y1 * width + x1] = (uint)rgba;
                        }
                    }
                }
            }

            return Shared.UIntArrayToByteArray(output);
        }

        private byte[] FromIA8(byte[] tpl, int width, int height)
        {
            uint[] output = new uint[width * height];
            int inp = 0;

            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 4)
                {
                    for (int y1 = y; y1 < y + 4; y1++)
                    {
                        for (int x1 = x; x1 < x + 4; x1++)
                        {
                            byte[] pixelbytes = new byte[2];
                            pixelbytes[1] = tpl[inp * 2];
                            pixelbytes[0] = tpl[inp * 2 + 1];
                            ushort pixel = BitConverter.ToUInt16(pixelbytes, 0);
                            inp++;

                            if (y1 >= height || x1 >= width)
                                continue;

                            int i = (pixel >> 8);
                            int a = pixel & 0xff;

                            int rgba = (i << 0) | (i << 8) | (i << 16) | (a << 24);
                            output[y1 * width + x1] = (uint)rgba;
                        }
                    }
                }
            }

            return Shared.UIntArrayToByteArray(output);
        }

        private byte[] FromCMP(byte[] tpl, int width, int height)
        {
            uint[] output = new uint[width * height];
            UInt16[] c = new UInt16[4];
            int[] pix = new int[4];
            int inp = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int ww = Shared.AddPadding(width, 8);

                    int x0 = x & 0x03;
                    int x1 = (x >> 2) & 0x01;
                    int x2 = x >> 3;

                    int y0 = y & 0x03;
                    int y1 = (y >> 2) & 0x01;
                    int y2 = y >> 3;

                    int off = (8 * x1) + (16 * y1) + (32 * x2) + (4 * ww * y2);

                    byte[] tmp1 = new byte[2];
                    tmp1[1] = tpl[off];
                    tmp1[0] = tpl[off + 1];
                    c[0] = BitConverter.ToUInt16(tmp1, 0);
                    tmp1[1] = tpl[off + 2];
                    tmp1[0] = tpl[off + 3];
                    c[1] = BitConverter.ToUInt16(tmp1, 0);

                    if (c[0] > c[1])
                    {
                        c[2] = (UInt16)avg(2, 1, c[0], c[1]);
                        c[3] = (UInt16)avg(1, 2, c[0], c[1]);
                    }
                    else
                    {
                        c[2] = (UInt16)avg(1, 1, c[0], c[1]);
                        c[3] = 0;
                    }

                    byte[] pixeldata = new byte[4];
                    pixeldata[3] = tpl[off + 4];
                    pixeldata[2] = tpl[off + 5];
                    pixeldata[1] = tpl[off + 6];
                    pixeldata[0] = tpl[off + 7];
                    uint pixel = BitConverter.ToUInt32(pixeldata, 0);

                    int ix = x0 + (4 * y0);
                    int raw = c[(pixel >> (30 - (2 * ix))) & 0x03];

                    pix[0] = (raw >> 8) & 0xf8;
                    pix[1] = (raw >> 3) & 0xf8;
                    pix[2] = (raw << 3) & 0xf8;
                    pix[3] = 0xff;
                    if (((pixel >> (30 - (2 * ix))) & 0x03) == 3 && c[0] <= c[1]) pix[3] = 0x00;

                    int intout = (pix[0] << 16) | (pix[1] << 8) | (pix[2] << 0) | (pix[3] << 24);
                    output[inp] = (uint)intout;
                    inp++;
                }
            }

            return Shared.UIntArrayToByteArray(output);
        }

        private byte[] FromCI4(byte[] tpl, byte[] palette, int width, int height)
        {
            uint[] paletteData = Shared.ByteArrayToUIntArray(palette);
            uint[] output = new uint[width * height];
            int i = 0;

            for (int y = 0; y < height; y += 8)
            {
                for (int x = 0; x < width; x += 8)
                {
                    for (int y1 = y; y1 < y + 8; y1++)
                    {
                        for (int x1 = x; x1 < x + 8; x1 += 2)
                        {
                            byte pixel = tpl[i++];

                            if (y1 >= height || x1 >= width)
                                continue;

                            output[y1 * width + x1] = paletteData[pixel >> 4]; ;
                            output[y1 * width + x1 + 1] = paletteData[pixel & 0x0F];
                        }
                    }
                }
            }

            return Shared.UIntArrayToByteArray(output);
        }

        private byte[] FromCI8(byte[] tpl, byte[] palette, int width, int height)
        {
            uint[] paletteData = Shared.ByteArrayToUIntArray(palette);
            uint[] output = new uint[width * height];
            int i = 0;

            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 8)
                {
                    for (int y1 = y; y1 < y + 4; y1++)
                    {
                        for (int x1 = x; x1 < x + 8; x1++)
                        {
                            ushort pixel = tpl[i++];

                            if (y1 >= height || x1 >= width)
                                continue;

                            output[y1 * width + x1] = paletteData[pixel];
                        }
                    }
                }
            }

            return Shared.UIntArrayToByteArray(output);
        }

        private byte[] FromCI14X2(byte[] tpl, byte[] palette, int width, int height)
        {
            uint[] paletteData = Shared.ByteArrayToUIntArray(palette);
            uint[] output = new uint[width * height];
            int i = 0;

            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 4)
                {
                    for (int y1 = y; y1 < y + 4; y1++)
                    {
                        for (int x1 = x; x1 < x + 4; x1++)
                        {
                            ushort pixel = tpl[i++];

                            if (y1 >= height || x1 >= width)
                                continue;

                            output[y1 * width + x1] = paletteData[pixel & 0x3FFF];
                        }
                    }
                }
            }

            return Shared.UIntArrayToByteArray(output);
        }
        #endregion

        #region ImageToTpl
        private byte[] ToRGBA8(Bitmap img)
        {
            uint[] pixeldata = imageToRgba(img);
            int w = img.Width;
            int h = img.Height;
            int z = 0, iv = 0;
            byte[] output = new byte[Shared.AddPadding(w, 4) * Shared.AddPadding(h, 4) * 4];
            uint[] lr = new uint[32], lg = new uint[32], lb = new uint[32], la = new uint[32];

            for (int y1 = 0; y1 < h; y1 += 4)
            {
                for (int x1 = 0; x1 < w; x1 += 4)
                {
                    for (int y = y1; y < (y1 + 4); y++)
                    {
                        for (int x = x1; x < (x1 + 4); x++)
                        {
                            uint rgba;

                            if (y >= h || x >= w)
                            {
                                rgba = 0;
                            }
                            else
                            {
                                rgba = pixeldata[x + (y * w)];
                            }

                            lr[z] = (uint)(rgba >> 16) & 0xff;
                            lg[z] = (uint)(rgba >> 8) & 0xff;
                            lb[z] = (uint)(rgba >> 0) & 0xff;
                            la[z] = (uint)(rgba >> 24) & 0xff;

                            z++;
                        }
                    }

                    if (z == 16)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            output[iv++] = (byte)(la[i]);
                            output[iv++] = (byte)(lr[i]);
                        }
                        for (int i = 0; i < 16; i++)
                        {
                            output[iv++] = (byte)(lg[i]);
                            output[iv++] = (byte)(lb[i]);
                        }

                        z = 0;
                    }
                }
            }


            return output;
        }

        private byte[] ToRGB565(Bitmap img)
        {
            uint[] pixeldata = imageToRgba(img);
            int w = img.Width;
            int h = img.Height;
            int z = -1;
            byte[] output = new byte[Shared.AddPadding(w, 4) * Shared.AddPadding(h, 4) * 2];

            for (int y1 = 0; y1 < h; y1 += 4)
            {
                for (int x1 = 0; x1 < w; x1 += 4)
                {
                    for (int y = y1; y < y1 + 4; y++)
                    {
                        for (int x = x1; x < x1 + 4; x++)
                        {
                            ushort newpixel;

                            if (y >= h || x >= w)
                            {
                                newpixel = 0;
                            }
                            else
                            {
                                uint rgba = pixeldata[x + (y * w)];

                                uint b = (rgba >> 16) & 0xff;
                                uint g = (rgba >> 8) & 0xff;
                                uint r = (rgba >> 0) & 0xff;

                                newpixel = (UInt16)(((b >> 3) << 11) | ((g >> 2) << 5) | ((r >> 3) << 0));
                            }

                            byte[] temp = BitConverter.GetBytes(newpixel);
                            Array.Reverse(temp);

                            output[++z] = temp[0];
                            output[++z] = temp[1];
                        }
                    }
                }
            }

            return output;
        }

        private byte[] ToRGB5A3(Bitmap img)
        {
            uint[] pixeldata = imageToRgba(img);
            int w = img.Width;
            int h = img.Height;
            int z = -1;
            byte[] output = new byte[Shared.AddPadding(w, 4) * Shared.AddPadding(h, 4) * 2];

            for (int y1 = 0; y1 < h; y1 += 4)
            {
                for (int x1 = 0; x1 < w; x1 += 4)
                {
                    for (int y = y1; y < y1 + 4; y++)
                    {
                        for (int x = x1; x < x1 + 4; x++)
                        {
                            int newpixel;

                            if (y >= h || x >= w)
                            {
                                newpixel = 0;
                            }
                            else
                            {
                                int rgba = (int)pixeldata[x + (y * w)];
                                newpixel = 0;

                                int r = (rgba >> 16) & 0xff;
                                int g = (rgba >> 8) & 0xff;
                                int b = (rgba >> 0) & 0xff;
                                int a = (rgba >> 24) & 0xff;

                                if (a <= 0xda)
                                {
                                    //RGB4A3

                                    newpixel &= ~(1 << 15);

                                    r = ((r * 15) / 255) & 0xf;
                                    g = ((g * 15) / 255) & 0xf;
                                    b = ((b * 15) / 255) & 0xf;
                                    a = ((a * 7) / 255) & 0x7;

                                    newpixel |= a << 12;
                                    newpixel |= b << 0;
                                    newpixel |= g << 4;
                                    newpixel |= r << 8;
                                }
                                else
                                {
                                    //RGB5

                                    newpixel |= (1 << 15);

                                    r = ((r * 31) / 255) & 0x1f;
                                    g = ((g * 31) / 255) & 0x1f;
                                    b = ((b * 31) / 255) & 0x1f;

                                    newpixel |= b << 0;
                                    newpixel |= g << 5;
                                    newpixel |= r << 10;
                                }
                            }

                            byte[] temp = BitConverter.GetBytes((UInt16)newpixel);
                            Array.Reverse(temp);

                            output[++z] = temp[0];
                            output[++z] = temp[1];
                        }
                    }
                }
            }

            return output;
        }

        private byte[] ToI4(Bitmap img)
        {
            uint[] pixeldata = imageToRgba(img);
            int w = img.Width;
            int h = img.Height;
            int inp = 0;
            byte[] output = new byte[Shared.AddPadding(w, 8) * Shared.AddPadding(h, 8) / 2];

            for (int y1 = 0; y1 < h; y1 += 8)
            {
                for (int x1 = 0; x1 < w; x1 += 8)
                {
                    for (int y = y1; y < y1 + 8; y++)
                    {
                        for (int x = x1; x < x1 + 8; x += 2)
                        {
                            byte newpixel;

                            if (x >= w || y >= h)
                            {
                                newpixel = 0;
                            }
                            else
                            {
                                uint rgba = pixeldata[x + (y * w)];

                                uint r = (rgba >> 0) & 0xff;
                                uint g = (rgba >> 8) & 0xff;
                                uint b = (rgba >> 16) & 0xff;

                                uint intensity1 = ((r + g + b) / 3) & 0xff;

                                if ((x + (y * w) + 1) == pixeldata.Length) continue;
                                rgba = pixeldata[x + (y * w) + 1];

                                r = (rgba >> 0) & 0xff;
                                g = (rgba >> 8) & 0xff;
                                b = (rgba >> 16) & 0xff;

                                uint intensity2 = ((r + g + b) / 3) & 0xff;

                                newpixel = (byte)((((intensity1 * 15) / 255) << 4) | (((intensity2 * 15) / 255) & 0xf));
                            }

                            output[inp++] = newpixel;
                        }
                    }
                }
            }

            return output;
        }

        private byte[] ToI8(Bitmap img)
        {
            uint[] pixeldata = imageToRgba(img);
            int w = img.Width;
            int h = img.Height;
            int inp = 0;
            byte[] output = new byte[Shared.AddPadding(w, 8) * Shared.AddPadding(h, 4)];

            for (int y1 = 0; y1 < h; y1 += 4)
            {
                for (int x1 = 0; x1 < w; x1 += 8)
                {
                    for (int y = y1; y < y1 + 4; y++)
                    {
                        for (int x = x1; x < x1 + 8; x++)
                        {
                            byte newpixel;

                            if (x >= w || y >= h)
                            {
                                newpixel = 0;
                            }
                            else
                            {
                                uint rgba = pixeldata[x + (y * w)];

                                uint r = (rgba >> 0) & 0xff;
                                uint g = (rgba >> 8) & 0xff;
                                uint b = (rgba >> 16) & 0xff;

                                newpixel = (byte)(((r + g + b) / 3) & 0xff);
                            }

                            output[inp++] = newpixel;
                        }
                    }
                }
            }

            return output;
        }

        private byte[] ToIA4(Bitmap img)
        {
            uint[] pixeldata = imageToRgba(img);
            int w = img.Width;
            int h = img.Height;
            int inp = 0;
            byte[] output = new byte[Shared.AddPadding(w, 8) * Shared.AddPadding(h, 4)];

            for (int y1 = 0; y1 < h; y1 += 4)
            {
                for (int x1 = 0; x1 < w; x1 += 8)
                {
                    for (int y = y1; y < y1 + 4; y++)
                    {
                        for (int x = x1; x < x1 + 8; x++)
                        {
                            byte newpixel;

                            if (x >= w || y >= h)
                            {
                                newpixel = 0;
                            }
                            else
                            {
                                uint rgba = pixeldata[x + (y * w)];

                                uint red = (rgba >> 0) & 0xff;
                                uint green = (rgba >> 8) & 0xff;
                                uint blue = (rgba >> 16) & 0xff;

                                uint intensity = ((red + green + blue) / 3) & 0xff;
                                uint alpha = (rgba >> 24) & 0xff;

                                newpixel = (byte)((((intensity * 15) / 255) & 0xf) | (((alpha * 15) / 255) << 4));
                            }

                            output[inp++] = newpixel;
                        }
                    }
                }
            }

            return output;
        }

        private byte[] ToIA8(Bitmap img)
        {
            uint[] pixeldata = imageToRgba(img);
            int w = img.Width;
            int h = img.Height;
            int inp = 0;
            byte[] output = new byte[Shared.AddPadding(w, 4) * Shared.AddPadding(h, 4) * 2];

            for (int y1 = 0; y1 < h; y1 += 4)
            {
                for (int x1 = 0; x1 < w; x1 += 4)
                {
                    for (int y = y1; y < y1 + 4; y++)
                    {
                        for (int x = x1; x < x1 + 4; x++)
                        {
                            ushort newpixel;

                            if (x >= w || y >= h)
                            {
                                newpixel = 0;
                            }
                            else
                            {
                                uint rgba = pixeldata[x + (y * w)];

                                uint r = (rgba >> 0) & 0xff;
                                uint g = (rgba >> 8) & 0xff;
                                uint b = (rgba >> 16) & 0xff;

                                uint intensity = ((r + g + b) / 3) & 0xff;
                                uint Alpha = (rgba >> 24) & 0xff;

                                newpixel = (ushort)((intensity << 8) | Alpha);
                            }

                            byte[] temp = BitConverter.GetBytes(newpixel);
                            Array.Reverse(temp);

                            output[inp++] = temp[0];
                            output[inp++] = temp[1];
                        }
                    }
                }
            }

            return output;
        }
        #endregion
        #endregion

        #region Events
        /// <summary>
        /// Fires debugging messages. You may write them into a log file or log textbox.
        /// </summary>
        public event EventHandler<MessageEventArgs> Debug;

        private void fireDebug(string debugMessage, params object[] args)
        {
            EventHandler<MessageEventArgs> debug = Debug;
            if (debug != null)
                debug(new object(), new MessageEventArgs(string.Format(debugMessage, args)));
        }
        #endregion
    }

    public class TPL_Header
    {
        private uint tplMagic = 0x0020AF30;
        private uint numOfTextures = 1;
        private uint headerSize = 0x0C;

        public uint TplMagic { get { return tplMagic; } }
        public uint NumOfTextures { get { return numOfTextures; } set { numOfTextures = value; } }
        public uint HeaderSize { get { return headerSize; } }

        public void Write(Stream writeStream)
        {
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(tplMagic)), 0, 4);
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(numOfTextures)), 0, 4);
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(headerSize)), 0, 4);
        }
    }

    public class TPL_TextureEntry
    {
        private uint textureHeaderOffset = 0x00000000;
        private uint texturePaletteOffset = 0x00000000;

        public uint TextureHeaderOffset { get { return textureHeaderOffset; } set { textureHeaderOffset = value; } }
        public uint TexturePaletteOffset { get { return texturePaletteOffset; } set { texturePaletteOffset = value; } }

        public void Write(Stream writeStream)
        {
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(textureHeaderOffset)), 0, 4);
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(texturePaletteOffset)), 0, 4);
        }
    }

    public class TPL_TextureHeader
    {
        private ushort textureHeight;
        private ushort textureWidth;
        private uint textureFormat;
        private uint textureDataOffset;
        private uint wrapS = 0x00000000;
        private uint wrapT = 0x00000000;
        private uint minFilter = 0x00000001;
        private uint magFilter = 0x00000001;
        private uint lodBias = 0x00000000;
        private byte edgeLod = 0x00;
        private byte minLod = 0x00;
        private byte maxLod = 0x00;
        private byte unpacked = 0x00;

        public ushort TextureHeight { get { return textureHeight; } set { textureHeight = value; } }
        public ushort TextureWidth { get { return textureWidth; } set { textureWidth = value; } }
        public uint TextureFormat { get { return textureFormat; } set { textureFormat = value; } }
        public uint TextureDataOffset { get { return textureDataOffset; } set { textureDataOffset = value; } }
        public uint WrapS { get { return wrapS; } set { wrapS = value; } }
        public uint WrapT { get { return wrapT; } set { wrapT = value; } }
        public uint MinFilter { get { return minFilter; } set { minFilter = value; } }
        public uint MagFilter { get { return magFilter; } set { magFilter = value; } }
        public uint LodBias { get { return lodBias; } set { lodBias = value; } }
        public byte EdgeLod { get { return edgeLod; } set { edgeLod = value; } }
        public byte MinLod { get { return minLod; } set { minLod = value; } }
        public byte MaxLod { get { return maxLod; } set { maxLod = value; } }
        public byte Unpacked { get { return unpacked; } set { unpacked = value; } }

        public void Write(Stream writeStream)
        {
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(textureHeight)), 0, 2);
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(textureWidth)), 0, 2);
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(textureFormat)), 0, 4);
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(textureDataOffset)), 0, 4);
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(wrapS)), 0, 4);
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(wrapT)), 0, 4);
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(minFilter)), 0, 4);
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(magFilter)), 0, 4);
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(lodBias)), 0, 4);
            writeStream.WriteByte(edgeLod);
            writeStream.WriteByte(minLod);
            writeStream.WriteByte(maxLod);
            writeStream.WriteByte(unpacked);
        }
    }

    public class TPL_PaletteHeader
    {
        private ushort numberOfItems = 0x0000;
        private byte unpacked = 0x00;
        private byte pad = 0x00;
        private uint paletteFormat;
        private uint paletteDataOffset;

        public ushort NumberOfItems { get { return numberOfItems; } set { numberOfItems = value; } }
        public byte Unpacked { get { return unpacked; } set { unpacked = value; } }
        public byte Pad { get { return pad; } set { pad = value; } }
        public uint PaletteFormat { get { return paletteFormat; } set { paletteFormat = value; } }
        public uint PaletteDataOffset { get { return paletteDataOffset; } set { paletteDataOffset = value; } }

        public void Write(Stream writeStream)
        {
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(numberOfItems)), 0, 2);
            writeStream.WriteByte(unpacked);
            writeStream.WriteByte(pad);
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(paletteFormat)), 0, 4);
            writeStream.Write(BitConverter.GetBytes(Shared.Swap(paletteDataOffset)), 0, 4);
        }
    }
}
