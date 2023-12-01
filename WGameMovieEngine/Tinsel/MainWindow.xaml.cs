using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WGameMovieEngine.Engine;
using WGameMovieEngine.Engine.UI;
using System.IO.Compression;
using System.Windows.Documents;
using System.Xml.Linq;
using WGameMovieEngine.Engine.Resources;

namespace WGameMovieEngine
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.Hide();

            new Main();
        }
    }
}
