using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupMessagingSample.Models
{
public class Member
{
    [Key]
    public int MemberId { get; private set; }

    public string PhoneNumber { get; set; }
}
}