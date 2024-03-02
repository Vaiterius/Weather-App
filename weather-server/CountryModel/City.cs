using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CountryModel;

[Table("City")]
public partial class City
{
    [Key]
    public int CityId { get; set; }

    [Column(TypeName = "numeric(18, 4)")]
    public decimal Latitude { get; set; }

    [Column(TypeName = "numeric(18, 4)")]
    public decimal Longitude { get; set; }

    public int CountryId { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("Cities")]
    public virtual Country Country { get; set; } = null!;
}
