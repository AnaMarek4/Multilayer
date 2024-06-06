﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Common
{
    public class PageFilter
    {
        public int rpp;
        public int pageNumber;

        public PageFilter(int rpp, int pageNumber)
        {
            this.rpp = rpp;
            this.pageNumber = pageNumber;
        }
    }
}