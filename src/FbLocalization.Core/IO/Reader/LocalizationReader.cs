using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FbLocalization.Core.ViewModel.Models;

namespace FbLocalization.Core.IO.Reader
{
    /// <summary>
    /// Localization binary chunk file reader
    /// </summary>
    public class LocalizationReader : BinaryReader
    {
        public LocalizationReader(Stream input) : base(input, Encoding.Unicode)
        {
        }

        public LocalizationReader(Stream input, Encoding encoding) : base(input, encoding)
        {
        }

        /// <summary>
        /// Read entries
        /// </summary>
        /// <returns>all <see cref="LocalizationEntryVM"/> entries</returns>
        public IEnumerable<LocalizationEntryVM> ReadEntries()
        {
            if (this.BaseStream is null || !this.BaseStream.CanRead)
                throw new Exception("Invalid stream!");

            this.BaseStream.Position = 0;

            // Read header
            if (this.ReadUInt32() != 0x39000)
                throw new InvalidDataException();

            // File size - 8
            uint fileSize = this.ReadUInt32();

            // List size
            uint listSize = this.ReadUInt32();

            // Read offset where the data starts
            uint dataOffset = this.ReadUInt32() + 8; // Skip zero bytes

            // Read strings offset
            uint stringsOffset = this.ReadUInt32() + 8;

            // Read section
            this.ReadCString();

            // Go to the data offset
            this.BaseStream.Position = dataOffset;

            // All ID hashes
            var hashList = new List<uint>();

            // All string offsets
            var offsetList = new List<uint>();

            // Read table
            for (int i = 0; i < listSize; i++)
            {
                // Add hash
                hashList.Add(this.ReadUInt32());

                // Add string offset
                offsetList.Add(this.ReadUInt32());
            }

            // Read values
            for (int i = 0; i < hashList.Count; i++)
            {
                // Navigate to the string position
                this.BaseStream.Position = (stringsOffset + offsetList[i]);

                // Hash text value
                string value = this.ReadCString();

                // Hash
                uint hash = hashList[i];

                yield return new LocalizationEntryVM(hash, value);
            }
        }

        /// <summary>
        /// Read zero character terminated string
        /// </summary>
        /// <returns>a zero terminated string</returns>
        private string ReadCString()
        {
            var sb = new StringBuilder();

            while (true)
            {
                byte c = this.ReadByte();

                if (c == 0)
                    break;

                sb.Append((char)c);
            }

            return sb.ToString();
        }
    }
}