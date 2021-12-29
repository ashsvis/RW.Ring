﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Schicksal.Regression;

namespace Schicksal.Helm
{
  public partial class CorrelationForm : Form
  {
    public CorrelationForm()
    {
      this.InitializeComponent();
    }

    public DataTable Table { get; set; }

    public string Factor { get; set; }

    public string Effect { get; set; }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      var results = new CorrelationResults(this.Table, this.Factor, this.Effect);
      var data = results.Run((x, y) => m_chart.Series[0].Points.AddXY(x, y));

      m_chart.Series[0].Name = this.Factor;
      m_chart.Series[1].Name = data.ToString();
      m_chart.Series[1].Points.AddXY(data.MinX, data.MinY);
      m_chart.Series[1].Points.AddXY(data.MaxX, data.MaxY);
    }

    private void m_cmd_copy_Click(object sender, EventArgs e)
    {
      var image = new Bitmap(m_chart.Width, m_chart.Height);

      m_chart.DrawToBitmap(image, m_chart.DisplayRectangle);

      Clipboard.SetImage(image);
    }
  }
}