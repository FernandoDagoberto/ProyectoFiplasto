using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFiplasto.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BioTabPage : TabbedPage
	{
		public BioTabPage()
		{
			InitializeComponent ();
            Children.Add(new BioPendientePage());
            Children.Add(new BioCargadosPage());
            Children.Add(new BioInformePage());
        }
	}
}