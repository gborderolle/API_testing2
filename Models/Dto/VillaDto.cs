﻿using System.ComponentModel.DataAnnotations;

namespace API_testing2.Models.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string ImageUrl { get; set; }
        public int Tenants { get; set; }
        public int SizeMeters { get; set; }
        public decimal Fee { get; set; }
        public DateTime Creation { get; set; }
        public DateTime Update { get; set; }

    }
}