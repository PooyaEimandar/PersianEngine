using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace PersianEditor.Windows
{
    public partial class Example : System.Windows.Window
    {
        Dictionary<int, ToggleButton> toggleButtons;

        public Example()
        {
            InitializeComponent();
            InitializeFields();
        }

        private void InitializeFields()
        {
            this.toggleButtons = new Dictionary<int, ToggleButton>();
            this.tileView1.ItemsSource = new Countries();
            this.Dispatcher.BeginInvoke(new Action(this.GetMaximizeButtons));
        }

        private void GetMaximizeButtons()
        {
            for (int i = 0; i < this.tileView1.Items.Count; i++)
            {
                RadTileViewItem tileViewItem = this.tileView1.ItemContainerGenerator.ContainerFromIndex(i) as RadTileViewItem;
                if (tileViewItem != null)
                {
                    Panel visualRoot = VisualTreeHelper.GetChild(tileViewItem, 0) as Panel;
                    if (visualRoot != null)
                    {
                        ToggleButton maximizedToggleButton = visualRoot.FindName("MaximizeToggleButton") as ToggleButton;
                        if (maximizedToggleButton != null)
                        {
                            this.toggleButtons.Add(i, maximizedToggleButton);

                            if (i == 0)
                            {
                                maximizedToggleButton.Opacity = 0.0;
                            }
                        }
                    }
                }
            }
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            RadTileViewItem tileViewItem = image.ParentOfType<RadTileViewItem>();
            if (tileViewItem != null)
            {
                tileViewItem.TileState = TileViewItemState.Maximized;
            }
        }

        private void TileView1_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            if (this.toggleButtons.Count == 0)
            {
                return;
            }

            RadTileViewItem tileViewItem = e.OriginalSource as RadTileViewItem;
            int index = this.tileView1.ItemContainerGenerator.IndexFromContainer(tileViewItem);
            if (tileViewItem.TileState == TileViewItemState.Maximized)
            {
                this.toggleButtons[index].Opacity = 0.0;
            }
            else
            {
                this.toggleButtons[index].Opacity = 1.0;
            }
        }
    }

    public class Country
    {
        public string LargeFlag { get; set; }
        public string Name { get; set; }
        public string PoliticalSystem { get; set; }
        public string CapitalCity { get; set; }
        public string TotalArea { get; set; }
        public string Population { get; set; }
        public string Currency { get; set; }
        public string OfficialLanguage { get; set; }
        public string Description { get; set; }

        public Country(string name)
        {
            this.LargeFlag = string.Format("/PersianEditor;component/Resources/Images/Intro/{0}.png", name);
        }
    }

    public class Countries : List<Country>
    {
        public Countries()
        {
            Country austria = new Country("G");
            austria.PoliticalSystem = "Islamic Republic";
            austria.CapitalCity = "Tehran";
            austria.TotalArea = "100000 sq. km";
            austria.Population = "75 million";
            austria.Currency = "Rial";
            austria.OfficialLanguage = "Farsi";
            austria.Description = "Belgium is a federal state divided into three regions: Dutch-speaking Flanders in the north, francophone Wallonia in the south and Brussels, the bilingual capital, where French and Dutch share official status. There is also a small German-speaking minority of some 70 000 in the eastern part of the country. Belgium’s landscape varies widely: 67 kilometres of seacoast and flat coastal plains along the North Sea, a central plateau and the rolling hills and forests of the Ardennes region in the southeast. Brussels hosts several international organisations: most of the European institutions are located here as well as the NATO headquarters. Independent since 1830, Belgium is a constitutional monarchy. The two houses of Parliament are the Chamber of Representatives, whose members are elected for a maximum period of four years, and the Senate or upper house, whose members are elected or co-opted. Given its political make-up, Belgium is generally run by coalition governments.";
            this.Add(austria);

            Country belgium = new Country("G");
            belgium.PoliticalSystem = "Constitutional monarchy";
            belgium.CapitalCity = "Brussels";
            belgium.TotalArea = "30 528 sq. km";
            belgium.Population = "10.7 million";
            belgium.Currency = "euro";
            belgium.OfficialLanguage = "German, French, Dutch";
            belgium.Description = "Belgium is a federal state divided into three regions: Dutch-speaking Flanders in the north, francophone Wallonia in the south and Brussels, the bilingual capital, where French and Dutch share official status. There is also a small German-speaking minority of some 70 000 in the eastern part of the country. Belgium’s landscape varies widely: 67 kilometres of seacoast and flat coastal plains along the North Sea, a central plateau and the rolling hills and forests of the Ardennes region in the southeast. Brussels hosts several international organisations: most of the European institutions are located here as well as the NATO headquarters. Independent since 1830, Belgium is a constitutional monarchy. The two houses of Parliament are the Chamber of Representatives, whose members are elected for a maximum period of four years, and the Senate or upper house, whose members are elected or co-opted. Given its political make-up, Belgium is generally run by coalition governments.";
            this.Add(belgium);

            Country bulgaria = new Country("G");
            bulgaria.PoliticalSystem = "Republic";
            bulgaria.CapitalCity = "Sofia";
            bulgaria.TotalArea = "111 910 sq. km";
            bulgaria.Population = "7.6 million";
            bulgaria.Currency = "lev";
            bulgaria.OfficialLanguage = "Bulgarian";
            bulgaria.Description = "Located in the heart of the Balkans, Bulgaria offers a highly diverse landscape: the north is dominated by the vast lowlands of the Danube and the south by the highlands and elevated plains. In the east, the Black Sea coast attracts tourists all year round. Founded in 681, Bulgaria is one of the oldest states in Europe. Its history is marked by its location near Europe’s frontier with Asia. Some 85% of the population are Orthodox Christians and 13% Muslims. Around 10% of the population are of Turkish origin while 3% are Roma. Similarly, its traditional dishes are a mixture of east and west. The most famous Bulgarian food must be yoghurt, with its reputed gift of longevity for those who consume it regularly.";
            this.Add(bulgaria);
        }
    }
}