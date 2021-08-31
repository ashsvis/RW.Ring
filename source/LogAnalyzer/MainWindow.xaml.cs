﻿using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace LogAnalyzer
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void OpenFile_Click(object sender, RoutedEventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();

      dialog.Filter = "Log files|*.log";

      if (dialog.ShowDialog(this) == true)
      {
        ((TablePresenter)this.DataContext).OpenFile(dialog.FileName);
      }
    }

    private void DataGrid_AutoGeneratedColumns(object sender, EventArgs e)
    {
      var grid = (DataGrid)sender;

      if (grid.Columns.Count > 0)
      grid.Columns[grid.Columns.Count - 1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
    }

    private void FilterRow_TextChanged(object sender, TextChangedEventArgs e)
    {
      var ctrl = sender as TextBox;

      var obj= ctrl.TemplatedParent as DataGridColumnHeader;

      var table = ((TablePresenter)this.DataContext).LoadedTable;

      if (string.IsNullOrEmpty(ctrl.Text))
        table.DefaultView.RowFilter = null;

      var name = ((Binding)((DataGridBoundColumn)obj.Column).Binding).Path.Path;

      if (table.Columns[name].DataType == typeof(string))
      {
        table.DefaultView.RowFilter = name + " LIKE '" + ctrl.Text + "%'";
      }
      if (table.Columns[name].DataType.IsPrimitive && table.Columns[name].DataType != typeof(bool))
      {
        int value;
        if (int.TryParse(ctrl.Text, out value))
        {
          table.DefaultView.RowFilter = name + " = " + ctrl.Text;
        }
      }
    }

    private void OpenConfig_Click(object sender, RoutedEventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();

      dialog.Filter = "Config files|*.config";

      if (dialog.ShowDialog(this) == true)
      {
        ((TablePresenter)this.DataContext).OpenConfig(dialog.FileName);
      }
    }

    private void MenuItemClose_Click(object sender, RoutedEventArgs e)
    {
      ((TablePresenter)this.DataContext).CloseCurrent();
    }
  }
}
