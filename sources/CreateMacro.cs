using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

/*
Images used should be PNG but JPG should work
if you paste an image into a macro it will be named LOCALIMAGEFILE-X-Y.PNG
where X is PC, PRN or HP
and Y is 0..29
It's path will be the location of the executable program.  A tool (dialogbox "manage") can help
with replacing the LOCALIMAGEFILE with one in the HP album after you upload the local file to your HP album
*/


namespace MacroViewer
{
    public partial class CreateMacro : Form
    {
        public string strResultOut { get; set; }  
        //private bool bIsServer = false; // is on the HP image server
        private bool bIsPath = false;   // if a file on the disk then is true
        private string ExeFolder;
        string strType = "";
        public CreateMacro(string rstrType)
        {
            InitializeComponent();
            ExeFolder = Utils.WhereExe;
            strType = rstrType;
        }


        private void TryUseImage()
        {
            if (!Utils.IsUrlImage(tbUrlText.Text))
            {
                return; // needs to be an image
            }
            btnApply.Enabled = true;
            //bIsServer = tbUrlText.Text.Contains("image/serverpage");
            bIsPath = !Utils.bIsHTTP(tbUrlText.Text);
            pbImage.ImageLocation = Utils.RemoveOuterQuotes(tbUrlText.Text);
        }

        private void btnFormRemote_Click(object sender, EventArgs e)
        {
            TryUseImage();
        }




        private void AddImage()
        {
            string strImageName = "", strImagePath="";
            int Width = 200, Height = 200;  //default unless image-size is present
            if (bIsPath) // do not know H or W so default is used
            {
                strImageName =  Utils.GetNextImageFile(strType, ExeFolder);
                strImagePath = ExeFolder + "/" + strImageName;
                pbImage.Image.Save(strImagePath, ImageFormat.Png);
                Width = pbImage.Image.Width;
                Height = pbImage.Image.Height;
            }
            else
            {
                strImageName = tbUrlText.Text;
            }
            strResultOut = Utils.AssembleImage(strImageName, Height, Width);
        }


        private void btnApply_Click(object sender, EventArgs e)
        {
            AddImage();
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
                pbImage.SizeMode = PictureBoxSizeMode.Zoom; // .StretchImage;
                pbImage.Image = Clipboard.GetImage();
                bIsPath = true;
                btnApply.Enabled = true; 
            }
            else
            {
                MessageBox.Show("Empty clipboard: Please copy image using windows key + 'S'\r\nOr any screen capture tool such as snagit");
            }            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pbImage.ImageLocation =null;
            pbImage.Image = null;
        }

        private void pbImage_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            btnApply.Enabled = true;
        }

      
        private void btnBrowseImg_Click(object sender, EventArgs e)
        {
            var codecs = ImageCodecInfo.GetImageEncoders();
            var codecFilter = "Image Files|";
            string strFilename = "";
            foreach (var codec in codecs)
            {
                codecFilter += codec.FilenameExtension + ";";
            }
            ofd.Filter = codecFilter;
            ofd.DefaultExt = ".png";
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                strFilename = ofd.FileName;
                tbUrlText.Text = strFilename;
            }
        }

        private void tbUrlText_TextChanged(object sender, EventArgs e)
        {
            TryUseImage();
        }
    }
}
