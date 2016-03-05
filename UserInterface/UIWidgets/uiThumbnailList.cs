using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Photoman.UserInterface.UIData;

namespace Photoman.UserInterface.UIWidgets
{
    public delegate void IndexChangedHandler();

    public partial class uiThumbnailList : UserControl
    {
        public IndexChangedHandler IndexChanged;

        public enum ThumbnailMode
        {
            LoadedImages = 0,
            ImageHistory
        }

        public ThumbnailMode Thumbnail_Mode { get; set; }
        //Make sure to comment out where this gets set in the designer for the parent object!
        private Size SelectableElementSize { get; set; }

        private int m_iIndex = -1;
        public int Index
        {
            private set
            {
                m_iIndex = value;
                if (pnlMain != null)
                    pnlMain.Refresh();//pnlMain.Invalidate(true);
                if(IndexChanged != null)
                    IndexChanged();
            }
            get { return m_iIndex; }
            
        }

        public int Count
        {
            get
            {
                if (m_dbcDisplayItems != null)
                    return m_dbcDisplayItems.Count;
                else
                    return 0;
            }
        }

        private Color m_cPanelA = Color.Gray;
        private Color m_cPanelB = Color.LightGray;
        private Color m_cPanelSelected = Color.DarkBlue;

        private List<DoubleBufferedControl> m_dbcDisplayItems = new List<DoubleBufferedControl>();
        private List<Bitmap> m_bmpThumbnails = new List<Bitmap>();
        private List<String> m_strThumbText = new List<String>();
        private List<bool> m_bBackgroundColor = new List<bool>();


        public uiThumbnailList()
        {
            InitializeComponent();

            SelectableElementSize = new Size(pnlMain.Width, Constants.iThumbnailSize + 2);
            this.MouseWheel += new MouseEventHandler(uiThumbnailList_MouseWheel);
            this.vsbMain.SmallChange = SelectableElementSize.Height;
            this.vsbMain.LargeChange = SelectableElementSize.Height * 2;
        }

        void uiThumbnailList_MouseWheel(object sender, MouseEventArgs e)
        {
            int iOldVal = vsbMain.Value;
            try
            {
                if (iOldVal - (e.Delta / 120) < 0 || iOldVal - (e.Delta / 120) > vsbMain.Maximum)
                    return;
                vsbMain.Value -= vsbMain.SmallChange * (e.Delta / 120);
                pnlMain.Location = new Point(pnlMain.Location.X, pnlMain.Location.Y + iOldVal - vsbMain.Value);
            }
            catch (Exception ex)
            {
                vsbMain.Value = iOldVal;
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
            }
        }

        public void SetIndex(int Index)
        {
            this.Index = Index;
        }

        protected virtual void OnIndexChanged()
        {
            if (IndexChanged != null)
                IndexChanged();
        }

        public void AddElement(Bitmap ImageForThumbnail, string FilePath)
        {

            //Hide Horizontal scrollbar. Dont understand bits value
            //this.SetScrollState(0, false);


            DoubleBufferedControl dbc = new DoubleBufferedControl();
            dbc.Paint += new PaintEventHandler(PaintElement);
            dbc.Click += new EventHandler(ElementClicked);
            dbc.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            
            if (m_dbcDisplayItems.Count == 0)
            {
                dbc.Location = new Point(0, 0);
                m_bBackgroundColor.Add(false);
            }
            else
            {
                dbc.Location = new Point(0, m_dbcDisplayItems[m_dbcDisplayItems.Count - 1].Location.Y + Constants.iThumbnailSize + 2);
                m_bBackgroundColor.Add(!m_bBackgroundColor[m_dbcDisplayItems.Count - 1]);
            }

            dbc.Size = new Size(pnlMain.Width, SelectableElementSize.Height);
            

            //Set width to match panel

            //Add the thumbnail. For now stretch image instead of resize + letterbox

            //Create a temp transparent bitmap
            Bitmap bmp = new Bitmap(Constants.iThumbnailSize, Constants.iThumbnailSize);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.FillRectangle(Brushes.Transparent, new Rectangle(0,0,bmp.Width,bmp.Height));

            //Get scaled size for thumbnail
            Bitmap bmpResized = new Bitmap(ImageForThumbnail, Core.BasicOps.MaintainAspectRatio_DontExcedeNewSize(new Size(Constants.iThumbnailSize, Constants.iThumbnailSize), ImageForThumbnail.Size));
            //Get offset
            Point pntOffset = new Point((Constants.iThumbnailSize - bmpResized.Size.Width) / 2, (Constants.iThumbnailSize - bmpResized.Size.Height) / 2);
            //Draw onto transparent bitmap
            gfx.DrawImage(bmpResized, pntOffset);
            m_bmpThumbnails.Add(bmp);

            //Add text
            m_strThumbText.Add(FilePath);
            
            //Pass through events
            dbc.KeyDown += new KeyEventHandler(uiThumbnailList_KeyDown);
            dbc.GotFocus += new EventHandler(TransferFocusToPanel);
            dbc.MouseDown += new MouseEventHandler(uc_MouseDown);
            dbc.MouseUp += new MouseEventHandler(uc_MouseUp);
            pnlMain.Controls.Add(dbc);
            pnlMain.Height += dbc.Height;

            m_dbcDisplayItems.Add(dbc);
            
            m_iIndex = m_dbcDisplayItems.Count;

            //RecalculateScrollbars();
        }

