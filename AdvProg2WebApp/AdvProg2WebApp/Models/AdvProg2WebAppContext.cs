using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AdvProg2WebApp.Models
{
    /// <summary>
    /// db context class
    /// </summary>
    public class AdvProg2WebAppContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public AdvProg2WebAppContext() : base("name=AdvProg2WebAppContext")
        {
        }

        public System.Data.Entity.DbSet<AdvProg2WebApp.Models.User> Users { get; set; }
    }
}
