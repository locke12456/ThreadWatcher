﻿using libWatcherDialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadWathcerDialog
{
    public partial class Watcher : Form
    {
        public Watcher()
        {
            InitializeComponent();
          //  TheThreadWatcher logo = new TheThreadWatcher();
          //  logo.Show();
            BreakPoints bp = new BreakPoints();
            bp.Show();
        }
    }
}
