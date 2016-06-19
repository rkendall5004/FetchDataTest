using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace FetchDataTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           GetSiteHtml("http://dynamic.pulselive.com/dynamic/cricinfo/64599/uds.json.jgz?t=1466187592697");
        }

        public string GetSiteHtml(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            string returnValue = null;
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (TextReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        returnValue = reader.ReadToEnd();

                        var dyn = JsonConvert.DeserializeObject<dynamic>(reader.ReadToEnd());
                        var jsonData = dyn.Data.tostring();

                        List<string> list = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);

                        foreach (string item in list)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }

                             

                return returnValue;
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    if (response != null)
                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                            Console.WriteLine(streamReader.ReadToEnd());
                }
            }
            return null;
        }
    }
}
