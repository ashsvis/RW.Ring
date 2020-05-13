﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Notung.Helm.Properties;
using System.Runtime.InteropServices;
using System.Text;
using Notung.Log;

namespace Notung.Helm
{
  public class MainFormAppInstanceView : IAppInstanceView
  {
    private static readonly ILog _log = LogManager.GetLogger(typeof(MainFormAppInstanceView));
    private readonly Form m_main_form;

    public MainFormAppInstanceView(Form mainForm)
    {
      if (mainForm == null)
        throw new ArgumentNullException("mainForm");

      m_main_form = mainForm;
    }
    
    public ISynchronizeInvoke Invoker
    {
      get { return m_main_form; }
    }

    public const uint StringArgsMessageCode = 0xA146;

    public bool ReliableThreading
    {
      get { return true; }
    }

    public bool SupportSendingArgs
    {
      get
      {
        return m_main_form.GetType().GetMethod("WndProc", 
          BindingFlags.Instance | BindingFlags.NonPublic).DeclaringType != typeof(Form);
      }
    }

    public bool SendArgsToProcess(Process previous, IList<string> args)
    {
      var text_to_send = string.Join("\n", args);

      using (var atom = new Atom(text_to_send))
      {
        if (atom.IsValid && atom.Send(previous.MainWindowHandle, StringArgsMessageCode) != IntPtr.Zero)
        {
          WinAPIHelper.SetForegroundWindow(previous.MainWindowHandle);
          return true;
        }
      }

      using (var cd = new CopyData(Encoding.Unicode.GetBytes(text_to_send), StringArgsMessageCode))
      {
        if (cd.IsValid && cd.Send(m_main_form.Handle, previous.MainWindowHandle) != IntPtr.Zero)
        {
          WinAPIHelper.SetForegroundWindow(previous.MainWindowHandle);
          return true;
        }
      }

      return false;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct COPYDATASTRUCT
    {
      public IntPtr dwData;
      public uint cbData;
      public IntPtr lpData;
    }

    public void Restart(string startPath, IList<string> args)
    {
      System.Windows.Forms.Application.Restart();
    }

    public static string[] GetStringArgs(Message message)
    {
      if (message.Msg == StringArgsMessageCode)
      {
        _log.Debug("GetStringArgs(): atom recieved");
        using (var atom = new Atom(message.LParam, message.WParam.ToInt32()))
        {
          if (atom.Text != null)
            return atom.Text.Split('\n');
          else
            return ArrayExtensions.Empty<string>();
        }
      }
      else if (message.Msg == WinAPIHelper.WM_COPYDATA)
      {
        _log.Debug("GetStringArgs(): copy data structure recieved");
        using (var cd = new CopyData(message.LParam))
        {
          if (cd.TypeCode == StringArgsMessageCode && cd.Data != null)
            return Encoding.Unicode.GetString(cd.Data).Split('\n');
          else
            return ArrayExtensions.Empty<string>();
        }
      }
      else
        throw new ArgumentException(string.Format(Resources.NO_LINK_MESSAGE_CODE, message.Msg, MethodInfo.GetCurrentMethod().DeclaringType));
    }
  }
}
