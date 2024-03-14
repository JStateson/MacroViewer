using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Media.Imaging;


namespace MacroViewer
{
    public partial class LinkObject : Form
    {
        private string sLoc;
        private bool bBoxing = false;
        private bool bBlinking = false;
        string strBoxed = "";
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
            sLoc = Utils.WhereExe;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            strResultOut = bBoxing ? strBoxed : tbImageUrl.Text;
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
                strImgUrl = Utils.AssembleIMG(tbSelectedItem.Text);
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
            if(strTemp == "")strTemp = strBoxed;
            if (strTemp == "") return;
            CShowBrowser MyBrowser = new CShowBrowser();
            MyBrowser.Init();
            MyBrowser.ShowInBrowser(sLoc, strTemp, true);
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


        private void TimerControl(bool bEnable)
        {
            bBoxing = bEnable;
            bBlinking = bEnable;
            BlinkTimer.Enabled = bEnable;
            if (bEnable) btnBoxIT.Text = "Remove from box";
            else
            {
                btnBoxIT.Text = "Put in box";
                strBoxed = "";
            }
        }

        private void StartTimer()
        {
            bBoxing = true;
            bBlinking = true;
            btnBoxIT.Text = "Remove from box";
            BlinkTimer.Enabled = true;
            Application.DoEvents();
        }

        private void StopTimer()
        {
            BlinkTimer.Enabled = false;
            Application.DoEvents();
            bBoxing = false;
            bBlinking = false;
            btnBoxIT.Text = "Put in box";
            strBoxed = "";

        }

        private void btnBoxIT_Click(object sender, EventArgs e)
        {
            string strUnBoxed = tbImageUrl.Text.Trim();
            if (strUnBoxed =="")
            {
                strUnBoxed= tbSelectedItem.Text.Trim();
            }
            if (strUnBoxed == "") return;
            strBoxed = Utils.Form1CellTable(strUnBoxed);
            if (bBoxing) StopTimer(); // TimerControl(false);
            else StartTimer(); // TimerControl(true);
        }

        private void LinkObject_FormClosing(object sender, FormClosingEventArgs e)
        {
            BlinkTimer.Enabled = false;
            BlinkTimer = null;
        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            lbBoxed.Visible = bBoxing;
            bBoxing = !bBoxing;
        }
    }
}
