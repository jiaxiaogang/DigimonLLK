using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XixiLink.Command;
using XixiLink.DataModel;
using System.ComponentModel;
using Windows.UI.Popups;


// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace XixiLink
{
    /// <summary>
    /// 可独立使用或用于导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int uiX;
        private int uiY;
        private int uixImg;
        private LinkMap linkmap = new LinkMap(12, 8, 28);
        List<LinkImage> listImgs = new List<LinkImage>();
        LinkBlock firstBlock = null;
        LinkImage firstImg = null;
        List<string> listTag = new List<string>();
        public MainPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                LoadMapGrid();
            }
        }

        private void LoadMapGrid()
        {
            //gameGrid.ClearValue(Grid.RowProperty);
            //gameGrid.ClearValue(Grid.ColumnProperty);
            for (int i = 0; i < LinkMap.MapY; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                gameGrid.RowDefinitions.Add(rowDef);
            }

            for (int i = 0; i < LinkMap.MapX; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                gameGrid.ColumnDefinitions.Add(colDef);
            }
            for (int i = 0; i < LinkMap.MapY; i++)
            {
                for (int j = 0; j < LinkMap.MapX; j++)
                {
                    LinkImage linkImg = new LinkImage();
                    linkImg.DataContext = linkmap[j, i];
                    linkImg.Tag = j.ToString() + i.ToString();
                    listTag.Add(linkImg.Tag);
                    listImgs.Add(linkImg);
                    linkImg.Tapped += linkImg_Tapped;
                    gameGrid.Children.Add(linkImg);
                    Grid.SetColumn(linkImg, j);
                    Grid.SetRow(linkImg, i);
                }
            }
            linkmap.Restart();
        }





        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listImgs.Clear();
            listTag.Clear();
            for (int i = 0; i < LinkMap.MapY; i++)
            {
                for (int j = 0; j < LinkMap.MapX; j++)
                {
                    LinkImage linkImg = new LinkImage();
                    linkImg.DataContext = linkmap[j, i];
                    linkImg.Tag = j.ToString() + i.ToString();
                    listTag.Add(linkImg.Tag);
                    listImgs.Add(linkImg);
                    linkImg.Tapped += linkImg_Tapped;
                    gameGrid.Children.Add(linkImg);
                    Grid.SetColumn(linkImg, j);
                    Grid.SetRow(linkImg, i);
                }
            }
            linkmap.Restart();
            btnNew.Content = "重新开始";
        }

        void linkImg_Tapped(object sender, TappedRoutedEventArgs e)
        {
            bo.Play();
            if (firstBlock == null)
            {
                firstImg = sender as LinkImage;
                firstBlock = (sender as LinkImage).DataContext as LinkBlock;
                firstImg.StartFlash();
            }
            else
            {
                LinkBlock secondBlock = (sender as LinkImage).DataContext as LinkBlock;
                (sender as LinkImage).StopFlash();
                firstImg.StopFlash();
                if (linkmap.isCanDead(firstBlock, secondBlock))
                {
                    firstBlock.IsAlive = false;
                    secondBlock.IsAlive = false;
                    firstBlock = null;
                }
                else
                {
                    firstBlock = null;
                }
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                uiX = Convert.ToInt32(txtbX.Text);
                uiY = Convert.ToInt32(txtbY.Text);
                if (uiX*uiY<130&&5<uiX&&uiX<15&&5<uiY&&uiY<15)
                {
                    uixImg = uiX * uiY / 4;
                    linkmap = new LinkMap(uiX, uiY, uixImg);
                    LoadMapGrid();
                }
                else
                {
                    MessageDialog msd = new MessageDialog("要求宽和高必须大于5并且小于15并且难度别太高");
                    await msd.ShowAsync();
                }
            }
            catch
            {
                txtbX.Text = "错误";
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LinkBlock[] linkHelps = linkmap.HelpMe();
            if (linkHelps[0] != null)
            {
                LinkBlock b1 = linkHelps[0];
                LinkBlock b2 = linkHelps[1];
                LinkImage i1 = listImgs.First(linkImage => linkImage.Tag == b1.Tag);
                LinkImage i2 = listImgs.First(linkImage => linkImage.Tag == b2.Tag);
                i1.StartFlash();
                i2.StartFlash();
                b1 = null; b2 = null; linkHelps = null;
            }
            else
            {
                MessageDialog msd = new MessageDialog("没有可连了你完蛋了！");
                await msd.ShowAsync();
            }
            //int i, j;
            //i = linkHelps[0].X;
            //j = linkHelps[0].Y;


            //foreach(var itemLinkImg in listImgs.Where(LinkImage =>LinkImage.Tag==b1.Tag))
            //{
            //    if (i1==null)
            //    {
            //        i1 = itemLinkImg as LinkImage;
            //    }
            //}
            // foreach(var itemLinkImg in listImgs.Where(LinkBlock=>LinkBlock.Tag==b2.Tag))
            // {
            //     if(i2==null)
            //     {
            //         i2 = itemLinkImg as LinkImage;
            //     }
            // }

            //i1=where(b1=>b1.Tag==i1.Tag)
            //where(x=>x.Tag==控件.Tag)

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            linkmap.Refresh();
        }
    }
}
