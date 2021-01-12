using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FbLocalization.Core.ViewModel.Models;

namespace FbLocalization.Core.IO.Writer
{
    /// <summary>
    /// Localization binary chunk file writer
    /// </summary>
    public class LocalizationWriter : BinaryWriter
    {
        private Encoding m_Encoding;

        public LocalizationWriter(Stream output) : base(output) => this.m_Encoding = Encoding.Default;

        public LocalizationWriter(Stream output, Encoding encoding) : base(output, encoding) => this.m_Encoding = encoding;

        /// <summary>
        /// Write localization binary chunk file
        /// </summary>
        /// <param name="localization">localization items</param>
        public void Write(IEnumerable<LocalizationEntryVM> localization)
        {
            // Write header
            this.Write(0x39000);

            // Write file size
            this.Write(0);

            // Write list size
            this.Write(0);

            // VARIABLE TO SAVE POSITIONS
            long savePos = 0L;

            long dataOffsetCountPosition = this.BaseStream.Position;

            // Write temp data offset
            this.Write(0);

            long stringsOffsetCountPosition = this.BaseStream.Position;

            // Write temp strings offset
            this.Write(0);

            // Write something like `Global`
            this.WriteCString("Global");

            // Write zero bytes
            this.Write(new byte[113]);

            // Fix data offset
            savePos = this.BaseStream.Position;

            this.BaseStream.Position = dataOffsetCountPosition;
            this.Write((uint)savePos); // Write data offset
            this.BaseStream.Position = savePos;

            // Write zero bytes
            this.Write(new byte[8]);

            // Write hashes
            var hashStringOffsets = new List<uint>();
            foreach (var item in localization)
            {
                // Write hash
                this.Write(item.Hash);

                // Add offset
                hashStringOffsets.Add((uint)this.BaseStream.Position);

                // Write temp string offset
                this.Write(0);
            }

            // Save position
            long stringsOffsetStart = this.BaseStream.Position;

            var stringOffsets = new List<uint>();

            // Write strings
            foreach (var item in localization)
            {
                stringOffsets.Add((uint)this.BaseStream.Position);
                this.WriteCString(item.Value);
            }

            // Rewrite hash offsets
            for (int i = 0; i < stringOffsets.Count; i++)
            {
                // Got to the hash string offset
                this.BaseStream.Position = hashStringOffsets[i];

                // Get string offset from strings table
                uint offset = stringOffsets[i];

                // Write string offset for the hash
                this.Write(offset - (uint)stringsOffsetStart);
            }

            // Write strings offset
            this.BaseStream.Position = stringsOffsetCountPosition;
            this.Write((uint)stringsOffsetStart - 8);

            // Write size offsets
            {
                // Go to file size offset
                this.BaseStream.Position = dataOffsetCountPosition - 8;

                // Write File size - 8
                this.Write((uint)(this.BaseStream.Length - 8));

                // Write list size
                this.Write((uint)(hashStringOffsets.Count));
            }
        }

        /// <summary>
        /// Write CString
        /// </summary>
        /// <param name="text">string to write</param>
        public void WriteCString(string text)
        {
            this.WriteStringEncoding(text);
            this.Write((byte)0);
        }

        /// <summary>
        /// Writes a string with the defined encoding
        /// </summary>
        /// <param name="text">string to write</param>
        public void WriteStringEncoding(string text)
        {
            byte[] stringBytes = this.m_Encoding.GetBytes(text);
            this.Write(stringBytes, 0, stringBytes.Length);
        }
    }
}