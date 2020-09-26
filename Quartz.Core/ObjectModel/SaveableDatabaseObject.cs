using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PersistentEntity;

namespace Quartz.Core.ObjectModel
{
    public class SaveableDatabaseObject : SaveableObject, IDatabaseObject
    {
        public int ID { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}