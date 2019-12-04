using System.ComponentModel.DataAnnotations;
using EpamNetProject.DAL.Models;

namespace EpamNetProject.BLL.Models
{
    public class SeatDto
    {
        public int Id { get; set; }

        [Required]
        public int AreaId { get; set; }

        [Required]
        public int Row { get; set; }

        [Required]
        public int Number { get; set; }
    }
}