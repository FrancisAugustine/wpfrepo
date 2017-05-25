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

            /*ServiceReference1.ServiceNowSoapClient soapClient = new ServiceReference1.ServiceNowSoapClient();
            soapClient.ClientCredentials.UserName.UserName = "admin";
            soapClient.ClientCredentials.UserName.Password = "Administrator@123";

            /*var b = soapClient.Endpoint.Binding as System.ServiceModel.BasicHttpBinding;
            //b.ProxyAddress = new Uri("arihant-proxy:80");

            b.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.TransportCredentialOnly;
            b.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Basic;
            b.Security.Transport.ProxyCredentialType = System.ServiceModel.HttpProxyCredentialType.Basic;
            System.Net.ServicePointManager.Expect100Continue = false;

            string proxy = string.Format("http://{0}", "arihant-proxy");
            //proxy = string.Format("{0}:{1}", proxy, "80");
            b.UseDefaultWebProxy = false;
            b.ProxyAddress = new Uri(proxy);

            /*soapClient.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy();
            soapClient.Proxy.Credentials = CredentialCache.DefaultCredentials;*/

            /*ServiceReference1.insert insert = new WpfApplication1.ServiceReference1.insert();
            ServiceReference1.insertResponse response = new WpfApplication1.ServiceReference1.insertResponse();
            insert.category = "dfdsdfsdfs";
            insert.comments = "Comments";
            insert.short_description = "my new incident";
    
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
            /*foreach (ServiceReference1.getRecordsResponseGetRecordsResult Result in Results)
            {
                //string consoleReturn = Result.number + " " + Result.short_description;
                //Console.WriteLine(consoleReturn)
                DataRow newRow = data.NewRow();
                newRow["Number"] = Result.number;
                newRow["Opened"] = Result.opened_at;
                newRow["Short Description"] = Result.short_description;
                newRow["Caller"] = Result.caller_id;
                newRow["Priority"] = Result.priority;
                newRow["State"] = Result.state;
                newRow["Assigned Group"] = Result.assignment_group;

                // Add the row to the rows collection.
                data.Rows.Add(newRow);
            }*/
            //gridview.DataContext = data.DefaultView;
            //gridview.Items.Refresh();

            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            // Set the Interval to 5 seconds.
            aTimer.Interval = 5000;
            aTimer.Enabled = true;

            //Console.WriteLine("Press \'q\' to quit the sample.");
            //while (Console.Read() != 'q') ;
        }

        // Specify what you want to happen when the Elapsed event is raised.
        public static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
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
            //DataGrid gridview = new DataGrid();
            MainWindow tabledata = new MainWindow();
            //tabledata.DataContext = data.DefaultView;
            tabledata.gridview.DataContext = data.DefaultView;
            //gridview.Items.Refresh();
        }
    }
}
