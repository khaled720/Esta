﻿using System.ComponentModel.DataAnnotations;

namespace ESTA.Models
{
    public class Level
    {


        [Key]
        public int Id { get; set; }

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "levelname")]
        public string TypeName { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Forum> forum { get; set; }

    }
}
