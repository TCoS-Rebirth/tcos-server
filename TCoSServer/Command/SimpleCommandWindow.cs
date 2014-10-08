using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TCoSServer.Command
{
    public partial class SimpleCommandWindow : Form
    {
        public SimpleCommandWindow()
        {
            InitializeComponent();
        }

        private void SimpleCommandWindow_Load(object sender, EventArgs e)
        {

        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                var c = Parser.Parse(commandBox.Text);
                CommandHandler.run(c);
            } catch(ArgumentException)
            { 
            }
            
        }

    }
}
