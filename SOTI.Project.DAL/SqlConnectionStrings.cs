﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Project.DAL
{
    public class SqlConnectionStrings
    {
        public static string GetConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["NorthwindCon"].ConnectionString;
            }
        }
    }
}
