using System.Data.Entity;
using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.ComponentModel.DataAnnotations;
//using System.Data;

namespace XdsRepository
{
    public class RepositoryDataBase : DbContext
    {
        public DbSet<Document> Documents { get; set; }
    }

    public class Document
    {
        [Key]
        public string DocUniqueId { get; set; }
        public string Location { get; set; }
        public string MimeType { get; set; }
        public DateTime DocDateTime { get; set; }
    }


    //public class ContextInitializer : DropCreateDatabaseAlways<XdsDataBase>
    public class ContextInitializer : CreateDatabaseIfNotExists<RepositoryDataBase>
    {

    }
}
