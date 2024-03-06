﻿using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace MacroViewer
{
    public partial class ManageMacros : Form
    { 
        private string[] AllBody;
        private string WhereExe;
        private string strType;
        private string sBody;
        private string sLoc = "";
        private int iRow, iCol;
        private class CImageItems
        {
            public string ImageLocation { get; set; }
        }

        List<CImageItems> MyItems;

        public ManageMacros(string rstrType, ref string[] rBody)
        {
            InitializeComponent();
            AllBody = rBody;
            WhereExe = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
            strType = rstrType;
            MyItems = new List<CImageItems>();
            int i = 0;
            pbImage.SizeMode = PictureBoxSizeMode.StretchImage;


            dgManage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            sLoc = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
            return;
            while (true)
            {
                if (i > 29) return;
                sBody = rBody[i];
                if (sBody == ""|| sBody == null) break;
                if (sBody.Contains(Utils.AssignedImageName))
                {
                    AddImages(i);
                }
                i++;
            }
        }

        private void AddRow(string strPath)
        {
            CImageItems c3i = new CImageItems();
            c3i.ImageLocation = strPath;
            MyItems.Add(c3i);
        }

        private void AddImages(int r)
        {
            int j,i = sBody.IndexOf(Utils.AssignedImageName);
            int k = 0;
            while(i > 0)
            {
                j = sBody.IndexOf(".png", i);
                j += 4;
                string strUrl = sBody.Substring(i, j - i);
                AddRow(strUrl);
                k++;
                i += (j-i);
                i = sBody.IndexOf(Utils.AssignedImageName,i);
                if (i < 0) break;
            }
            if (k == 0) return;
            dgManage.DataSource = MyItems.ToArray();
            dgManage.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgManage.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgManage.Refresh();
        }

        private void RunTest()
        {
            string strTest = "12345LOCALIMAGEFILE1.png6789012345LOCALIMAGEFILE2.png6789012345LOCALIMAGEFILE3.png";
            sBody = strTest;
            AddImages(0);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            Process.Start(WhereExe);
        }



        private void btnDelAll_Click(object sender, EventArgs e)
        {
            Utils.PurgeLocalImages(strType,WhereExe);
        }


        private void SetLocation()
        {
            System.Drawing.Point ThisRC = dgManage.CurrentCellAddress;
            iRow = ThisRC.Y;
            iCol = ThisRC.X;
       }


        private void button1_Click(object sender, EventArgs e)
        {
            RunTest();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            //https://h30434.www3.hp.com/t5/media/gallerypage/user-id/2126190/tab/all
            string UserID = Utils.VolunteerUserID;
            string PhotoGallery;
            if (UserID == "") PhotoGallery = "https://h30434.www3.hp.com/t5/user/myprofilepage/tab/personal-profile:personal-info";
            else PhotoGallery = "https://h30434.www3.hp.com/t5/media/gallerypage/user-id/" + UserID + "/tab/albums";
            //string PhotoGallery = "https://h30434.www3.hp.com/t5/media/gallerypage/user-id/2126190/tab/albums";
            switch(Utils.BrowserWanted)
            {
                case Utils.eBrowserType.eFirefox: Process.Start("firefox.exe", "-new-window " + PhotoGallery);
                    break;
                case Utils.eBrowserType.eEdge: Process.Start("microsoft-edge:" + PhotoGallery);
                    break;
                case Utils.eBrowserType.eChrome: Process.Start("chrome.exe", PhotoGallery);
                    break;
            }
        }

        private void dgManage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SetLocation();
            string strTemp = sLoc + "/" + dgManage.Rows[iRow].Cells[0].Value.ToString();
            pbImage.ImageLocation = strTemp;
            for(int i = 0; i < 10; i++) Application.DoEvents();
        }
    }
}
