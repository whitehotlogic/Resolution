using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Resolution
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;

        private uint newWidth = 1024,newHeight=768;


		private System.ComponentModel.Container components = null;

		public Form1()
		{
			
			InitializeComponent();

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Button
			// 
			this.button1.Location = new System.Drawing.Point(40, 88);
			this.button1.Name = "button2";
			this.button1.Size = new System.Drawing.Size(208, 48);
			this.button1.TabIndex = 1;
            this.button1.Text = "Click Here to Change Resolution of All Screens to " + newWidth.ToString() + "x" + newHeight.ToString();
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 206);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {this.button1});
			this.Name = "Resolution Test Form";
			this.Text = "Resolution Test Form";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
		
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
            // change each screen's resolution to FixHeight and FixWidth
            //foreach (Screen display in Screen.AllScreens)
            //{
            Screen display = Screen.AllScreens[0];
                MessageBox.Show("Resolution of " + display.DeviceName + " is going to change to " + newWidth.ToString() + "x" + newHeight.ToString());
                Resolution.CResolution ChangeRes = new Resolution.CResolution(newWidth, newHeight, display);

            //}
            

        }
	}
}