        void uc_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                int i = -1;
                for (i = 0; i < m_dbcDisplayItems.Count(); i++)
                {
                    if (m_dbcDisplayItems[i] == (UserControl)sender)
                        break;
                }

                Index = i;

                this.Invalidate(true);
                this.Focus();
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }
        }

        void uc_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //Store mouse position so we can check against it at the end of the method
                Point pntMousePosStart = e.Location;
                int i = -1;
                for (i = 0; i < m_dbcDisplayItems.Count(); i++)
                {
                    if (m_dbcDisplayItems[i] == (UserControl)sender)
                        break;
                }

                udImage_DragDrop idd = new udImage_DragDrop();
                idd.Index = i;
                idd.BitmapData = (Bitmap)m_bmpThumbnails[i].Clone();

                DragDropEffects effects = DoDragDrop(idd, DragDropEffects.Copy);

                //If button is up at the end of the drag/drop effect, assume that we are letting go.  
                Point pntMousPosEnd = ((UserControl)sender).PointToClient(Control.MousePosition);
                if (Control.MouseButtons != MouseButtons.Left && pntMousePosStart == pntMousPosEnd)
                    uc_MouseUp(sender, e);
            }
            catch (Exception ex)
            {
                Global.WriteToLog(ex);
            }
        }

        void TransferFocusToPanel(object sender, EventArgs e)
        {
            this.Focus();
        }

        public void UpdateElement(Bitmap bmpUpdatedThumbnail)
        {
            UpdateElement(bmpUpdatedThumbnail, Index);
        }

        public void UpdateElement(Bitmap bmpUpdatedThumbnail, int IndexToUpdate)
        {
            if (IndexToUpdate > m_bmpThumbnails.Count - 1 || IndexToUpdate < 0)
                return;

            //Create a temp transparent bitmap
            Bitmap bmp = new Bitmap(Constants.iThumbnailSize, Constants.iThumbnailSize);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, bmp.Width, bmp.Height));

            //Get scaled size for thumbnail
            Bitmap bmpResized = new Bitmap(bmpUpdatedThumbnail, Core.BasicOps.MaintainAspectRatio_DontExcedeNewSize(new Size(Constants.iThumbnailSize, Constants.iThumbnailSize), bmpUpdatedThumbnail.Size));
            //Get offset
            Point pntOffset = new Point((Constants.iThumbnailSize - bmpResized.Size.Width) / 2, (Constants.iThumbnailSize - bmpResized.Size.Height) / 2);
            //Draw onto transparent bitmap
            gfx.DrawImage(bmpResized, pntOffset);
            m_bmpThumbnails[IndexToUpdate] = bmp;

            //pnlMain.Invalidate(true);
            pnlMain.Refresh();
        }

        void ElementClicked(object sender, EventArgs e)
        {
            //This method was used until drag/drop was implimented. See MouseDown now

            //int i = -1;
            //for (i = 0; i < m_pnlDisplayItems.Count(); i++)
            //{
            //    if (m_pnlDisplayItems[i] == (UserControl)sender)
            //        break;
            //}

            //Index = i;            

            //this.Invalidate(true);
            //this.Focus();

            //udImage_DragDrop idd = new udImage_DragDrop();
            //idd.Index = i;
            //idd.BitmapData = (Bitmap)m_bmpThumbnails[i].Clone();

            //DragDropEffects effects = DoDragDrop(idd, DragDropEffects.Copy);
        }

        void PaintElement(object sender, PaintEventArgs e)
        {
            if (Global.bDontPaint)
                return;

            //Generate grphics object
            Graphics gfx = e.Graphics;

            int iCurrentElementIndex = 0;
            //Get index
            foreach (UserControl uc in m_dbcDisplayItems)
            {                
                if ((UserControl)sender == uc)
                    break;
                iCurrentElementIndex++;
            }

            //Fill with Color
            if (iCurrentElementIndex == Index)//Is the selected element?
            {
                gfx.FillRectangle(Brushes.SkyBlue, 0, 0, ((UserControl)sender).Width, ((UserControl)sender).Height);
            }
            else //Use regular highlighting
            {
                if (m_bBackgroundColor[iCurrentElementIndex])
                    gfx.FillRectangle(Brushes.Gray, 0, 0, ((UserControl)sender).Width, ((UserControl)sender).Height);
                else
                    gfx.FillRectangle(Brushes.LightGray, 0, 0, ((UserControl)sender).Width, ((UserControl)sender).Height);
            }
            
            //Add a rectangle - Commented out because it looked very Win95ish
            //gfx.DrawRectangle(Pens.Black, 0, 0, ((UserControl)sender).Width, ((UserControl)sender).Height);

            //Draw thumbnail
            try
            {
                gfx.DrawImage(m_bmpThumbnails[iCurrentElementIndex], 5, 1);
            }
            catch(Exception ex)
            {
                Bitmap bmp = new Bitmap(Constants.iThumbnailSize, Constants.iThumbnailSize);
                gfx.DrawImage(bmp, 5, 1);
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
            }

            Font fnt = new Font(new FontFamily("Microsoft Sans Serif"), 8.25f);

            //Draw text
            gfx.DrawString(m_strThumbText[iCurrentElementIndex], fnt, Brushes.Black, Constants.iThumbnailSize + 10, ((UserControl)sender).Height / 2);

            
            //Disable horizontal scrolling
            //this.SetScrollState(2, false);           
            
        }

        public void RemoveElement()
        {
            RemoveElement(Index);
        }

        public void RemoveElement(int IndexToRemove)
        {
            UserControl ucRemoved = m_dbcDisplayItems[IndexToRemove];

            m_dbcDisplayItems.RemoveAt(IndexToRemove);
            m_bmpThumbnails.RemoveAt(IndexToRemove);
            m_bBackgroundColor.RemoveAt(IndexToRemove);
            m_strThumbText.RemoveAt(IndexToRemove);

            pnlMain.Controls.Remove(ucRemoved);

            ucRemoved = null;

            //Fix control positions and repaint
            for (int i = 0; i < m_dbcDisplayItems.Count; i++)
            {
                UserControl uc = m_dbcDisplayItems[i];

                if (i == 0)
                {
                    uc.Location = new Point(0, 0);
                    m_bBackgroundColor[i] = false;
                }
                else
                {
                    uc.Location = new Point(0, m_dbcDisplayItems[i - 1].Location.Y + Constants.iThumbnailSize + 2);
                    m_bBackgroundColor[i] = !m_bBackgroundColor[i - 1];
                }

                RecalculateScrollbars();

                uc.Refresh();
            }

            //Reassign index
            if (Index >= m_dbcDisplayItems.Count)
                Index--;

        }

        public void RemoveAllElements()
        {
            for (int i = 0; i < m_dbcDisplayItems.Count; i++)
            {                
                m_dbcDisplayItems[i] = null;
            }

            m_dbcDisplayItems.Clear();
            m_bmpThumbnails.Clear();
            m_bBackgroundColor.Clear();
            m_strThumbText.Clear();
            pnlMain.Controls.Clear();

            m_iIndex = -1;

            RecalculateScrollbars();
            Refresh();//Invalidate();
        }

        private void uiThumbnailList_Scroll(object sender, ScrollEventArgs e)
        {
            this.Refresh();//this.Invalidate(true);
        }

        private void uiThumbnailList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (Index > 0)
                    Index--;
                this.Refresh();// this.Invalidate(true);
            }
            else if(e.KeyCode == Keys.Down)
            {
                if (Index < m_dbcDisplayItems.Count - 1)
                    Index++;
                this.Refresh(); this.Invalidate(true);
            }
                
        }

        private void vsbMain_Scroll(object sender, ScrollEventArgs e)
        {            
            pnlMain.Location = new Point(pnlMain.Location.X, pnlMain.Location.Y + e.OldValue - e.NewValue);
            
        }

        private void uiThumbnailList_SizeChanged(object sender, EventArgs e)
        {
            RecalculateScrollbars();
                    
        }

        public void RecalculateScrollbars()
        {            
            vsbMain.Minimum = 0;
            vsbMain.Maximum = (m_dbcDisplayItems.Count * SelectableElementSize.Height) - this.Height + (SelectableElementSize.Height / 2);
            if (vsbMain.Minimum < 0)
                vsbMain.Enabled = false;                
            else
            {
                vsbMain.Enabled = true;

                //Experimental fix to see if we can fix the "cant scroll to last item without scroll wheel" bug
                decimal dMaxIsDivisableByControlSize = (decimal)vsbMain.Maximum / (decimal)SelectableElementSize.Height;
                if (dMaxIsDivisableByControlSize % 1 != 0) //Check if we're even or slightly off
                {
                    //Calculate the next highest
                    decimal dNextUp = Math.Round(dMaxIsDivisableByControlSize) + 1;
                    vsbMain.Maximum = (int)dNextUp * SelectableElementSize.Height;
                }

                vsbMain.Value = (m_dbcDisplayItems.Count * SelectableElementSize.Height) - Index - this.Height + (SelectableElementSize.Height / 2);//(pnlMain.Location.Y / SelectableElementSize.Height * 8) * -1;
                pnlMain.Location = new Point(pnlMain.Location.X, -vsbMain.Value);
            }
        }

    }
}
