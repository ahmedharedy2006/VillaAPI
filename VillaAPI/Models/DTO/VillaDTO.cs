﻿using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Rate { get; set; }

        public string ImageUrl { get; set; }
    }
}
