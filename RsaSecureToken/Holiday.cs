﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaSecureToken
{
    public class Holiday
    {
        public string SayHello()
        {
            var today = GetToday();
            return today.Month == 12 && today.Day == 25 ? "Xmas" : "Today is not Xmas";
        }

        protected virtual DateTime GetToday()
        {
            return DateTime.Now;
        }
	}
}