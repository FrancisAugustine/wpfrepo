using System;
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
            aTimer.AutoReset = true;
            aTimer.Interval = 50000;
            aTimer.Enabled = true;
            OnTimedEvent(null, null);
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
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

            foreach (ServiceReference1.getRecordsResponseGetRecordsResult Result in Results)
            {
                DataRow newRow = data.NewRow();
                newRow["Number"] = Result.number;
                newRow["Opened"] = Result.opened_at;
                newRow["Short Description"] = Result.short_description;
                newRow["Caller"] = Result.caller_id;
                newRow["Priority"] = Result.priority;
                newRow["State"] = Result.state;
                newRow["Assigned Group"] = Result.assignment_group;
                newRow["Assigned To"] = Result.assigned_to;
                newRow["Updated On"] = Result.sys_updated_on;
                newRow["Updated By"] = Result.sys_updated_by;

                //var rows = DataGrid.ItemsControlFromItemContainer(Result) as DataGridRow;                
                var rows = gridview.ItemContainerGenerator.ContainerFromItem(Result) as DataGridRow;
                if (Result.state == "1")
                {
                    if (Result.priority == "1")
                    {
                        rows.Background = Brushes.Red;
                    }
                    else if (Result.priority == "2")
                    {
                        rows.Background = Brushes.Yellow;
                    }
                    else if (Result.priority == "3")
                    {
                        //rows.Background = Brushes.Green;
                    }
                    else if (Result.priority == "4")
                    {
                        rows.Background = Brushes.Green;
                    }
                }
                // Add the row to the rows collection.                
                data.Rows.Add(newRow);
            }

            Application.Current.Dispatcher.Invoke((Action)delegate 
            {
                gridview.DataContext = data.DefaultView;
                gridview.Items.Refresh();
            });
        }
    }
}
