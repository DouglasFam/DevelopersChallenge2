using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;


namespace Xayah.App.ViewModels
{
    public class DocumentViewModel
    {
        [Key]
        public int Id { get; set; }

        public string FileUpload { get; set; }

        public IFormFile UploadDocument { get; set; }

        
        

    }
}
