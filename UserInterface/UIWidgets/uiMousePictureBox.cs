using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Photoman;
using Photoman.Core;

namespace Photoman.UserInterface.UIWidgets
{
    //Mouse mode
    public enum MouseMode
    {
        Resize = 0,
        Crop,
        RedEyeRemoval,
        AddText
    }

    //Enum of handles
    public enum SelectedMouseHandle
    {
        None = 0,
        UpperLeft,
        Up,
        UpperRight,
        Right,
        LowerRight,
        Down,
        LowerLeft,
        Left,
        Unknown
    }

    public delegate void ImageResizedHandler();

    public partial class uiMousePictureBox : UserControl
    {
        //Debug window
        //DebugConsole //DebugOutput;        

        //Image Resized event
        public ImageResizedHandler ImageResized;

        //Mouse Handles
        private DoubleBufferedControl m_dbcUpperLeft;
        private DoubleBufferedControl m_dbcUp;
        private DoubleBufferedControl m_dbcUpperRight;
        private DoubleBufferedControl m_dbcRight;
        private DoubleBufferedControl m_dbcLowerRight;
        private DoubleBufferedControl m_dbcDown;
        private DoubleBufferedControl m_dbcLowerLeft;
        private DoubleBufferedControl m_dbcLeft;

        //The rich text box for inputting text.
        private RichTextBox m_rtbInput;

        //Selection rectangle that contains mouse handles
        private DoubleBufferedControl m_dbcSelection;

        //The mouse handle image
        private Bitmap m_bmpMouseHandleGraphic;

        //Int for ant border offset
        private int m_iAntOffset = 0;

        //Bool for if we are in move control mode
        private bool m_bMoveSelectionMode = false;

        //Bool for if we are in draw selection mode
        private bool m_bDrawSelection = false;

        //Point of original mouse position
        private Point m_pntMouseDown;

        //The original location of the selection rectangle
        private Point m_pntSelection_OriginalLocation;

        //The last moved point. Used as a temporary fix to stop repeated mouse move actions when mouse is idle
        private Point m_pntLastMousePos;

        //The offset of the point the mouse was clicked down and the upper left of the mouse handle
        private Point m_pntMHMouseOffset;

        //A record of the last place the selection box was
        private Point m_pntLastSelectionPosition;
        //A record of the last size the selection box was
        private Size m_sizLastSelectionSize;

        //The ammount of pixels from the border to start automating resize
        private int m_iAutoResizePixelBounds = 5;

        //Points for bounding box boundries when resizing.
        private Point m_pntMHUpperLeft;
        private Point m_pntMHUp;
        private Point m_pntMHUpperRight;
        private Point m_pntMHRight;
        private Point m_pntMHLowerRight;
        private Point m_pntMHDown;
        private Point m_pntMHLowerLeft;
        private Point m_pntMHLeft;

        //The mouse handle that has been selected
        private SelectedMouseHandle m_SelectedHandle = 0;

        //The size of the selection rectangle on mouse down
        private Size m_sizeSelectionRectangle_OnMousehandleDown;
        
        //Rectangle of centered positioning for image
        private Rectangle m_recCenteredImageBounds;
        
        //Font info
        private string m_strAddTextString = "";
        private Brush m_brshFontColor;
        private Font m_fntAddTextFont;

        //The factor to scale by. Currently not used, will be implimented when resize is functional without
        private int m_iScaleFactor = 1;
        public int ScaleFactor
        {
            get { return m_iScaleFactor; }
        }

        //The size of the working image
        private Size m_sizSizeOfBitmap;

        //The working image
        private Bitmap m_bmpWorkingImage;
        public Bitmap WorkingImage
        {
            get
            {
                return m_bmpWorkingImage;
            }
            set
            {
                m_bmpWorkingImage = value;
                if (m_bmpWorkingImage != null)
                {
                    m_sizSizeOfBitmap = m_bmpWorkingImage.Size;
                    tlSize.Text = "Size: " + m_sizSizeOfBitmap.Width + "x" + m_sizSizeOfBitmap.Height;
                    tlAspectRatio.Text = "Aspect Ratio: " + GetStringFractionFromDecimal((decimal)((decimal)m_sizSizeOfBitmap.Width / (decimal)m_sizSizeOfBitmap.Height));//((float)m_sizSizeOfBitmap.Width / (float)m_sizSizeOfBitmap.Height);
                }
                
                m_iScaleFactor = 1;

                AutoZoom();
                CenterImage();

                //Check mouse mode and set accordingly
                switch (m_mmMouseMode)
                {
                    case MouseMode.Resize:
                        m_dbcSelection.Bounds = m_recCenteredImageBounds;
                        break;
                    default:
                        break;
                }                

                this.Invalidate(true);

                //Fire off WorkingImageChanged event here once code is further along
            }
        }

        private MouseMode m_mmMouseMode = 0;
        public MouseMode Mouse_Mode
        {
            get
            {
                return m_mmMouseMode;
            }
            set
            {
                m_mmMouseMode = value;

                //fire off mouse mode changed event here once further along

                //Change appropriate settings to accomodate mode
                switch (value)
                {
                    case MouseMode.Resize://Resize mode
                        if (m_recCenteredImageBounds != null)
                            m_dbcSelection.Bounds = m_recCenteredImageBounds;
                        else
                            m_dbcSelection.Bounds = pnlMain.Bounds;
                        m_rtbInput.Visible = false;
                        break;
                    default://All other modes, as they all operate around crop
                        m_dbcSelection.Size = new Size(1, 1);
                        m_dbcSelection.Location = new Point (-1,-1);
                        if (value == MouseMode.AddText)
                            m_rtbInput.Visible = true;
                        else
                            m_rtbInput.Visible = false;
                        break;
                }                
            }
        }

        public uiMousePictureBox()
        {
            InitializeComponent();
            Setup();
        }


        public Size ImageSize
        {
            get
            {
                return new Size(m_dbcSelection.Width * m_iScaleFactor, m_dbcSelection.Height * m_iScaleFactor);
            }
        }

        public Rectangle CropBounds
        {
            get
            {
                if (m_dbcSelection != null && m_recCenteredImageBounds != null)
                    return new Rectangle((m_dbcSelection.Location.X - m_recCenteredImageBounds.X) * m_iScaleFactor, (m_dbcSelection.Location.Y - m_recCenteredImageBounds.Y) * m_iScaleFactor,
                                     m_dbcSelection.Width * m_iScaleFactor, m_dbcSelection.Height * m_iScaleFactor);
                else
                    return new Rectangle(-1, -1, -1, -1);//Return an invalid crop bounds and handle elsewhere
            }
        }

