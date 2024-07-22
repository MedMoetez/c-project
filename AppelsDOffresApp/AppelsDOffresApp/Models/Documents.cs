using AppelsDOffresApp.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace AppelsDOffresApp.Models;
public class Documents
{
    [Key]
    public int Id { get; set; }
    public string Nom { get; set; }
    public DocumentType Type { get; set; }

    [NotMapped]
    [Required]
    [FileExtensions(Extensions = "pdf,jpg,jpeg,png", ErrorMessage = "Please upload a valid file format (pdf, jpg, jpeg, png).")]
    public IFormFile UploadedFile { get; set; }
    public byte[] Contenu { get; set; }

    [ForeignKey("AppelOffre")]
    public int AppelDOffreId { get; set; }

    // Navigation property
    public virtual AppelOffre AppelOffre { get; set; }
}
