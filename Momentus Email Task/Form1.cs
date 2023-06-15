using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Momentus_Email_Task
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        string CSVPath = null;
        ///<summary>
             ///this function is opening the Dialog to select the location on your local machine 
        ///</summary>
        private void btnPath_Click(object sender, EventArgs e)
        {

            dynamic objBrowserDialog = new FolderBrowserDialog();
            DialogResult objDialogResult = objBrowserDialog.ShowDialog();
            CSVPath = "";
            if (objDialogResult == DialogResult.OK)
            {
                CSVPath = objBrowserDialog.SelectedPath + "\\export_" + DateTime.Now.ToString("ddMMyyyy") + ".csv";
                PathLabel.Text = "Exported to : " + CSVPath;
            }
        }
        ///<summary>
            /// this function will export the csv to to the location that selected in the previous function
        ///</summary>
        private void btnExport_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(CSVPath))
            {
                if (createExport())
                {
                    MessageBox.Show("Export Done...");
                }
                else
                {
                    MessageBox.Show("Export Error, please check the log...");
                }
            }
            else
            {
                MessageBox.Show("Please select a Path...");
            }
        }
        ///<summary>
             /// terminating the app
        ///</summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        ///<summary>
        /// this function will request data from the dp and call another function to save these data in csv
        ///</summary>

        public bool createExport()
        {
            string strSQL = null;
            string strValues = null;
            //string strFields = null;
            string strConnectionString = null;
            StringBuilder objExport = new StringBuilder();
            bool bolReturn = false;
            try
            { ///query string
              strSQL = @"
                        SELECT 
                         ev870_acct_code AS 'Code', 
                         ev870_name AS 'Name', 
                         ev870_city AS 'City', 
                         mm540_country_master.mm540_country_name AS 'Country' 
                        FROM 
                         ev870_acct_master 
                         join mm540_country_master ON mm540_country_master.mm540_country_code = ev870_acct_master.ev870_country 
                        WHERE 
                         ev870_class = 'O' 
                         AND ev870_status = 'A' 
                         AND ev870_city <> '' 
                         AND ev870_country = 'GER'";

                strConnectionString = "Data Source=euger-svr3\\sql2008r2;Initial Catalog=Briefing195;User ID=interface;Password=interface;";
                using (SqlConnection objConnection = new SqlConnection(strConnectionString))
                {
                    SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
                    objConnection.Open();
                    SqlDataReader objReader = objCommand.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            strValues = "";
                            for (int i = 0; i <= objReader.FieldCount - 1; i++)
                            {
                                strValues = strValues + "" + objReader.GetString(i) + ",";
                            }
                            strValues = strValues.Substring(0, strValues.Length - 1);
                            objExport.AppendLine(strValues);
                        }
                    }
                    objReader.Close();
                    createFile(objExport.ToString());
                }
                bolReturn = true;
            }
            catch (Exception ex)
            {
                log(ex.Message);
            }
            return bolReturn;
        }

        ///<summary>
            /// this function will check if the file is exist will delete it and create a new one with the exported data.
        ///</summary>
        public void createFile(string astrExport)
        {

            string strExportPath = null;
            try
            {
                strExportPath = CSVPath;
                if (File.Exists(strExportPath))
                {
                    File.Delete(strExportPath);
                }
                using (StreamWriter objStreamReader = new StreamWriter(strExportPath, false))
                {
                    objStreamReader.Write(astrExport);
                }
            }
            catch (Exception ex)
            {
                log(ex.Message);
            }
        }
        ///<summary>
             /// here we are loging all the errors
        ///</summary>
        public void log(string astrMsg)
        {
           
            string strLogPath = null;
            strLogPath = Application.StartupPath + "\\" + Application.ProductName.Replace(" ", "_") + "_" + DateTime.Now.ToString("ddMMyyyy") + ".log";
            using (StreamWriter objStreamReader = new StreamWriter(strLogPath, true))
            {
                objStreamReader.WriteLine(DateTime.Now.ToString("dd.MM.yyyy - HH:mm") + "\n" + astrMsg + "\n");
            }
        }

    }
}

