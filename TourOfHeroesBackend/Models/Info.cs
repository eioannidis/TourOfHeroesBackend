﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourOfHeroesBackend.Models
{
    public class Info
    {
        public string OriginalFileName { get; set; }
        public string XmlFileName { get; set; }
        public string Title { get; set; }
        public UserDto UploaderInfo { get; set; }
        public string Description { get; set; }
    }
}
