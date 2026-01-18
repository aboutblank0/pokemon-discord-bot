using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pokemon_discord_bot.Data
{
    [Table("frames")]
    public class Frame
    {
        [Key]
        [Column("frame_id")]
        public int FrameId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("cost")]
        public int Cost { get; set; }

        [Column("tradeable")]
        public bool Tradeable { get; set; } = true;

        [Required]
        [Column("img")]
        public string ImgPath { get; set; } = null!;
    }
}
