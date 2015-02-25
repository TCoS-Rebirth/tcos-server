using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PackageExtractor
{
    public partial class MainWindow : Form
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtFileAppend_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtMapFile.Visible = !txtMapFile.Visible;
            lblMapFilename.Visible = !lblMapFilename.Visible;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnDoExtract_Click(object sender, EventArgs e)
        {
            if (checkBoxAllMaps.Checked)
            {

                
                

                

                //Construct list of all map filenames
                string[] maps = Directory.GetFiles(txtSBPath.Text + @"\data\environment\maps\");
  

                foreach (string map in maps)
                {
                    /* Dodgy code - Valshaaran
                    Console.WriteLine("Reading map at " + map);
                    pack = new Package(txtSBPath.Text + @"\data\environment\maps\" + map);
                    string noExtension = map.Substring(0, map.Length-4);
                    Console.WriteLine("DEBUG noExtension path = " + noExtension);
                    string saveFileName = noExtension.Substring(txtSBPath.Text.Length+23, noExtension.Length - (txtSBPath.Text.Length+23));
                    saveFileName = saveFileName + txtFilenameSuffix.Text;
                    Console.WriteLine("DEBUG saveFileName = " + saveFileName);

                    string saveFilePath;

                    if (checkBoxLocalSave.Checked)
                    {
                        saveFilePath = saveFileName;
                    }
                    else
                    {
                        saveFilePath = txtSavePath.Text + saveFileName;
                    }


                    Console.WriteLine("Saving to " + saveFilePath);
                    pack.Load(saveFilePath);
                    */

                    
                    Package pack = new Package(map);
                    pack.ReadLocalizedStrings(txtSBPath.Text + @"\data\static\descriptions.s");

                    string savePath = map.Remove(map.Length-4) + txtFilenameSuffix.Text;
                    Console.WriteLine("Output file : " + savePath);
                    pack.Load(txtSearchString.Text, savePath);

                }

            }
            else
            {
                Package pack = new Package(txtSBPath.Text + @"\data\environment\maps\" + txtMapFile.Text + ".sbw");
                pack.ReadLocalizedStrings(txtSBPath.Text + @"\data\static\descriptions.s");
                pack.Load(txtSearchString.Text, txtSBPath.Text + @"\data\" + txtMapFile.Text + txtFilenameSuffix.Text);

            }
            
            

            
        }

        private void checkBoxLocalSave_CheckedChanged(object sender, EventArgs e)
        {
            label3.Visible = !label3.Visible;
            txtSavePath.Visible = !txtSavePath.Visible;

        }

        private void txtSavePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSBPath_TextChanged(object sender, EventArgs e)
        {

        }
        
        
    }
}
