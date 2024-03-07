using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

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
        public string strResultOut { get; set; }        public string strLocalOut { get; set; }
        //private bool bIsServer = false; // is on the HP image server
        private bool bIsPath = false;   // if a file on the disk then is true
        private int dHeight = 200;
        private int dWidth = 200;
        private string ExeFolder;
        string strType = "";
        public CreateMacro(string rstrType)
        {
            InitializeComponent();
            ExeFolder = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
            strLocalOut = "";
            strType = rstrType;
        }


        private void btnFormRemote_Click(object sender, EventArgs e)
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


        public string AssembleImage(string strUrl, int Height, int Width)
        {
            return "<img src=\"" + strUrl + "\" border=\"2\"  height=\"" + Height.ToString() + " width=\"" + Width.ToString() + "\">";
        }

        private void AddImage()
        {
            string strImageName = "", strImagePath="";
            int Width = 200, Height = 200;  //default
            if (bIsPath) // do not know H or W so default is used
            {
                strImageName =  Utils.GetNextImageFile(strType, ExeFolder);
                strImagePath = ExeFolder + "/" + strImageName;
                ImageConverter converter = new ImageConverter();
                byte[] MyByteArray = (byte[])converter.ConvertTo(pbImage.Image, typeof(byte[]));
                File.WriteAllBytes(strImagePath, MyByteArray);
                Width = pbImage.Image.Width;
                Height = pbImage.Image.Height;
            }
            else
            {
                strImageName = tbUrlText.Text;
            }
            strResultOut = AssembleImage(strImageName, Width, Height);
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
                pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
                pbImage.Image = Clipboard.GetImage();
                bIsPath = true;
                btnApply.Enabled = true; 
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
    }
}
