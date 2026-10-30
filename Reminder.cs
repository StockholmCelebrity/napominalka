using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace напоминалкаа
{
    public class Reminder
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public string Text { get; set; }
        public bool IsShown { get; set; } // уже показано
    }
    
}