        public bool AntMarching
        {
            get { return tmrAntMarchTimer.Enabled; }
            set
            {
                if(m_bAllowAntMarching)
                    tmrAntMarchTimer.Enabled = value;
            }
        }

        private bool m_bAllowAntMarching = true;
        public bool AllowAntMarching
        {
            get { return m_bAllowAntMarching; }
            set
            {
                m_bAllowAntMarching = value;
                if (!m_bAllowAntMarching)
                    tmrAntMarchTimer.Enabled = false;
            }
        }

        //Sets up unintialized stuff
        private void Setup()
        {
            try
            {
                //Create mouse handle graphic
                m_bmpMouseHandleGraphic = new Bitmap(8, 8);
                Graphics gfx = Graphics.FromImage(m_bmpMouseHandleGraphic);
                gfx.FillRectangle(Brushes.Black, 0, 0, 8, 8);
                gfx.DrawRectangle(Pens.White, 1, 1, 5, 5);

                //Init selection panel
                m_dbcSelection = new DoubleBufferedControl();

                //Init mouse handle panels
                m_dbcUpperLeft = MouseHandleInit();
                m_dbcUpperLeft.Location = new Point(0, 0);
                m_dbcUpperLeft.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                m_dbcUpperLeft.Cursor = Cursors.SizeNWSE;

                m_dbcUp = MouseHandleInit();
                m_dbcUp.Location = new Point((m_dbcSelection.Width / 2) - (m_dbcUp.Width / 2), 0);
                m_dbcUp.Anchor = AnchorStyles.Top;
                m_dbcUp.Cursor = Cursors.SizeNS;

                m_dbcUpperRight = MouseHandleInit();
                m_dbcUpperRight.Location = new Point(m_dbcSelection.Width - m_dbcUpperRight.Width, 0);
                m_dbcUpperRight.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                m_dbcUpperRight.Cursor = Cursors.SizeNESW;

                m_dbcRight = MouseHandleInit();
                m_dbcRight.Location = new Point(m_dbcSelection.Width - m_dbcUpperRight.Width, (m_dbcSelection.Height / 2) - (m_dbcUp.Height / 2));
                m_dbcRight.Anchor = AnchorStyles.Right;
                m_dbcRight.Cursor = Cursors.SizeWE;

                m_dbcLowerRight = MouseHandleInit();
                m_dbcLowerRight.Location = new Point(m_dbcSelection.Width - m_dbcUpperRight.Width, m_dbcSelection.Height - m_dbcLowerRight.Height);
                m_dbcLowerRight.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                m_dbcLowerRight.Cursor = Cursors.SizeNWSE;

                m_dbcDown = MouseHandleInit();
                m_dbcDown.Location = new Point((m_dbcSelection.Width / 2) - (m_dbcUp.Width / 2), m_dbcSelection.Height - m_dbcLowerRight.Height);
                m_dbcDown.Anchor = AnchorStyles.Bottom;
                m_dbcDown.Cursor = Cursors.SizeNS;

                m_dbcLowerLeft = MouseHandleInit();
                m_dbcLowerLeft.Location = new Point(0, m_dbcSelection.Height - m_dbcLowerRight.Height);
                m_dbcLowerLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                m_dbcLowerLeft.Cursor = Cursors.SizeNESW;

                m_dbcLeft = MouseHandleInit();
                m_dbcLeft.Location = new Point(0, (m_dbcSelection.Height / 2) - (m_dbcUp.Height / 2));
                m_dbcLeft.Anchor = AnchorStyles.Left;
                m_dbcLeft.Cursor = Cursors.SizeWE;

                //Wire up selection panel events
                m_dbcSelection.Paint += new PaintEventHandler(m_dbcSelection_Paint);
                m_dbcSelection.MouseDown += new MouseEventHandler(m_dbcSelection_MouseDown);
                m_dbcSelection.MouseMove += new MouseEventHandler(m_dbcSelection_MouseMove);
                m_dbcSelection.MouseUp += new MouseEventHandler(m_dbcSelection_MouseUp);
                m_dbcSelection.BackColor = Color.FromArgb(0, 0, 0, 0);

                //Wire up events on panel
                pnlMain.Paint += new PaintEventHandler(pnlMain_Paint);

                //Add selection control to main panel
                pnlMain.Controls.Add(m_dbcSelection);

                //Create rich text box for adding text
                m_rtbInput = new RichTextBox();
                m_dbcSelection.Controls.Add(m_rtbInput);
                m_rtbInput.Dock = DockStyle.Fill;
                m_rtbInput.BorderStyle = BorderStyle.None;
                m_rtbInput.Visible = false;
                m_rtbInput.DetectUrls = false;
                

                m_rtbInput.MouseDown += new MouseEventHandler(m_dbcSelection_MouseDown);
                m_rtbInput.MouseMove += new MouseEventHandler(m_dbcSelection_MouseMove);
                m_rtbInput.MouseUp += new MouseEventHandler(m_dbcSelection_MouseUp);
                //Hide out of view
                m_dbcSelection.Size = new Size(1, 1);
                m_dbcSelection.Location = new Point(-1, -1);

                tcMouseMode.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
            }
        }
        

        //Set up mouse handles, add to selection rectangle, wire up events and set image
        private DoubleBufferedControl MouseHandleInit()
        {
            //if(//DebugOutput != null)
                //DebugOutput.WriteToConsole("Mouse Handle Init");

            DoubleBufferedControl MouseHandle = new DoubleBufferedControl();

            try
            {                
                MouseHandle.Size = m_bmpMouseHandleGraphic.Size;
                MouseHandle.BackgroundImage = m_bmpMouseHandleGraphic;
                m_dbcSelection.Controls.Add(MouseHandle);

                //Wire up events here
                MouseHandle.MouseDown += new MouseEventHandler(MouseHandle_MouseDown);
                MouseHandle.MouseUp += new MouseEventHandler(MouseHandle_MouseUp);
                MouseHandle.MouseMove += new MouseEventHandler(MouseHandle_MouseMove);

            }
            catch (Exception ex)
            {
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
            }

            return MouseHandle;
        }

        private void CenterImage()
        {
            if (m_bmpWorkingImage != null)
            {
                Size sizCanvasSize = pnlMain.Size;

                Size sizImageSize = new Size (m_bmpWorkingImage.Width / m_iScaleFactor,m_bmpWorkingImage.Height / m_iScaleFactor);

                
                //For now center without scaling. Will scale once zoom is implimented.
                m_recCenteredImageBounds = new Rectangle((sizCanvasSize.Width / 2) - (sizImageSize.Width / 2), (sizCanvasSize.Height / 2) - (sizImageSize.Height / 2), sizImageSize.Width, sizImageSize.Height);
            }
        }

