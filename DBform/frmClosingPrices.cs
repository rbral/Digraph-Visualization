using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DBform
{
    public partial class frmClosingPrices : Form
    {
        public frmClosingPrices()
        {
            InitializeComponent();
        }


        private void btnGetData_Click(object sender, EventArgs e)
        {
            SqlConnection sqlCon = null;

            try
            {
                /* get database parameters from App.config file */
                String strServer = ConfigurationManager.AppSettings["server"];
                String strDatabase = ConfigurationManager.AppSettings["database"];

                /* open a connection to database */
                //  typical connection string:
                //      sqlCon = new SqlConnection("Server=DESKTOP-17VOE83;Database=Finance;Trusted_Connection=True;");
                String strConnect = $"Server={strServer};Database={strDatabase};Trusted_Connection=True;";
                sqlCon = new SqlConnection(strConnect);
                sqlCon.Open();

                /* prepare parameters for stored procedure  called below */
                double minPrc = Convert.ToDouble(nudWprice.Value);
                String symbol = tbSymbol.Text;

                /* set up a call to spGetPrcForSymbol stored procedure */
                SqlCommand sqlCmd = new SqlCommand("Select date, [Close] as Price, volume from TS_DailyData WHERE Ticker = '" +
                    symbol +"'",sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                //sqlCmd.Parameters.Add("@symbol", System.Data.SqlDbType.VarChar).Value = symbol;
                //sqlCmd.Parameters.Add("@MinPrc", System.Data.SqlDbType.Float).Value = minPrc;

                /* execute spGetPrcForSymbol */
                sqlCmd.ExecuteNonQuery();

                /* get the data returned by spGetPrcForSymbol and display it */
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataSet dataset = new DataSet();
                da.Fill(dataset, "Prices");
                dgvData.AutoGenerateColumns = true;
                dgvData.DataSource = dataset.Tables["Prices"];

                //// show a single cell (remove this call):
                //showSingleCell(dataset);

                dgvData.Columns["Price"].DefaultCellStyle.Format = "c";
                dgvData.Columns["volume"].DefaultCellStyle.Format = "#,###";
                UpdateChart(dataset);
                UpdateChart2(dataset);
                
            }

            catch (Exception ex)

            {
                MessageBox.Show(" " + DateTime.Now.ToLongTimeString() + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally

            {

                if (sqlCon != null && sqlCon.State == System.Data.ConnectionState.Open)

                    sqlCon.Close();

            }


        }
        private void UpdateChart(DataSet dataset)
        {
            chrtPrices.Series[0].Points.Clear();
            var nrRows = dataset.Tables["Prices"].Rows.Count;
            double maxPr = Double.MinValue;
            double minPr = Double.MaxValue;
            for (int row = 1; row < nrRows; ++row)
            {
                DateTime date = (DateTime)dataset.Tables["Prices"].Rows[row].ItemArray[0];
                double price = (double)dataset.Tables["Prices"].Rows[row].ItemArray[1];
                chrtPrices.Series[0].Points.AddXY(date, price);
                if (price > maxPr) maxPr = price;
                if (price < minPr) minPr = price;
            }
            chrtPrices.ChartAreas[0].AxisY.Maximum = Math.Ceiling(1.1 * maxPr);
            chrtPrices.ChartAreas[0].AxisY.Minimum = Math.Floor(0.9 * minPr);
        }


        private void UpdateChart2(DataSet dataset)
        {
            //chart1.Series[0].Points.Clear();
            //var nrRows = dataset.Tables["Prices"].Rows.Count;
            //for (int row = 1; row < nrRows; ++row)
            //{
            //    DateTime date = (DateTime)dataset.Tables["Prices"].Rows[row].ItemArray[0];
            //    double price = (double)dataset.Tables["Prices"].Rows[row].ItemArray[1];
            //    chart1.Series[0].Points.AddXY(date, price);
            //}
        }
        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You clicked me");
        }

        private void showSingleCell(DataSet dataset)
        {
            double q = (double)dataset.Tables["Prices"].Rows[2].ItemArray[1];
            MessageBox.Show($"Rows[2].ItemArray[1] = {q}");
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
