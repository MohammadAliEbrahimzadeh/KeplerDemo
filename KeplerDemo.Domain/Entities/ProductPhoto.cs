using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeplerDemo.Domain.Entities;

[Table(name: "ProductPhoto", Schema = "Production")]
public class ProductPhoto
{
    [Key]
    public int ProductPhotoID { get; set; }
    public byte[] ThumbNailPhoto { get; set; } = null!;
    public string ThumbnailPhotoFileName { get; set; } = null!;
    public byte[] LargePhoto { get; set; } = null!;
    public string LargePhotoFileName { get; set; } = null!;

    public ICollection<ProductProductPhoto> ProductProductPhotos { get; set; } = new List<ProductProductPhoto>();
}



