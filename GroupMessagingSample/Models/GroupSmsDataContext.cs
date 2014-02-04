using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GroupMessagingSample.Models
{
    public class GroupSmsDataContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
    }
}