using System.Drawing;
using System.Runtime.CompilerServices;

namespace OptiSafeAI.App
{
    partial class frmCameras
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.webView22 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.webView23 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.webView24 = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.webView22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.webView23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.webView24)).BeginInit();
            this.SuspendLayout();
            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Location = new System.Drawing.Point(8, 7);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(500, 300);
            this.webView21.TabIndex = 4;
            this.webView21.ZoomFactor = 1D;
            // 
            // webView22
            // 
            this.webView22.AllowExternalDrop = true;
            this.webView22.CreationProperties = null;
            this.webView22.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView22.Location = new System.Drawing.Point(514, 7);
            this.webView22.Name = "webView22";
            this.webView22.Size = new System.Drawing.Size(500, 300);
            this.webView22.TabIndex = 5;
            this.webView22.ZoomFactor = 1D;
            // 
            // webView23
            // 
            this.webView23.AllowExternalDrop = true;
            this.webView23.CreationProperties = null;
            this.webView23.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView23.Location = new System.Drawing.Point(8, 329);
            this.webView23.Name = "webView23";
            this.webView23.Size = new System.Drawing.Size(500, 300);
            this.webView23.TabIndex = 6;
            this.webView23.ZoomFactor = 1D;
            // 
            // webView24
            // 
            this.webView24.AllowExternalDrop = true;
            this.webView24.CreationProperties = null;
            this.webView24.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView24.Location = new System.Drawing.Point(514, 329);
            this.webView24.Name = "webView24";
            this.webView24.Size = new System.Drawing.Size(500, 300);
            this.webView24.TabIndex = 7;
            this.webView24.ZoomFactor = 1D;
            // 
            // frmCameras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.webView24);
            this.Controls.Add(this.webView23);
            this.Controls.Add(this.webView22);
            this.Controls.Add(this.webView21);
            this.Name = "frmCameras";
            this.Size = new System.Drawing.Size(1067, 632);
            this.Load += new System.EventHandler(this.frmCameras_Load);
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.webView22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.webView23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.webView24)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView22;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView23;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView24;
    }
}
