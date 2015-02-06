﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace LiteDB.Shell.Commands
{
    public class FileUpload : BaseGridFS, ILiteCommand
    {
        public bool IsCommand(StringScanner s)
        {
            return this.IsFileCommand(s, "upload");
        }

        public BsonValue Execute(LiteDatabase db, StringScanner s)
        {
            if (db == null) throw new LiteException("No database");

            var id = this.ReadId(s);

            var filename = Path.GetFullPath(s.Scan(@"\s*.*").Trim());

            if (!File.Exists(filename)) throw new IOException("File " + filename + " not found");

            var file = db.GridFS.Upload(id, filename);

            return file.AsDocument;
        }
    }
}