using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VCRrefunds;
using log4net;



namespace VCRrefunds
{
	public partial class Form1 : Form
	{

		public Form1()
		{
			
			InitializeComponent();

			log4net.Config.XmlConfigurator.Configure();
			ILog log;

			//Use the right logger
			log = LogManager.GetLogger("VCRrefundsLog");

			log.Error("test");
			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			log4net.Config.XmlConfigurator.Configure();
			ILog log;
			log = LogManager.GetLogger("VCRrefundsLog");
			log.Fatal("HELLO");

		}
	}
}