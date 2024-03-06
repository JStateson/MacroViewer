using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;


namespace MacroViewer
{
    public partial class LinkObject : Form
    {
        private string sLoc;
        private string strLC;
        public string strResultOut { get; set; }
        private bool bIsImage = false;
        private string strImgUrl;
        /*
        // ext = "*.bmp;*.dib;*.rle"           descr = BMP
        // ext = "*.jpg;*.jpeg;*.jpe;*.jfif"   descr = JPEG
        // ext = "*.gif"                       descr = GIF
        // ext = "*.tif;*.tiff"                descr = TIFF
        // ext = "*.png"                       descr = PNG
        */

        private bool IsUrlImage(string aLCase)
        {
            string[] ImgExt = new string[11]
            {".bmp",".dib",".rle",".jpg",".jpeg",".jpe",".jfif",".gif",".tif",".tiff",".png" };
            if (aLCase.Contains("image/serverpage"))
                return true; // must be from HP server
            foreach (string aImg in ImgExt)
            {
                if(aLCase.Contains(aImg))return true;
            }
            return false;
        }

        private void RBsetContext()
        {
            if (rbimage.Checked)
            {
                pbImage.ImageLocation = tbSelectedItem.Text;
                tbUrlText.Enabled = false; // not used for images
            }
            else
            {
                pbImage.ImageLocation = "";
                tbUrlText.Enabled = true;
            }
        }
        public LinkObject(string rstrIn)
        {
            strResultOut = "";
            bIsImage = IsUrlImage(rstrIn.ToLower());
            InitializeComponent();
            rbimage.Checked = bIsImage;
            rbNotImage.Checked = !bIsImage;
            tbSelectedItem.Text = rstrIn;
            RBsetContext();
            sLoc = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            strResultOut = tbImageUrl.Text;
            this.Close();
        }

        private void btnCencel_Click(object sender, EventArgs e)
        {
            strResultOut = "";
            this.Close();
        }

        private void btnApplyText_Click(object sender, EventArgs e)
        {
            if(rbimage.Checked)
            {
                strImgUrl = "<img src=\"" + tbSelectedItem.Text.Trim() + "\" border=\"2\">";
                tbImageUrl.Text = strImgUrl;
            }
            else
            {
                string strTmp = tbUrlText.Text;
                string strUrl = tbSelectedItem.Text;
                if (strTmp == "") strTmp = strUrl;
                tbImageUrl.Text = "<a href=\"" + strUrl + "\" target=\"_blank\">" + strTmp + "</a>";
            }
        }

        private void RunBrowser(string sLoc)
        {
            string strTemp = tbImageUrl.Text;
            if (strTemp == "") return;
            CShowBrowser MyBrowser = new CShowBrowser();
            MyBrowser.Init();
            MyBrowser.ShowInBrowser(sLoc, strTemp);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            RunBrowser(sLoc);
        }

        private void rbNotImage_CheckedChanged(object sender, EventArgs e)
        {
            RBsetContext();
        }

        private void rbimage_CheckedChanged(object sender, EventArgs e)
        {
            RBsetContext();
        }

    }
}
