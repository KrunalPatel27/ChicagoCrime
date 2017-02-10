using System;
using System.Windows.Forms;


namespace ChicagoCrime
{
  public partial class Form1 : Form
  {
     public Form1()
    {
      InitializeComponent();
    }

    private bool doesFileExist(string filename)
    {
      if (!System.IO.File.Exists(filename))
      {
        string msg = string.Format("Input file not found: '{0}'",
          filename);

        MessageBox.Show(msg);
        return false;
      }

      // exists!
      return true;
    }

    private void clearForm()
    {
      // 
      // if a chart is being displayed currently, remove it:
      //
      if (this.graphPanel.Controls.Count > 0)
      {
        this.graphPanel.Controls.RemoveAt(0);
        this.graphPanel.Refresh();
      }
    }

    private void cmdByYear_Click(object sender, EventArgs e)
    {
      string filename = this.txtFilename.Text;

      if (!doesFileExist(filename))
        return;

      clearForm();

      //
      // Call over to F# code to analyze data and return a 
      // chart to display:
      //
      this.Cursor = Cursors.WaitCursor;

      var chart = FSAnalysis.CrimesByYear(filename);

      this.Cursor = Cursors.Default;

      //
      // we have chart, display it:
      //
      this.graphPanel.Controls.Add(chart);
      this.graphPanel.Refresh();
    }

         private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filename = this.txtFilename.Text;

            if (!doesFileExist(filename))
                return;

            clearForm();

            //
            // Call over to F# code to analyze data and return a 
            // chart to display:
            //
            this.Cursor = Cursors.WaitCursor;

            var chart = FSAnalysis.ArrestByYears(filename);

            this.Cursor = Cursors.Default;
            //
            // we have chart, display it:
            this.graphPanel.Controls.Add(chart);
            this.graphPanel.Refresh();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filename = this.txtFilename.Text;
            string filename2 = "IUCR-codes.csv";
            if (!doesFileExist(filename) || !doesFileExist(filename2))
                return;

            clearForm();
            string textBox1Txt = this.textBox1.Text;

            //
            // Call over to F# code to analyze data and return a 
            // chart to display:
            //
            this.Cursor = Cursors.WaitCursor;

            var chart = FSAnalysis.CrimebyIUCR(filename, filename2, textBox1Txt);

            this.Cursor = Cursors.Default;
            //
            // we have chart, display it:
            this.graphPanel.Controls.Add(chart);
            this.graphPanel.Refresh();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filename = this.txtFilename.Text;
            string filename2 = "Areas.csv";
            if (!doesFileExist(filename) || !doesFileExist(filename2))
                return;

            clearForm();
            string textBox2Txt = this.textBox2.Text;

            //
            // Call over to F# code to analyze data and return a 
            // chart to display:
            //
            this.Cursor = Cursors.WaitCursor;

            var chart = FSAnalysis.CrimebyAreas(filename, filename2, textBox2Txt);

            this.Cursor = Cursors.Default;
            //
            // we have chart, display it:
            this.graphPanel.Controls.Add(chart);
            this.graphPanel.Refresh();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string filename = this.txtFilename.Text;
            string filename2 = "Areas.csv";
            if (!doesFileExist(filename) || !doesFileExist(filename2))
                return;
            
            clearForm();

            //
            // Call over to F# code to analyze data and return a 
            // chart to display:
            //
            this.Cursor = Cursors.WaitCursor;

            var chart = FSAnalysis.totalCrimes(filename,filename2);

            this.Cursor = Cursors.Default;

            //
            // we have chart, display it:
            //
            this.graphPanel.Controls.Add(chart);
            this.graphPanel.Refresh();

        }
    }//class
}//namespace
