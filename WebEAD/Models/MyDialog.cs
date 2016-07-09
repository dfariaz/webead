using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEAD.Models
{
    public class MyDialog
    {
        public enum DialogType : short
        {
            Info = 0,
            Success = 1,
            Warning = 2,
            Error = 3
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public DialogType @Type { get; set; }
        public override string ToString()
        {
            return string.Format("{{ \"title\": \"{0}\", \"content\": \"{1}\", \"type\": \"{2}\"  }}", Title, Content, @Type.ToString().ToLower());
        }
    }
}