using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * macros-0-0.jpg is the first image of the first macro
 * macros-0-1.jpg is the second images of the first macro
 * macros-1.0.jpg is the first image of the second marco
 * ====up to 30 macros
 * macros-0-0.id   is a two line text file where the first line is the word REMOTE or LOCAL
 *                 the second line is the url if remote or the name of the jpg if local
 *                 Any number of images can be associateed with each macro.  The local jpg must be 
 *                 uploaded to the HP server and once uploaded the url needs to be used
 *                 instead of the jpg name and LOCAL must change to REMOTE.
 *                 
 * The above macro names are prefix with PCmacros or PRNmacros (PC or PRN )
*/


namespace MacroViewer
{
    public partial class CreateMacro : Form
    {
        public string strResultOut { get; set; }
        private bool bLocal = false;
        private CMacImages MyMacImg;
        public CreateMacro(ref CMacImages MacImg)
        {
            InitializeComponent();
            if(MacImg != null)
            {
                gbSelectType.Text = "Macro number " + (MacImg.MacNum + 1).ToString();
            }
            else
            {
                gbSelectType.Text = "";
                tbImgCnt.Text = "0";
                tbNextNum.Text = "0";
                return;
            }
            if(MacImg.MacroImageL == null)
            {
                tbImgCnt.Text = "0";
                tbNextNum.Text = "0";
            }
            else
            {
                tbImgCnt.Text = MacImg.MacroImageL.Count.ToString();
                tbNextNum.Text = MacImg.NextAvailableImgIndex.ToString();
            }
            MyMacImg = MacImg;
            tbMacName.Text = MacImg.sMacName;
        }


        private void btnFormRemote_Click(object sender, EventArgs e)
        {
            if(tbUrlText.Text == "")
            {
                return;
            }
            pbImage.ImageLocation = tbUrlText.Text;
            bLocal = true;
        }


        private void AddImage()
        {
            if (bLocal)
            {
                MyMacImg.AddRemoteImage(tbUrlText.Text);
            }
            else
            {
                MyMacImg.AddLocalImage(ref pbImage);
            }
        }


        private void btnApply_Click(object sender, EventArgs e)
        {
            AddImage();
            strResultOut = MyMacImg.AssembleImage();
            this.Close();
        }

        private void btnCencel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {

                pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
                pbImage.Image = Clipboard.GetImage();
                bLocal = false;
                tbUrlText.Text = MyMacImg.GetNextFilename();

            }
            else
            {
                MessageBox.Show("Clipboard is empty. Please Copy Image.");
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pbImage.ImageLocation =null;
            pbImage.Image = null;
        }
    }
}
