using System;
using System.Windows;

namespace MSF.Util.Framework.FolderBrowser
{
    public static class Folder
    {
        public static void SelectFolder()
        {
            //var mainWindow = Application.Current.MainWindow;

            var selectFolder = new System.Windows.Forms.FolderBrowserDialog();
            selectFolder.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var result = selectFolder.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(selectFolder.SelectedPath))
            {

            }
        }
    }
}
