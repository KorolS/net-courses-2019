﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traiding.Core.Models
{
    public class ShareRegistrationInfo
    {
        public string CompanyName { get; set; }
        public ShareTypeEntity Type { get; set; }
        public bool Status { get; set; }
    }
}