        public void SetAddTextInfo(string Text, Font font, Brush color)
        {
            m_strAddTextString = Text;
            m_brshFontColor = color;
            m_fntAddTextFont = font;
        }

        void m_dbcSelection_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (m_bMoveSelectionMode)
                {
                    m_bMoveSelectionMode = false;                    

                    m_pntMouseDown = new Point(-1, -1);
                    m_pntSelection_OriginalLocation = new Point(-1, -1);
                }
            }
            catch (Exception ex)
            {
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
            }
        }

        void m_dbcSelection_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (Mouse_Mode != MouseMode.Resize)//For now treat resize as croping modes for test
                {
                    //Check if in move mode
                    if (m_bMoveSelectionMode)
                    {
                        try
                        {
                            m_dbcSelection.Location = new Point(pnlMain.PointToClient(m_pntSelection_OriginalLocation).X + (pnlMain.PointToClient(m_dbcSelection.PointToScreen(e.Location)).X - pnlMain.PointToClient(m_pntMouseDown).X),
                                                        pnlMain.PointToClient(m_pntSelection_OriginalLocation).Y + (pnlMain.PointToClient(m_dbcSelection.PointToScreen(e.Location)).Y - pnlMain.PointToClient(m_pntMouseDown).Y));

                            Point pntSelectionLocation_RelativeToWidget = PointToClient(pnlMain.PointToScreen(m_dbcSelection.Location));

                            this.Invalidate(new Rectangle(pntSelectionLocation_RelativeToWidget.X, pntSelectionLocation_RelativeToWidget.Y, m_dbcSelection.Width, m_dbcSelection.Height));
                            //m_dbcSelection.Invalidate(false);
                            m_dbcSelection.Refresh();
                            this.Update();
                        }
                        catch (Exception ex)
                        {
                            Global.WriteToLog("EXCEPTION - " + ex.Message, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
            }
        }

        void m_dbcSelection_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                m_bMoveSelectionMode = true;

                m_pntMouseDown = m_dbcSelection.PointToScreen(e.Location);
                m_pntSelection_OriginalLocation = PointToScreen(m_dbcSelection.Location);
            }
            catch (Exception ex)
            {
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
            }
        }

        private void pnlMain_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (m_bDrawSelection)
                {
                    m_pntMouseDown = new Point(-1, -1);
                    m_bDrawSelection = false;
                }
            }
            catch (Exception ex)
            {
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
            }
        }     

        private void pnlMain_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //Check if we are in resize or crop mode
                if (Mouse_Mode != MouseMode.Resize)//For now treat resize as croping modes for test
                {
                    //Check if in move mode
                    if (m_bMoveSelectionMode)
                    {
                        try
                        {
                            m_dbcSelection_MouseMove(sender, e);
                        }
                        catch (Exception ex)
                        {
                            Global.WriteToLog("EXCEPTION - " + ex.Message, true);
                        }
                    }
                    else if (m_SelectedHandle != SelectedMouseHandle.None) //Check if resizing selection box
                    {
                        try
                        {
                            MouseHandle_MouseMove(sender, e);
                        }
                        catch (Exception ex)
                        {
                            Global.WriteToLog("EXCEPTION - " + ex.Message, true);
                        }
                    }
                    else if (m_bDrawSelection)
                    {
                        m_dbcSelection.Width = e.X - m_pntMouseDown.X;
                        m_dbcSelection.Height = e.Y - m_pntMouseDown.Y;

                        pnlMain.Invalidate(m_dbcSelection.Bounds);
                        if (m_recCenteredImageBounds != null)
                            pnlMain.Invalidate(m_recCenteredImageBounds);
                        pnlMain.Refresh();
                    }
                }

                tsNewDimensions.Text = "New Size: " + (m_dbcSelection.Width * m_iScaleFactor) + "x" + (m_dbcSelection.Height * m_iScaleFactor);
            }
            catch (Exception ex)
            {
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
            }
        }

        private void pnlMain_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (m_mmMouseMode != MouseMode.Resize)
                {
                    //Change size to 1,1. Do this before the mouse is clicked so the user doesnt see it for a breif second.
                    m_dbcSelection.Size = new Size(1, 1);
                    //store mouse position
                    m_pntMouseDown = e.Location;
                    //Change selection position to mouse position
                    m_dbcSelection.Location = e.Location;
                    //Enter draw selection mode
                    m_bDrawSelection = true;
                }
            }
            catch (Exception ex)
            {
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
            }
        }

        void MouseHandle_MouseUp(object sender, MouseEventArgs e)
        {
            Point pntRelativeMouse = PointToClient(e.Location);
            try
            {
                if (Mouse_Mode != MouseMode.Resize) //Resize for now, but code is applicable to crop
                {
                    m_SelectedHandle = SelectedMouseHandle.None;
                }
                else //Handle resize
                {
                    if (m_SelectedHandle != SelectedMouseHandle.None && ImageResized != null)
                        ImageResized();

                    m_SelectedHandle = SelectedMouseHandle.None;
                }

            }
            catch (Exception ex)
            {
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
            }
        }

        void MouseHandle_MouseMove(object sender, MouseEventArgs e)
        {
            //Used to stop repeated mouse move events when mouse is idle
            if (m_pntLastMousePos != null && m_pntLastMousePos == e.Location)
                return;
            else
                m_pntLastMousePos = e.Location;            

            try
            {
                if (Mouse_Mode != MouseMode.Resize) //Check if in crop mode
                {
                    //Mouse position relative to main panel
                    Point pntRelativeMousePosition = pnlMain.PointToClient(MousePosition);

                    //Check moved handle and adjust other points accordingly
                    switch (m_SelectedHandle)
                    {
                        case SelectedMouseHandle.UpperLeft:

                            m_pntMHUpperLeft = new Point(pntRelativeMousePosition.X - m_pntMHMouseOffset.X, pntRelativeMousePosition.Y - m_pntMHMouseOffset.Y);
                            
                            m_pntMHUp.Y = m_pntMHUpperLeft.Y;
                            m_pntMHUpperRight.Y = m_pntMHUpperLeft.Y;
                            m_pntMHLeft.X = m_pntMHUpperLeft.X;
                            m_pntMHLowerLeft.X = m_pntMHLowerLeft.X;
                            break;
                        case SelectedMouseHandle.Up:
                            m_pntMHUpperLeft.Y = pntRelativeMousePosition.Y - m_pntMHMouseOffset.Y;
                            m_pntMHUp.Y = pntRelativeMousePosition.Y - m_pntMHMouseOffset.Y;
                            m_pntMHUpperRight.Y = pntRelativeMousePosition.Y - m_pntMHMouseOffset.Y;
                            break;
                        case SelectedMouseHandle.UpperRight:
                            m_pntMHUpperRight = new Point(pntRelativeMousePosition.X + m_pntMHMouseOffset.X, pntRelativeMousePosition.Y - m_pntMHMouseOffset.Y);
                            m_pntMHUp.Y = m_pntMHUpperRight.Y;
                            m_pntMHUpperLeft.Y = m_pntMHUpperRight.Y;
                            m_pntMHRight.X = m_pntMHUpperRight.X;
                            m_pntMHLowerRight.X = m_pntMHUpperRight.X;
                            break;
                        case SelectedMouseHandle.Right:
                            m_pntMHUpperRight.X = pntRelativeMousePosition.X + m_pntMHMouseOffset.X;
                            m_pntMHRight.X = m_pntMHUpperRight.X;
                            m_pntMHLowerRight.X = m_pntMHRight.X;
                            break;
                        case SelectedMouseHandle.LowerRight:
                           
                            m_pntMHLowerRight = new Point(pntRelativeMousePosition.X + m_pntMHMouseOffset.X, pntRelativeMousePosition.Y + m_pntMHMouseOffset.Y);
                            m_pntMHDown.Y = m_pntMHLowerRight.Y;
                            m_pntMHLowerLeft.Y = m_pntMHLowerRight.Y;
                            m_pntMHRight.X = m_pntMHLowerRight.X;
                            m_pntMHUpperRight.X = m_pntMHLowerRight.X;
                            break;
                        case SelectedMouseHandle.Down:
                            m_pntMHLowerLeft.Y = pntRelativeMousePosition.Y + m_pntMHMouseOffset.Y;
                            m_pntMHDown.Y = m_pntMHLowerLeft.Y;
                            m_pntMHLowerRight.Y = m_pntMHLowerLeft.Y;
                            break;
                        case SelectedMouseHandle.LowerLeft:
                            m_pntMHLowerLeft = new Point(pntRelativeMousePosition.X - m_pntMHMouseOffset.X, pntRelativeMousePosition.Y + m_pntMHMouseOffset.Y);
                            m_pntMHLeft.X = m_pntMHLowerLeft.X;
                            m_pntMHUpperLeft.X = m_pntMHLowerLeft.X;
                            m_pntMHDown.Y = m_pntMHLowerLeft.Y;
                            m_pntMHLowerRight.Y = m_pntMHLowerRight.Y;
                            break;
                        case SelectedMouseHandle.Left:
                            m_pntMHUpperLeft.X = pntRelativeMousePosition.X - m_pntMHMouseOffset.X;
                            m_pntMHLeft.X = m_pntMHUpperLeft.X;
                            m_pntMHLowerLeft.X = m_pntMHUpperLeft.X;
                            break;
                        default:
                            //Not resizing. Return.
                            return;
                    }

                    //Check for inversion and set bounds accordingly
                    if (m_pntMHUp.Y < m_pntMHDown.Y && m_pntMHLeft.X < m_pntMHRight.X)
                        m_dbcSelection.Bounds = new Rectangle(m_pntMHUpperLeft.X, m_pntMHUpperLeft.Y, m_pntMHUpperRight.X - m_pntMHUpperLeft.X, m_pntMHLowerLeft.Y - m_pntMHUpperLeft.Y);
                    else if (m_pntMHUp.Y < m_pntMHDown.Y && m_pntMHLeft.X > m_pntMHRight.X)
                        m_dbcSelection.Bounds = new Rectangle(m_pntMHUpperRight.X, m_pntMHUpperLeft.Y, m_pntMHUpperLeft.X - m_pntMHUpperRight.X, m_pntMHLowerLeft.Y - m_pntMHUpperLeft.Y);
                    else if (m_pntMHUp.Y > m_pntMHDown.Y && m_pntMHLeft.X < m_pntMHRight.X)
                        m_dbcSelection.Bounds = new Rectangle(m_pntMHUpperLeft.X, m_pntMHLowerLeft.Y, m_pntMHUpperRight.X - m_pntMHUpperLeft.X, m_pntMHUpperLeft.Y - m_pntMHLowerLeft.Y);
                    else
                        m_dbcSelection.Bounds = new Rectangle(m_pntMHUpperRight.X, m_pntMHLowerLeft.Y, m_pntMHUpperLeft.X - m_pntMHUpperRight.X, m_pntMHUpperLeft.Y - m_pntMHLowerLeft.Y);

                    pnlMain.Invalidate(m_dbcSelection.Bounds);
                    if (m_recCenteredImageBounds != null)
                        pnlMain.Invalidate(m_recCenteredImageBounds);
                    //m_dbcSelection.Invalidate(false);
                    pnlMain.Update();
                }
                else
                {
                    //Mouse position relative to main panel
                    Point pntRelativeMousePosition = pnlMain.PointToClient(MousePosition);

                    Point pntOriginalHandleLocation = new Point(-1, -1);
                    
                    //Move handles. Pretty much same as code in crop except slightly different settings.
                    switch (m_SelectedHandle)
                    {
                        case SelectedMouseHandle.UpperLeft:
                            {
                                //The ammount we moved on X
                                int iNewX = (pntRelativeMousePosition.X - m_pntMHMouseOffset.X);
                                int iNewY = (pntRelativeMousePosition.Y - m_pntMHMouseOffset.Y);

                                pntOriginalHandleLocation = m_pntMHUpperLeft;

                                //Set upper left
                                m_pntMHUpperLeft.X = iNewX;
                                m_pntMHUpperLeft.Y = iNewY;

                                //Get difference in handle location
                                int iDifferenceInX = iNewX - pntOriginalHandleLocation.X;
                                int iDifferenceInY = iNewY - pntOriginalHandleLocation.Y;

                                //Set Upper Right
                                m_pntMHUpperRight.X -= iDifferenceInX;
                                m_pntMHUpperRight.Y = m_pntMHUpperLeft.Y;

                                //Set Right
                                m_pntMHRight.X = m_pntMHUpperRight.X;

                                //Set Lower right
                                m_pntMHLowerRight.X = m_pntMHRight.X;
                                m_pntMHLowerRight.Y -= iDifferenceInY;

                                //Set Down
                                m_pntMHDown.Y = m_pntMHLowerRight.Y;

                                //Set Lower Left
                                m_pntMHLowerLeft.X = m_pntMHUpperLeft.X;
                                m_pntMHLowerLeft.Y = m_pntMHDown.Y;
                                //Set Left
                                m_pntMHLeft.X = m_pntMHUpperLeft.X;
                            }
                            break;
                        case SelectedMouseHandle.Up:
                            {
                                //The ammount we moved on Y
                                //int iNewX = (pntRelativeMousePosition.X - m_pntMHMouseOffset.X);
                                int iNewY = (pntRelativeMousePosition.Y - m_pntMHMouseOffset.Y);

                                //Store the handle's original position
                                pntOriginalHandleLocation = m_pntMHUpperLeft;

                                //Set upper left
                                m_pntMHUpperLeft.Y = iNewY;

                                //Get difference in handle location
                                int iDifferenceInY = iNewY - pntOriginalHandleLocation.Y;

                                //Set Upper Right
                                m_pntMHUpperRight.Y = m_pntMHUpperLeft.Y;

                                //Set Lower right
                                m_pntMHLowerRight.Y -= iDifferenceInY;

                                //Set Down
                                m_pntMHDown.Y = m_pntMHLowerRight.Y;

                                //Set Lower Left
                                m_pntMHLowerLeft.Y = m_pntMHDown.Y;
                            }
                            break;
                        case SelectedMouseHandle.UpperRight:
                            {
                                //The ammount we moved on X
                                int iNewX = (pntRelativeMousePosition.X - m_pntMHMouseOffset.X);
                                int iNewY = (pntRelativeMousePosition.Y - m_pntMHMouseOffset.Y);

                                //The original handle location
                                pntOriginalHandleLocation = m_pntMHUpperRight;

                                //Set upper Right
                                m_pntMHUpperRight.X = iNewX;
                                m_pntMHUpperRight.Y = iNewY;

                                //Get difference in handle location
                                int iDifferenceInX = iNewX - pntOriginalHandleLocation.X;
                                int iDifferenceInY = iNewY - pntOriginalHandleLocation.Y;

                                //Set Upper Left
                                m_pntMHUpperLeft.X -= iDifferenceInX;
                                m_pntMHUpperLeft.Y = m_pntMHUpperRight.Y;

                                //Set Right
                                m_pntMHRight.X = m_pntMHUpperRight.X;

                                //Set Lower right
                                m_pntMHLowerRight.X = m_pntMHRight.X;
                                m_pntMHLowerRight.Y -= iDifferenceInY;

                                //Set Down
                                m_pntMHDown.Y = m_pntMHLowerRight.Y;

                                //Set Lower Left
                                m_pntMHLowerLeft.X = m_pntMHUpperLeft.X;
                                m_pntMHLowerLeft.Y = m_pntMHDown.Y;
                                //Set Left
                                m_pntMHLeft.X = m_pntMHUpperLeft.X;
                            }
                            break;
                        case SelectedMouseHandle.Right:
                            {
                                //The ammount we moved on X
                                int iNewX = (pntRelativeMousePosition.X - m_pntMHMouseOffset.X);
                                int iNewY = (pntRelativeMousePosition.Y - m_pntMHMouseOffset.Y);

                                //The original handle location
                                pntOriginalHandleLocation = m_pntMHRight;

                                //Set upper Right
                                m_pntMHUpperRight.X = iNewX;

                                //Get difference in handle location
                                int iDifferenceInX = iNewX - pntOriginalHandleLocation.X;

                                //Set Upper Left
                                m_pntMHUpperLeft.X -= iDifferenceInX;

                                //Set Right
                                m_pntMHRight.X = m_pntMHUpperRight.X;

                                //Set Lower right
                                m_pntMHLowerRight.X = m_pntMHRight.X;

                                //Set Lower Left
                                m_pntMHLowerLeft.X = m_pntMHUpperLeft.X;

                                //Set Left
                                m_pntMHLeft.X = m_pntMHUpperLeft.X;
                            }
                            break;
                        case SelectedMouseHandle.LowerRight:
                            {
                                //The ammount we moved on X
                                int iNewX = (pntRelativeMousePosition.X - m_pntMHMouseOffset.X);
                                int iNewY = (pntRelativeMousePosition.Y - m_pntMHMouseOffset.Y);

                                //The original handle location
                                pntOriginalHandleLocation = m_pntMHLowerRight;

                                //Set Lower Right
                                m_pntMHLowerRight.X = iNewX;
                                m_pntMHLowerRight.Y = iNewY;

                                //Get difference in handle location
                                int iDifferenceInX = iNewX - pntOriginalHandleLocation.X;
                                int iDifferenceInY = iNewY - pntOriginalHandleLocation.Y;

                                //Set Upper Left
                                m_pntMHUpperLeft.X -= iDifferenceInX;
                                m_pntMHUpperLeft.Y -= iDifferenceInY;

                                //Set Right
                                m_pntMHRight.X = m_pntMHLowerRight.X;

                                //Set Upper Right
                                m_pntMHUpperRight.X = m_pntMHLowerRight.X;
                                m_pntMHUpperRight.Y = m_pntMHUpperLeft.Y;

                                //Set Down
                                m_pntMHDown.Y = m_pntMHLowerRight.Y;

                                //Set Lower Left
                                m_pntMHLowerLeft.X = m_pntMHUpperLeft.X;
                                m_pntMHLowerLeft.Y = m_pntMHDown.Y;

                                //Set Left
                                m_pntMHLeft.X = m_pntMHUpperLeft.X;
                            }
                            break;
                        case SelectedMouseHandle.Down:
                            {
                                //The ammount we moved on Y
                                int iNewY = (pntRelativeMousePosition.Y - m_pntMHMouseOffset.Y);

                                //The original handle location
                                pntOriginalHandleLocation = m_pntMHDown;

                                //Set upper Right
                                m_pntMHDown.Y = iNewY;

                                //Get difference in handle location
                                int iDifferenceInY = iNewY - pntOriginalHandleLocation.Y;

                                //Set Upper Left
                                m_pntMHUpperLeft.Y -= iDifferenceInY;

                                //Set Lower right
                                m_pntMHLowerRight.Y = m_pntMHDown.Y;

                                //Set Upper right
                                m_pntMHUpperRight.Y = m_pntMHUpperLeft.Y;

                                //Set Lower Left
                                m_pntMHLowerLeft.Y = m_pntMHDown.Y;
                            }
                            break;
                        case SelectedMouseHandle.LowerLeft:
                            {
                                //The ammount we moved on X
                                int iNewX = (pntRelativeMousePosition.X - m_pntMHMouseOffset.X);
                                int iNewY = (pntRelativeMousePosition.Y - m_pntMHMouseOffset.Y);

                                //The original handle location
                                pntOriginalHandleLocation = m_pntMHLowerLeft;

                                //Set Lower left
                                m_pntMHLowerLeft.X = iNewX;
                                m_pntMHLowerLeft.Y = iNewY;

                                //Get difference in handle location
                                int iDifferenceInX = iNewX - pntOriginalHandleLocation.X;
                                int iDifferenceInY = iNewY - pntOriginalHandleLocation.Y;

                                //Set Upper Left
                                m_pntMHUpperLeft.X = m_pntMHLowerLeft.X;
                                m_pntMHUpperLeft.Y -= iDifferenceInY;

                                //Set Right
                                m_pntMHRight.X -= iDifferenceInX;

                                //Set Lower right
                                m_pntMHLowerRight.X = m_pntMHRight.X;
                                m_pntMHLowerRight.Y = m_pntMHLowerLeft.Y;

                                //Set Down
                                m_pntMHDown.Y = m_pntMHLowerRight.Y;

                                //Set Upper Right
                                m_pntMHUpperRight.X = m_pntMHLowerRight.X;
                                m_pntMHUpperRight.Y = m_pntMHUpperLeft.Y;

                                //m_pntMHLowerLeft.X = m_pntMHUpperLeft.X;
                                //m_pntMHLowerLeft.Y = m_pntMHDown.Y;
                                
                                //Set Left
                                m_pntMHLeft.X = m_pntMHUpperLeft.X;
                            }
                            break;
                        case SelectedMouseHandle.Left:
                            {
                                //The ammount we moved on X
                                int iNewX = (pntRelativeMousePosition.X - m_pntMHMouseOffset.X);

                                //The original handle location
                                pntOriginalHandleLocation = m_pntMHLeft;

                                //Set left
                                m_pntMHLeft.X = iNewX;

                                //Get difference in handle location
                                int iDifferenceInX = iNewX - pntOriginalHandleLocation.X;

                                //Set Upper Left
                                m_pntMHUpperLeft.X = m_pntMHLeft.X;

                                //Set upper right
                                m_pntMHUpperRight.X -= iDifferenceInX;

                                //Set Right
                                m_pntMHRight.X = m_pntMHUpperRight.X;

                                //Set Lower right
                                m_pntMHLowerRight.X = m_pntMHRight.X;

                                //Set Down
                                m_pntMHDown.Y = m_pntMHLowerRight.Y;

                                //Set Lower Left
                                m_pntMHLowerLeft.X = m_pntMHUpperLeft.X;

                                //m_pntMHUpperLeft.X = pntRelativeMousePosition.X - m_pntMHMouseOffset.X;
                                //m_pntMHLeft.X = m_pntMHUpperLeft.X;
                                //m_pntMHLowerLeft.X = m_pntMHUpperLeft.X;
                            }
                            break;
                        default:
                            //Not resizing. Return.
                            return;


                    }
                    
                    m_dbcSelection.Bounds = new Rectangle(m_pntMHUpperLeft.X, m_pntMHUpperLeft.Y, m_pntMHUpperRight.X - m_pntMHUpperLeft.X, m_pntMHLowerLeft.Y - m_pntMHUpperLeft.Y);

                    //Check if we're in bounds to zoom out and do.
                    if (pntRelativeMousePosition.X <= m_iAutoResizePixelBounds || pntRelativeMousePosition.Y <= m_iAutoResizePixelBounds ||
                       pntRelativeMousePosition.X > pnlMain.Width - m_iAutoResizePixelBounds || pntRelativeMousePosition.Y > pnlMain.Height - m_iAutoResizePixelBounds)
                    {
                        tmrAutoZoomTimer.Enabled = true;
                    }
                    else
                        tmrAutoZoomTimer.Enabled = false;

                    tsNewDimensions.Text = "New Size: " + (m_dbcSelection.Width * m_iScaleFactor) + "x" + (m_dbcSelection.Height * m_iScaleFactor);

                    //pnlMain.Invalidate();
                    pnlMain.Invalidate(m_dbcSelection.Bounds);
                    if (m_recCenteredImageBounds != null)
                        pnlMain.Invalidate(m_recCenteredImageBounds);
                    //m_dbcSelection.Invalidate(false);
                    
                    //Following line ensures that refreshes dont look bad, but also causes jerkiness
                    pnlMain.Update();
                }
            }
            catch (Exception ex)
            {
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
            }


            //DebugOutput.WriteToConsole("Exit Mouse Handle Mouse Move");
        }

        void MouseHandle_MouseDown(object sender, MouseEventArgs e)
        {
            //DebugOutput.WriteToConsole("Mouse Handle Mouse Down");

            try
            {
                //Store mouse cursor and rectangle size
                m_pntMouseDown = PointToClient(e.Location);
                m_sizeSelectionRectangle_OnMousehandleDown = m_dbcSelection.Size;

                //Check sender for handle and set accordingly
                if (sender == m_dbcUpperLeft)
                    m_SelectedHandle = SelectedMouseHandle.UpperLeft;
                else if (sender == m_dbcUp)
                    m_SelectedHandle = SelectedMouseHandle.Up;
                else if (sender == m_dbcUpperRight)
                    m_SelectedHandle = SelectedMouseHandle.UpperRight;
                else if (sender == m_dbcRight)
                    m_SelectedHandle = SelectedMouseHandle.Right;
                else if (sender == m_dbcLowerRight)
                    m_SelectedHandle = SelectedMouseHandle.LowerRight;
                else if (sender == m_dbcDown)
                    m_SelectedHandle = SelectedMouseHandle.Down;
                else if (sender == m_dbcLowerLeft)
                    m_SelectedHandle = SelectedMouseHandle.LowerLeft;
                else if (sender == m_dbcLeft)
                    m_SelectedHandle = SelectedMouseHandle.Left;
                else
                    m_SelectedHandle = SelectedMouseHandle.Unknown;

                
                //Set upper left, up and left points. Commented out code for some reason did not generate proper width/height in mouse move
                m_pntMHUpperLeft = pnlMain.PointToClient(m_dbcUpperLeft.PointToScreen(m_dbcUpperLeft.Location));                
                m_pntMHUp = pnlMain.PointToClient(m_dbcUp.PointToScreen(m_dbcUp.Location));
                //m_pntMHUpperRight = pnlMain.PointToClient(m_dbcUpperRight.PointToScreen(m_dbcUpperRight.Location));
                //m_pntMHRight = pnlMain.PointToClient(m_dbcRight.PointToScreen(m_dbcRight.Location));
                //m_pntMHLowerRight = pnlMain.PointToClient(m_dbcLowerRight.PointToScreen(m_dbcLowerRight.Location));
                //m_pntMHDown = pnlMain.PointToClient(m_dbcDown.PointToScreen(m_dbcDown.Location));
                //m_pntMHLowerLeft = pnlMain.PointToClient(m_dbcLowerLeft.PointToScreen(m_dbcLowerLeft.Location));
                m_pntMHLeft = pnlMain.PointToClient(m_dbcLeft.PointToScreen(m_dbcLeft.Location));

                //Set other points based on Upper left
                m_pntMHUpperRight = new Point(m_pntMHUpperLeft.X + m_dbcSelection.Width, m_pntMHUpperLeft.Y);
                m_pntMHLowerLeft = new Point(m_pntMHUpperLeft.X, m_pntMHUpperLeft.Y + m_dbcSelection.Height);
                m_pntMHLowerRight = new Point(m_pntMHUpperRight.X, m_pntMHLowerLeft.Y);
                
                //Set Right and Down. Only set X and Y axies respectivly because Y and X are not needed
                m_pntMHRight = new Point(m_pntMHUpperRight.X, 0);
                m_pntMHDown = new Point(0, m_pntMHLowerLeft.Y);
                
                //Set the mouse offset from the handle's upper left.
                m_pntMHMouseOffset = e.Location;
            }
            catch (Exception ex)
            {
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
            }

            //DebugOutput.WriteToConsole("Exit Mouse Handle Mouse Down");
        }

        void pnlMain_Paint(object sender, PaintEventArgs e)
        {
            //Commented out all debug loging in this method because extensive writing to console results in slugish program

            ////DebugOutput.WriteToConsole("Main Panel Paint");

            try
            {
                if (Global.bDontPaint)
                    return;

                if (Global.bDontPaint)
                    return;

                //Set variables for next paint event.
                m_pntLastSelectionPosition = m_dbcSelection.Location;
                m_sizLastSelectionSize = m_dbcSelection.Size;

                Graphics gfx = e.Graphics;

                //Check if image is loaded
                if (m_bmpWorkingImage != null)
                {
                    ////DebugOutput.WriteToConsole("Working image is not null");

                    //Calculate image size after scaling
                    Size ScaledSize = new Size(m_bmpWorkingImage.Width / m_iScaleFactor, m_bmpWorkingImage.Height / m_iScaleFactor);

                    //Render scaled image - should be centered but for now its not
                    gfx.DrawImage(m_bmpWorkingImage, m_recCenteredImageBounds);
                }
            }
            catch (Exception ex)
            {
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
            }

            ////DebugOutput.WriteToConsole("Exting Main Panel Paint");
        }

        void m_dbcSelection_Paint(object sender, PaintEventArgs e)
        {
            ////DebugOutput.WriteToConsole("Selection panel Paint");            

            try
            {
                //Init graphics object
                Graphics gfx = e.Graphics;

                //Check if we're in a selection mode and render boarders 
                if (Mouse_Mode != MouseMode.Resize)
                {
                    ////DebugOutput.WriteToConsole("Mouse mode is resize. Drawing blue shading and ants");

                    //The following code is based off of 
                    //http://www.codeproject.com/KB/GDI-plus/marchingants.aspx

                    //Only do this if ant marching is enabled. If its not, we assume we're in power conservation mode.
                    if (tmrAntMarchTimer.Enabled)
                    {
                        //Create a region for the entire image and color blue
                        Region rgnImage = new Region(e.ClipRectangle);
                        gfx.FillRegion(new SolidBrush(Color.FromArgb(128, 0, 255, 255)), rgnImage);
                    }


                    //Draw text
                    if ((Mouse_Mode == MouseMode.AddText) && (m_strAddTextString != "" && m_fntAddTextFont != null && m_brshFontColor != null))
                        gfx.DrawString(m_strAddTextString, m_fntAddTextFont, m_brshFontColor, 0, 0);
                }
                else if(MouseButtons == MouseButtons.Left)//Check for resize mode and draw transparent overlay, but only if mouse is down so we're not slowing down rendering unnessisarily
                {
                    ColorMatrix matTransparency = new ColorMatrix();
                    matTransparency.Matrix33 = 0.75f;

                    ImageAttributes iaTransparency = new ImageAttributes();
                    iaTransparency.SetColorMatrix(matTransparency, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    gfx.DrawImage(m_bmpWorkingImage, new Rectangle(0, 0, m_dbcSelection.Width, m_dbcSelection.Height),
                                  0, 0, m_bmpWorkingImage.Width, m_bmpWorkingImage.Height, GraphicsUnit.Pixel, iaTransparency);
                }

                //Draw ants
                //Create a pen with dashed white lines
                //Pen penWhite = new Pen(Color.White, 1);
                //penWhite.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                //penWhite.DashPattern = new float[2] { 3, 3 };

                Rectangle recWhiteBorder = e.ClipRectangle;
                recWhiteBorder.Width -= 1;
                recWhiteBorder.Height -= 1;

                gfx.DrawRectangle(Pens.White, recWhiteBorder);

                Pen penBlack = new Pen(Color.Black, 1);
                penBlack.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                penBlack.DashPattern = new float[2] { 6, 6 }; //original 3,3
                penBlack.DashOffset = m_iAntOffset;

                //gfx.DrawRectangle(penWhite, new Rectangle(0, 0, m_dbcSelection.Width - 1, m_dbcSelection.Height - 1));
                gfx.DrawRectangle(penBlack, new Rectangle(0, 0, m_dbcSelection.Width - 1, m_dbcSelection.Height - 1));

                //Check battery status and enable/disable ants as nessisary
                if (Global.IsOnBattery() && Global.bDisableAntsOnBattery)
                    tmrAntMarchTimer.Enabled = false;
                else
                    tmrAntMarchTimer.Enabled = true;

            }
            catch (Exception ex)
            {
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
            }

            ////DebugOutput.WriteToConsole("Exiting Selection Panel Paint");
        }
        
        private void tmrAntMarchTimer_Tick(object sender, EventArgs e)
        {
            ////DebugOutput.WriteToConsole("Ant March Timer");

            try
            {
                if (!m_bAllowAntMarching)
                {
                    tmrAntMarchTimer.Enabled = false;
                    return;
                }

                if (m_iAntOffset != Int32.MaxValue)
                    m_iAntOffset += 4; //original ++
                else
                    m_iAntOffset = 0;

                //Commented code disables ants on mouse move
                //if (!m_bMoveSelectionMode)
                //    m_dbcSelection.Invalidate(false);

                m_dbcSelection.Invalidate(false);
            }
            catch (Exception ex)
            {
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
            }

            ////DebugOutput.WriteToConsole("Exit ant march timer");
        }


        private void tmrAutoZoomTimer_Tick(object sender, EventArgs e)
        {
            if (MouseButtons != MouseButtons.Left)
            {
                tmrAutoZoomTimer.Enabled = false;
                return;
            }

            ZoomOut();
        }

        public void DEBUG_ShowDebugOutput()
        {
            //DebugOutput.Show();
        }

        public void DEBUG_HideDebugOutput()
        {
            //DebugOutput.Hide();
        }

        private void tcMouseMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcMouseMode.Text == "Resize")
                Mouse_Mode = MouseMode.Resize;
            else if (tcMouseMode.Text == "Crop")
                Mouse_Mode = MouseMode.Crop;
            else if (tcMouseMode.Text == "Red Eye Removal")
                Mouse_Mode = MouseMode.RedEyeRemoval;
            else if (tcMouseMode.Text == "Add Text")
                Mouse_Mode = MouseMode.AddText;
        }

        private void uiMousePictureBox_SizeChanged(object sender, EventArgs e)
        {
            if (Mouse_Mode == MouseMode.Resize)
            {
                m_iScaleFactor = 1;
                AutoZoom();
            }

            CenterImage();
            if (m_mmMouseMode == MouseMode.Resize)
                m_dbcSelection.Bounds = m_recCenteredImageBounds;
            else
                HideSelectionRectangle();

            //pnlMain.Invalidate();
            pnlMain.Refresh();
        }

        private void tbZoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut();

            m_dbcSelection.Bounds = m_recCenteredImageBounds;
            this.Refresh();
        }

        private void tbZoomIn_Click(object sender, EventArgs e)
        {
            ZoomIn();

            m_dbcSelection.Bounds = m_recCenteredImageBounds;
            this.Refresh();
        }

        private bool CanZoomIn()
        {
            bool bReturn = false;

            if (m_iScaleFactor - 1 == 0)
            {
                tbZoomIn.Enabled = false;
                return false;
            }

            //Commented out and added the return true because disabling zoom works flakily when changing zoom level

            Size ZoomedSize = new Size(m_bmpWorkingImage.Width / (m_iScaleFactor - 1), m_bmpWorkingImage.Height / (m_iScaleFactor - 1));
            if (ZoomedSize.Width < pnlMain.Width && ZoomedSize.Height < pnlMain.Height)
                bReturn = true;
            else
                bReturn = false;

            //bReturn = true;

            tbZoomIn.Enabled = bReturn;            

            return bReturn;
        }

        private void ZoomIn()
        {
            if (CanZoomIn() && (m_iScaleFactor > 1))
                m_iScaleFactor--;

            tlZoomLevel.Text = "/" + m_iScaleFactor.ToString();

            //Check if we can zoom in again to disable the button if needed
            CanZoomIn();

            CenterImage();
        }

        private void ZoomOut()
        {
            m_iScaleFactor++;

            tlZoomLevel.Text = "/" + m_iScaleFactor.ToString();

            CenterImage();

            //Call CanZoomIn just to update button
            CanZoomIn();
        }

        private void AutoZoom()
        {
            if (m_bmpWorkingImage == null)
                return;

            //Use existing scaling code to check if we can scale
            Size ZoomedSize = BasicOps.MaintainAspectRatio_DontExcedeNewSize(new Size(pnlMain.Width - m_iAutoResizePixelBounds - 1, pnlMain.Height - m_iAutoResizePixelBounds - 1), m_bmpWorkingImage.Size);

            //If the image was zoomed in, abort
            if (ZoomedSize.Height > m_bmpWorkingImage.Height)
                return;

            //Calculate new scale factor from ZoomedSize
            int iTempScaleFactor = (int)Math.Ceiling(((decimal)m_bmpWorkingImage.Height / (decimal)ZoomedSize.Height));

            if (iTempScaleFactor >= 1)
                m_iScaleFactor = iTempScaleFactor;
            else
                iTempScaleFactor = 1;

            tlZoomLevel.Text = "/" + m_iScaleFactor.ToString();

            //Update button if needed
            CanZoomIn();
        }

        //Works but needs to be able to round off insanely large numbers to two didgets per side. Ex. Show 16/10 instead of LargeNumberHere/OtherLargeNumberHere
        private string GetStringFractionFromDecimal(decimal Input)
        {
            try
            {
                int iDenominator = 1;
                int iNumerator = 1;

                //Cut our decimal in half
                string[] strDecimalSides = Input.ToString().Split('.');

                //Incriment denominator
                for (int i = 0; i < strDecimalSides[1].Length; i++)
                    iDenominator *= 10;

                //Set Numerator based on denominator
                iNumerator = (int)(Input * (decimal)iDenominator);

                //Loop through and start reducing if possible. Dont exit until we cant reduce any longer
                //This is not foolproof, immediatly exits with prime numbers but should be adequet for most cases.
                while (true)
                {
                    if (iDenominator % 2 == 0 && iNumerator % 2 == 0)
                    {
                        iDenominator /= 2;
                        iNumerator /= 2;
                    }
                    else if (iDenominator % 3 == 0 && iNumerator % 3 == 0)
                    {
                        iDenominator /= 3;
                        iNumerator /= 3;
                    }
                    else if (iDenominator % 5 == 0 && iNumerator % 5 == 0)
                    {
                        iDenominator /= 5;
                        iNumerator /= 5;
                    }
                    else if (iDenominator % 7 == 0 && iNumerator % 7 == 0)
                    {
                        iDenominator /= 7;
                        iNumerator /= 7;
                    }
                    else
                        break;
                }

                //Return a string representation of our fraction
                return iNumerator.ToString() + "/" + iDenominator.ToString();
            }
            catch (Exception ex)
            {
                Global.WriteToLog("EXCEPTION - " + ex.Message, true);
                return "Unable to determine aspect ratio";
            }
        }

        public void HideSelectionRectangle()
        {
            m_dbcSelection.Size = new Size(1, 1);
            m_dbcSelection.Location = new Point(-1, -1);
        }
    }
}