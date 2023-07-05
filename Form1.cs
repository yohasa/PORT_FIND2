using System.Management;
using System.IO.Ports;
using System.ComponentModel;

namespace PORT_FIND2
{
    public partial class Form1 : Form
    {
        List<string> portnames= new List<string>();
        public Form1()
        {
            InitializeComponent();
            portnames = GetPorts();
            foreach (string port in portnames)
            {
                richTextBox1.AppendText(port + "\r\n");
            }
        }

        public List<string> GetPorts()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM " +
                 "Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                string[] portnames = SerialPort.GetPortNames();

                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());
                
                List<string> portList = portnames.Select(n => n + " - " + ports.FirstOrDefault(s => s.Contains(n))).ToList();
                
                return portList;

            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {

        }
    }
}