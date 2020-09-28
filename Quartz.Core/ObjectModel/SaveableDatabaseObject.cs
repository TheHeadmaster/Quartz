using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using PersistentEntity;

namespace Quartz.Core.ObjectModel
{
    /// <summary>
    /// An <see cref="IDatabaseObject"/> that also tracks relevant saveable changes.
    /// </summary>
    public class SaveableDatabaseObject : SaveableObject, IDatabaseObject
    {
        /// <summary>
        /// The unique identifier for the database object.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Used for concurrency checks involving simultaneous connections to the database.
        /// </summary>
        [Timestamp]
        public byte[]? RowVersion { get; set; }

        /// <summary>
        /// Saves asynchronously to the database.
        /// </summary>
        /// <param name="connection">
        /// The database connection to use.
        /// </param>
        public override async Task SaveAsync(Connection? connection = null)
        {
            if (connection is { })
            {
                using (DatabaseTransaction<QuartzContext> transaction = new DatabaseTransaction<QuartzContext>(connection))
                {
                    await transaction.AddOrUpdateAsync(this);
                    await transaction.SaveChangesAsync();
                }
                this.IsSaved = true;
            }
        }
    }
}