using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Kursach_1125.Model;
using Kursach_1125.VM;

namespace Kursach_1125.View
{
    /// <summary>
    /// Логика взаимодействия для AgreementEditWindow.xaml
    /// </summary>
    public partial class AgreementEditWindow : Window
    {
        public AgreementEditWindow(Agreement agreement)
        {
            InitializeComponent();
            ((AgreementEditWindowMvvm)this.DataContext).SetClose(Close);
        }
    }
}
