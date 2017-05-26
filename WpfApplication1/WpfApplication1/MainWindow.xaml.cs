﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Timer control
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            // Set the Interval to 5min.         
            aTimer.Interval = 50000;
            aTimer.Enabled = true;
        }

        public void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            ServiceReference1.ServiceNowSoapClient soapClient = new ServiceReference1.ServiceNowSoapClient();
            soapClient.ClientCredentials.UserName.UserName = "admin";
            soapClient.ClientCredentials.UserName.Password = "Administrator@123";

            ServiceReference1.getRecords Records = new ServiceReference1.getRecords();
            ServiceReference1.getRecordsResponseGetRecordsResult[] Results = soapClient.getRecords(Records);

            DataTable data = new DataTable();
            data.TableName = "ServiceNow Incidents";
            data.Columns.AddRange(new DataColumn[]{new DataColumn("Number"),
                                                     new DataColumn("Opened"),
                                                     new DataColumn("Short Description"),
                                                     new DataColumn("Caller"),
                                                     new DataColumn("Priority"),
                                                     new DataColumn("State"),
                                                     new DataColumn("Category"),
                                                     new DataColumn("Assigned Group"),
                                                     new DataColumn("Assigned To"),
                                                     new DataColumn("Updated On"),
                                                     new DataColumn("Updated By")});

            for (int i = 0; i < Results.Length; i++)
            {
                DataRow row = data.NewRow();
                row["Number"] = Results[i].number;
                row["Opened"] = Results[i].opened_at;
                row["Short Description"] = Results[i].short_description;
                row["Caller"] = Results[i].caller_id;
                row["Priority"] = Results[i].priority;
                row["State"] = Results[i].state;
                row["Category"] = Results[i].category;
                row["Assigned Group"] = Results[i].assignment_group;
                row["Assigned To"] = Results[i].assigned_to;
                row["Updated On"] = Results[i].sys_updated_on;
                row["Updated By"] = Results[i].sys_updated_by;
                data.Rows.Add(row);
            }

            Application.Current.Dispatcher.Invoke((Action)delegate 
            {
                gridview.DataContext = data.DefaultView;
                gridview.Items.Refresh();
            });
        }
    }
}
