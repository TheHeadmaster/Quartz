using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PersistentEntity;

namespace Quartz.IDE.ObjectModel
{
    public abstract class DatabaseDocumentControl : DocumentControl, IDatabaseObject
    {
        public int ID { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}