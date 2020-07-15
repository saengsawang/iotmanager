using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;


namespace IoT.Core.Regions.Entity
{
    //[Table(IoTConsts.TablePrefix + "Region")]
    public class Region : Entity<int>, IFullAudited
    {
        public Region()
        {
            CreationTime = DateTime.Now;
            LastModificationTime = DateTime.Now;
            DeletionTime = DateTime.Now;
        }

        [Required]
        [MaxLength(30)]
        public string Level { get; set; }

        [Required]
        [MaxLength(30)]
        public string RegionName { get; set; }

        [Required]
        [MaxLength(30)]
        public DateTime TimeStamp { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get ; set ; }
        public bool IsDeleted { get; set; }
    }
}
