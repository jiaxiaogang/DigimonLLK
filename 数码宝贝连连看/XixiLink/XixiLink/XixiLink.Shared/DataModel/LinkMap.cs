using System;
using System.Collections.Generic;
using System.Text;

namespace XixiLink.DataModel
{
    public class LinkMap
    {
        public LinkMap(int mapx, int mapy, int ximg)
        {
            mapX = mapx; mapY = mapy; xImg = ximg;
            data = new LinkBlock[mapx, mapy];
            for (int i = 0; i < mapx; i++)
            {
                for (int j = 0; j < mapy; j++)
                {
                    data[i, j] = new LinkBlock() { X = i, Y = j };
                }
            }
        }
        private static int mapX;
        private static int mapY;
        public static int xImg;
        LinkBlock[,] data = null;



        //地图X轴长度
        public static int MapX
        {
            get { return LinkMap.mapX; }
            set { LinkMap.mapX = value; }
        }
        //地图Y轴长度
        public static int MapY
        {
            get { return LinkMap.mapY; }
            set { LinkMap.mapY = value; }
        }



        public LinkBlock this[int x, int y]
        {
            get
            {
                return data[x, y];
            }
        }

        public bool isCanDead(LinkBlock b1, LinkBlock b2)
        {
            if (b1.ImgType != b2.ImgType)
            {
                return false;
            }
            if (b1 == b2)
            {
                return false;
            }
            if (XCanDead(b1, b2) || YCanDead(b1, b2))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// X两折
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public bool XCanDead(LinkBlock b1, LinkBlock b2)
        {
            if (b1.X == b2.X)
            {
                if (IsCanDeadLine(b1, b2))
                {
                    return true;
                }
            }
            for (int i = 0; i < mapX; i++)
            {
                for (int j = 0; j < mapX; j++)
                {
                    if (i == j)
                    {
                        if (i == 0 && j == 0 || i == mapX - 1 && j == mapX - 1)
                        {
                            if (IsCanDeadLine(data[i, b1.Y], b1) && IsCanDeadLine(data[j, b2.Y], b2) && !data[i, b1.Y].IsAlive && !data[j, b2.Y].IsAlive)
                            {
                                return true;
                            }
                        }
                        if (b1.X == i || b2.X == j)
                        {
                            if (IsCanDeadCorner(b1, b2))
                            {
                                return true;
                            }
                        }
                        if (IsCanDeadCorner(data[i, b2.Y], b1) && IsCanDeadCorner(data[j, b1.Y], b2)&&IsCanDeadLine(data[i,b2.Y],b2)&&IsCanDeadLine(data[j,b1.Y],b1))
                        {
                            return true;
                        }
                        if (b1.X == 0 && b2.X == 0 || b1.X == mapX - 1 && b2.X == mapX - 1)
                        {
                            return true;
                        }
                        if (b1.X == 0)
                        {
                            if (IsCanDeadLine(data[0, b2.Y], b2) && !data[0, b2.Y].IsAlive)
                            { return true; }
                        }
                        if (b2.X == 0)
                        {
                            if (IsCanDeadLine(data[0, b1.Y], b1) && !data[0, b1.Y].IsAlive)
                            { return true; }
                        }
                        if (b1.X == mapX - 1)
                        {
                            if (IsCanDeadLine(data[mapX - 1, b2.Y], b2) && !data[mapX - 1, b2.Y].IsAlive)
                            { return true; }
                        }
                        if (b2.X == mapX - 1)
                        {
                            if (IsCanDeadLine(data[mapX - 1, b1.Y], b1) && !data[mapX - 1, b1.Y].IsAlive)
                            { return true; }
                        }
                    }
                }
            }
            return false;
        }



        /// <summary>
        /// Y两折
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public bool YCanDead(LinkBlock b1, LinkBlock b2)
        {
            if (b1.Y == b2.Y)
            {
                if (IsCanDeadLine(b1, b2))
                {
                    return true;
                }
            }
            for (int i = 0; i < mapY; i++)
            {
                for (int j = 0; j < mapY; j++)
                {
                    if (i == j)
                    {
                        if (i == 0 && j == 0 || i == mapY - 1 && j == mapY - 1)
                        {
                            if (IsCanDeadLine(data[b1.X, i], b1) && IsCanDeadLine(data[b2.X, j], b2) && !data[b1.X, i].IsAlive && !data[b2.X, j].IsAlive)
                            {
                                return true;
                            }
                        }
                        if (b1.Y == i || b2.Y == j)
                        {
                            if (IsCanDeadCorner(b1, b2))
                            {
                                return true;
                            }
                        }
                        if (IsCanDeadCorner(data[b2.X, i], b1) && IsCanDeadCorner(data[b1.X, j], b2)&&IsCanDeadLine(data[b2.X,i],b2)&&IsCanDeadLine(data[b1.X,j],b1))
                        {
                            return true;
                        }
                        if (b1.Y == 0 && b2.Y == 0 || b1.Y == mapY - 1 && b2.Y == mapY - 1)
                        {
                            return true;
                        }
                        if (b1.Y == 0)
                        {
                            if (IsCanDeadLine(data[b2.X, 0], b2) && !data[b2.X, 0].IsAlive)
                            {
                                return true;
                            }
                        }
                        if (b2.Y == 0)
                        {
                            if (IsCanDeadLine(data[b1.X, 0], b1) && !data[b1.X, 0].IsAlive)
                            { return true; }
                        }
                        if (b1.Y == mapY - 1)
                        {
                            if (IsCanDeadLine(data[b2.X, mapY - 1], b2) && !data[b2.X, mapY - 1].IsAlive)
                            { return true; }
                        }
                        if (b2.Y == mapY - 1)
                        {
                            if (IsCanDeadLine(data[b1.X, mapY - 1], b1) && !data[b1.X, mapY - 1].IsAlive)
                            { return true; }
                        }
                    }
                }
            }
            return false;
        }





        /// <summary>
        /// 判断一折可不可以相连
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public bool IsCanDeadCorner(LinkBlock b1, LinkBlock b2)
        {
            LinkBlock corner1 = data[b1.X, b2.Y];
            LinkBlock corner2 = data[b2.X, b1.Y];
            if (!corner1.IsAlive)
            {
                if (IsCanDeadLine(corner1, b1) && IsCanDeadLine(corner1, b2))
                {
                    return true;
                }
            }
            if (!corner2.IsAlive)
            {
                if (IsCanDeadLine(corner2, b1) && IsCanDeadLine(corner2, b2))
                {
                    return true;
                }
            }
            return false;
        }




        /// <summary>
        /// 判断直线是否可以相连的方法
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public bool IsCanDeadLine(LinkBlock l1, LinkBlock l2)
        {
            if (l1.X == l2.X && l1.Y == l2.Y)
            { return true; }
            else if (l1.X == l2.X)
            {
                for (int i = Math.Min(l1.Y, l2.Y) + 1; i < Math.Max(l1.Y, l2.Y); i++)
                {
                    if (data[l1.X, i].IsAlive)
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (l1.Y == l2.Y)
            {
                for (int i = Math.Min(l1.X, l2.X) + 1; i < Math.Max(l1.X, l2.X); i++)
                {
                    if (data[i, l1.Y].IsAlive)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }



        public void Restart()
        {
            Random r = new Random();
            foreach (LinkBlock linkBlock in data)
            {
                linkBlock.IsAlive = true;
                linkBlock.ImgType = r.Next(1, xImg + 1);
            }


            //判断单双方法：
            int dansuansu = 1;
            while (xImg > dansuansu)
            {
                int i1 = 0;
                foreach (LinkBlock linkBlock in data)
                {
                    if (linkBlock.ImgType == dansuansu)
                    {
                        i1 += 1;
                    }
                }
                if (i1 % 2 == 1)
                {
                    bool b = true;
                    foreach (LinkBlock linkBlock in data)
                    {
                        if (b)
                        {
                            if (linkBlock.ImgType == dansuansu)
                            {
                                linkBlock.ImgType = r.Next(dansuansu + 1, xImg + 1);
                                b = false;
                                continue;
                            }
                        }
                    }
                }
                dansuansu += 1;
            }
        }


        public void Refresh()
        {
            Random r = new Random();
            List<LinkBlock> stillAlives = new List<LinkBlock>();
            foreach (LinkBlock linkBlock in data)
            {
                if(linkBlock.IsAlive)
                {
                    stillAlives.Add(linkBlock);
                }
                foreach (LinkBlock stillAlive in stillAlives)
                {
                    stillAlive.ImgType = r.Next(1, xImg + 1);
                }
            }


            //判断单双方法：
            int dansuansu = 1;
            while (xImg > dansuansu)
            {
                int i1 = 0;
                foreach (LinkBlock linkBlock in stillAlives)
                {
                    if (linkBlock.ImgType == dansuansu)
                    {
                        i1 += 1;
                    }
                }
                if (i1 % 2 == 1)
                {
                    bool b = true;
                    foreach (LinkBlock linkBlock in stillAlives)
                    {
                        if (b)
                        {
                            if (linkBlock.ImgType == dansuansu)
                            {
                                linkBlock.ImgType = r.Next(dansuansu + 1, xImg + 1);
                                b = false;
                                continue;
                            }
                        }
                    }
                }
                dansuansu += 1;
            }
        }



        //智能提示
        public LinkBlock[] HelpMe()
        {
            LinkBlock[] linkBlock = new LinkBlock[2];
            List<LinkBlock> listAlive = new List<LinkBlock>();
            List<LinkBlock> listImgVSImg = new List<LinkBlock>();
            foreach (LinkBlock linkB in data)
            {
                if (linkB.IsAlive)
                {
                    listAlive.Add(linkB);
                }
            }

            for (int iImg = 1; iImg <= xImg; iImg++)
            {
                foreach (LinkBlock linkB in listAlive)
                {
                    if (linkB.ImgType == iImg)
                    {
                        listImgVSImg.Add(linkB);
                    }
                }
                for (int i = 0; i < listImgVSImg.Count-1; i++)
                {
                    for (int j = i + 1; j < listImgVSImg.Count; j++)
                    {
                        if (isCanDead(listImgVSImg[i], listImgVSImg[j]))
                        {
                            linkBlock[0] = listImgVSImg[i];
                            linkBlock[1] = listImgVSImg[j];
                            return linkBlock;
                        }
                    }
                }
                listImgVSImg.Clear();
            }
            return linkBlock;
        }
    }

}