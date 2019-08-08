using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebsiteBook.Models.MetaData
{
    [MetadataTypeAttribute(typeof(BookMetadata))]
    public class Book
    {
        internal sealed class BookMetadata
        {
            public int BookID { get; set; }
            public string BookName { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }
            public string CoverImage { get; set; }
            public Nullable<System.DateTime> UpdateDay { get; set; }
            public Nullable<int> SoLuongTon { get; set; }

            [Display(Name = "Publishing company")]
            public Nullable<int> MaNXB { get; set; }

            [Display(Name = "Topic Name")]
            public Nullable<int> MaChuDe { get; set; }
        }
    }
}