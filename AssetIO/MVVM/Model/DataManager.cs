using AssetIO.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WinForms = System.Windows.Forms;

namespace AssetIO.MVVM.Model
{
    public class DataManager
    {

        public static ObservableCollection<AssetEntry> _DatabaseEntries { get; set; } = new ObservableCollection<AssetEntry>();
        public static ObservableCollection<AssetEntry> _db;

        public static ObservableCollection<AssetEntry> GetEntries()
        {
            return _DatabaseEntries;
        }
        public static ObservableCollection<AssetEntry> GetEntries(string path)
        {
            _db = new ObservableCollection<AssetEntry> ();
            
            var AllFiles = Directory.EnumerateFiles(path, "*.aio", SearchOption.TopDirectoryOnly);
            FileInfo fi;
            foreach (string file in AllFiles) 
            { 
                ParseFile(file);
            }

            return _db;
        }

        public static void ParseFile(string file)
        {
            try
            {
                using StreamReader reader = new(file);
                string line;
                string[] lineParse;
                AssetEntry t;
                while ((line = reader.ReadLine()) != null)
                {
                    lineParse = line.Split(",");
                    Debug.WriteLine(lineParse);
                    t = new AssetEntry(DateTime.Parse(lineParse[0]), lineParse[1], lineParse[2], (IO)Enum.Parse(typeof(IO), lineParse[3]));
                    _db.Add(t);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public static void AddEntry(AssetEntry entry)
        {
            _DatabaseEntries.Add(entry);
        }

        public static void ParseIOBoxes(string techName, string inputText, string outputText) {
            ObservableCollection<AssetEntry> newEntries = new ObservableCollection<AssetEntry>();
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings["database_folder_path"] == null || appSettings["database_folder_path"].Length == 0)
                {
                    System.Windows.MessageBox.Show("No Database folder selected. Please select a folder in Settings.");
                }
                else
                {
                    string docPath = appSettings["database_folder_path"];
                    if (inputText == null || inputText == "") { }
                    else
                    {
                        string[] inputArray = inputText.Split('\n');
                        foreach (string i in inputArray)
                        {
                            i.TrimEnd('\r', 'n');
                            if (i == null || i == "" || i == "\r" || i == " ") { }
                            else
                            {
                                AssetEntry ae = new AssetEntry(DateTime.Now, techName, i, IO.In);
                                AddEntry(ae);
                                newEntries.Add(ae);
                            }
                        }
                    }
                    if (outputText == null || outputText == "") { }
                    else
                    {
                        string[] outputArray = outputText.Split("\n");
                        foreach (string i in outputArray)
                        {
                            i.TrimEnd('\r', 'n');
                            if (i == null || i == "" || i == "\r" || i == " ") { }
                            else
                            {
                                AssetEntry ae = new AssetEntry(DateTime.Now, techName, i, IO.Out);
                                AddEntry(ae);
                                newEntries.Add(ae);
                            }
                        }
                    }
                    //Debug.WriteLine(Path.Combine(docPath, DateTime.Today.ToString("MM-dd-yyyy")+" AIOlog.csv"));
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, DateTime.Today.ToString("MM-dd-yyyy") + " AIOlog.aio"), append: true))
                    {

                        foreach (AssetEntry e in newEntries)
                        {
                            outputFile.WriteLine(e.ToString());
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Unable to write to Database. Please ensure database is selected in settings.");
            }
        } 
    }
}
