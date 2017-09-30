namespace Supero.Tasklist.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Task entity model
    /// </summary>
    public partial class Task
    {
        /// <summary>
        /// Code
        /// </summary>
        [Key]
        public Guid CD_TASK { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [Required]
        [StringLength(100)]
        public string DS_TITLE { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [Required]
        [StringLength(2000)]
        public string DS_TASK { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        [Required]
        public DateTime DT_CREATION { get; set; }

        /// <summary>
        /// Last change date
        /// </summary>
        public DateTime? DT_LAST_CHANGE { get; set; }

        /// <summary>
        /// Removed date
        /// Obs: Only used in case of task deletion
        /// </summary>
        public DateTime? DT_REMOVED { get; set; }

        /// <summary>
        /// Finished date
        /// </summary>
        public DateTime? DT_FINISHED { get; set; }

        /// <summary>
        /// Is task finished
        /// </summary>
        public bool? ST_FINISHED { get; set; }
    }
}
