using ClientAppliaction.Models;
using ClientAppliaction.Networking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace ClientAppliaction.Pages
{
    public partial class MainWindow : Window
    {
        public List<Card> Cards { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Cards = new List<Card>();
            getCardsData();
        }

        private void getCardsData()
        {
            Cards = Task.Run(() => AppWebRequests.GetRequest<List<Card>>(Links.GetCards)).Result.Value;
            foreach (Card card in Cards)
            {
                card.ImageSourceString = card.ImageSourceString
                    .Replace("data:image/png;base64,", "")
                    .Replace("data:image/jgp;base64,", "")
                    .Replace("data:image/jpg;base64,", "")
                    .Replace("data:image/jpeg;base64,", "");
                byte[] bytes = Convert.FromBase64String(card.ImageSourceString);
                MemoryStream memStream = new MemoryStream(bytes);
                System.Drawing.Image mImage = System.Drawing.Image.FromStream(memStream);
                System.Windows.Controls.Image cardImage = new System.Windows.Controls.Image()
                {
                    Height = 400,
                    Width = 400,
                    Source = convertBitmap(new Bitmap(mImage)),
                };
                cardsListView.Items.Add(cardImage);
            }
        }

        private BitmapImage convertBitmap(System.Drawing.Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}
