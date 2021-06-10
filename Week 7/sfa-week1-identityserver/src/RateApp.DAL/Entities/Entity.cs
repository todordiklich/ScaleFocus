using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateApp.DAL.Entities
{
    /// <summary>
    /// Base class for all objects that need to be stored and identified uniquely.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The date at which the entity was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
