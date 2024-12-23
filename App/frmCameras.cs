using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Web.WebView2;

namespace OptiSafeAI.App
{
    
    public partial class frmCameras : UserControl
    {

        public frmCameras()
        {
            InitializeComponent();
        }

        private void frmCameras_Load(object sender, EventArgs e)
        {
            webView21.Source = new Uri("http://127.0.0.1:5000/video_feed");
        }
    }
}
