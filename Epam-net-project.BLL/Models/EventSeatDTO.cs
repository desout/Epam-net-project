﻿using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.BLL.Models
{
    public class EventSeatDto
    {
        public int Id { get; set; }
        [Required] public int EventAreaId { get; set; }
        [Required] public int Row { get; set; }
        [Required] public int Number { get; set; }
        [Required] public int State { get; set; }
        public string UserId { get; set; }
    }
}