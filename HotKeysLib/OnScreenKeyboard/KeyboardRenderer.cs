using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Collections;

namespace HotKeysLib.OnScreenKeyboard
{
	/// <summary>
	/// Renders on screen keyboard
	/// </summary>
	public class KeyboardRenderer
	{
		public KeyboardRenderer()
		{
		}

		public void CleanUp()
		{
			if(this.bufferBitmap!=null)
			{
				this.bufferBitmap.Dispose();
				this.bufferBitmap = null;
			}
			if(this.bufferBitmapWithIcons!=null)
			{
				this.bufferBitmapWithIcons.Dispose();
				this.bufferBitmapWithIcons = null;
			}
			if(this.bufferBitmapWithIconsSpare!=null)
			{
				this.bufferBitmapWithIconsSpare.Dispose();
				this.bufferBitmapWithIconsSpare = null;
			}
		}

		public PointF GetIconPositionForKey(KeyboardLayoutKey key)
		{
			// Determines what the world location of a icon is for a given key
			foreach(ItemRect item in this.iconRectangles)
			{
				if(item.Item == key) return item.ActualRectangle.Location;
			}
			// Calaculate the rectangle
			// TODO: ugly icon positioning
			ItemRect keyItem = this.getItemRectForKey(key);
			// TODO: Icon issue
			return new PointF(keyItem.ActualRectangle.Right - this.ActualIconSize.Width, keyItem.ActualRectangle.Bottom - this.ActualIconSize.Height);
		}

		public void RestoreRect(Graphics g, Rectangle rect)
		{
			// Restore rectangle from backbuffer with icons
			g.DrawImage(bufferBitmapWithIcons,rect,rect,GraphicsUnit.Pixel);
		}

		public void RestoreKey(Graphics g, KeyboardLayoutKey key, bool restoreIcon)
		{
			// Restore key from backbuffer with icons
			ItemRect itemRect = getItemRectForKey(key);
			if(itemRect!=null)
				this.RestoreRect(g,Rectangle.Truncate( RectangleF.Inflate(itemRect.ActualRectangle, 2, 2) ));
		}

		public void Invalidate()
		{
			// Calling Invalidate deletes all cached data, on RenderAll everything is completly redrawn
			bufferBitmap = null;
			bufferBitmapWithIcons = null;
			bufferBitmapWithIconsSpare = null;
		}

		public KeyboardLayoutKey KeyFromPoint(PointF point)
		{
			// Get key for screen coordinates
			if(keyRectangles!=null)
			{
				foreach(ItemRect itemRect in keyRectangles)
				{
					if(itemRect.ActualRectangle.Contains(point))
						return (KeyboardLayoutKey)itemRect.Item;
				}
			}
			return null;
		}

		public KeyboardLayoutKey IconKeyFromPoint(PointF point)
		{
			// Get key for a point within an icon, if point is not within an icon null is returned
			foreach(ItemRect itemRect in iconRectangles)
			{
				if(itemRect.ActualRectangle.Contains(point))
					return (KeyboardLayoutKey)itemRect.Item;
			}
			return null;
		}

		public RectangleF IconRectFromPoint(PointF point)
		{
			// Gets a rect for a clicked icon if any, else a 0 rect is returned
			// Currently this function is only used to calcualte icon-pointer offset when drag operation starts
			foreach(ItemRect itemRect in iconRectangles)
			{
				if(itemRect.ActualRectangle.Contains(point))
					return itemRect.ActualRectangle;
			}
			return new RectangleF(0,0,0,0);
		}

		public void HighlightKey(KeyboardLayoutKey key, Color edgeColor, bool withIcon)
		{
			// Draws highlight on backbuffer
			ItemRect itemRect = this.getItemRectForKey(key);
			PointF position = new PointF(itemRect.WorldRectangle.X,itemRect.WorldRectangle.Y);
			// Regardless of what restore the original background
			Graphics bufferGraphics = Graphics.FromImage(this.bufferBitmapWithIcons);
			Rectangle rect = Rectangle.Truncate( RectangleF.Inflate( itemRect.ActualRectangle ,1f,1f) );
			if(withIcon)
				bufferGraphics.DrawImage(bufferBitmapWithIconsSpare,rect,rect,GraphicsUnit.Pixel);
			else
				bufferGraphics.DrawImage(bufferBitmap,rect,rect,GraphicsUnit.Pixel);
			// Draw the actual highlight rect
			initGraphics(bufferGraphics);
			Pen newpen = new Pen(new SolidBrush(edgeColor), outerPen.Width - 1f);
			RectangleF outerRect = RectangleF.Inflate( itemRect.WorldRectangle , (-outerPen.Width), (-outerPen.Width));
			this.drawRoundRect(bufferGraphics, newpen,null,outerRect.X,outerRect.Y,outerRect.Width,outerRect.Height,7.5F);
		}

		public void UnHighlightKey(KeyboardLayoutKey key, bool withIcon)
		{
			ItemRect itemRect = this.getItemRectForKey(key);
			if(itemRect!=null)
			{
				PointF position = new PointF(itemRect.WorldRectangle.X,itemRect.WorldRectangle.Y);
				Graphics bufferGraphics = Graphics.FromImage(this.bufferBitmapWithIcons);
				Rectangle rect = Rectangle.Truncate( RectangleF.Inflate( itemRect.ActualRectangle ,1f,1f) );
				if(withIcon)
					bufferGraphics.DrawImage(bufferBitmapWithIconsSpare,rect,rect,GraphicsUnit.Pixel);
				else
					bufferGraphics.DrawImage(bufferBitmap,rect,rect,GraphicsUnit.Pixel);
			}
		}

		public KeyboardLayoutRow RowFromPoint(PointF point /*, KeyboardLayout layout, KeyboardStyle style, Rectangle clientRectangle*/)
		{
			foreach(ItemRect itemRect in rowRectangles)
			{
				if(itemRect.ActualRectangle.Contains(point))
					return (KeyboardLayoutRow)itemRect.Item;
			}
			return null;
		}

		private Size actualSize = new Size(0,0);
		public Size ActualSize
		{
			get{return this.actualSize;}
			set{this.actualSize=value;}
		}

		private Size actualKeySize = new Size(0,0);
		public Size ActualKeySize
		{
			get{return this.actualKeySize;}
			set{this.actualKeySize=value;}
		}

		private Size actualIconSize = new Size(32,32);
		public Size ActualIconSize
		{
			get{return this.actualIconSize;}
			set{this.actualIconSize=value;}
		}

		private Size availableIconSize = new Size(0,0);
		public Size AvailableIconSize
		{
			get{return this.availableIconSize;}
			set{this.availableIconSize=value;}
		}

		private Bitmap bufferBitmap = null;
		private ArrayList rowRectangles = null;
		private ArrayList keyRectangles = null;
		private ArrayList iconRectangles = null;
		private SizeF keyboardSize;
		private Rectangle usableClientRectangle;
		private float edgeResultantOffset = 0;
		
		// Style
		private Pen outerPen = new Pen(Brushes.Black,4);
		private Color darkColor = Color.Black;
		private Color lightColor = Color.Gray;
		private Color fontColor = Color.White;
		private Color backColor = Color.FromArgb(15,15,15);

		private void renderKeyBoardBuffer()
		{
			PointF keyLocation = new PointF(0,0);
			// Create back buffer stuff
			bufferBitmap = new Bitmap(actualRect.Width, actualRect.Height,System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
			Graphics bufferGraphics = Graphics.FromImage(bufferBitmap);
			bufferGraphics.Clear(Color.Transparent);
			// Render external edge
			initializeCoordinates(bufferGraphics, keyboardSize, usableClientRectangle);
			this.drawRoundRect(bufferGraphics, new Pen(new SolidBrush(outerPen.Color),10),new SolidBrush(backColor),5,5, (keyboardSize.Width * 100) - 10, (keyboardSize.Height * 100) - 15, 15);
			// Initialize back buffer graphics
			//initGraphics(bufferGraphics);
			bufferGraphics.CompositingQuality = CompositingQuality.HighQuality;
			bufferGraphics.InterpolationMode = InterpolationMode.High;
			bufferGraphics.SmoothingMode = SmoothingMode.HighQuality;
			// Render Keys
			foreach(KeyboardLayoutRow row in layout.Rows)
			{
				if(!row.IsPlaceHolder)
				{
					foreach(KeyboardLayoutKey key in row.Keys)
					{
					    if (!key.IsPlaceHolder)
					    {
                            renderKey(bufferGraphics, key, new PointF((keyLocation.X + edgeResultantOffset) * 100, (keyLocation.Y + edgeResultantOffset) * 100), this.outerPen.Color);
                            renderKeyToFile(key, this.outerPen.Color, "c:\\temp2\\" + key.Key + ".png");

					    }
							
						keyLocation.X += (Single)key.Width;
					}
				}
				keyLocation.Y += (Single)row.Height;
				keyLocation.X = 0;
			}
			bufferGraphics.Dispose();
		}

		private void renderKeyBoardBufferWithIcons(Graphics g, IntPtr hWnd)
		{
			g.DrawImageUnscaled(bufferBitmap,0,0);
			this.calculateKeyAndRowRectangles();
			this.calculateIconRectangles();
			this.renderAllIcons(g);
			if(hWnd!=IntPtr.Zero)
			{
				// Take screen shot for cache (so we do not need to redraw icons all the time)
				this.createBufferBitmapWithIcons(hWnd, actualRect);
			}
		}

		private void calculateKeyAndRowRectangles()
		{
			keyRectangles = new ArrayList();
			rowRectangles = new ArrayList();
			PointF keyLocation = new PointF(0,0);
			float keyActualSize = clientRectangle.Width / keyboardSize.Width;
			foreach(KeyboardLayoutRow row in layout.Rows)
			{
				if(!row.IsPlaceHolder)
				{
					RectangleF actualRowRect = new RectangleF(new PointF( ((keyLocation.X + edgeResultantOffset) * keyActualSize), (keyLocation.Y + edgeResultantOffset) * keyActualSize) , new SizeF(0,0));
					foreach(KeyboardLayoutKey key in row.Keys)
					{
						RectangleF worldKeyRect = new RectangleF( new PointF( (keyLocation.X + edgeResultantOffset) * 100 , (keyLocation.Y + edgeResultantOffset) * 100) , new SizeF(  key.Width * 100 , key.Height * 100)  );
						RectangleF actualKeyRect = new RectangleF( new PointF( ((keyLocation.X + edgeResultantOffset) * keyActualSize) , ((keyLocation.Y + edgeResultantOffset) * keyActualSize)) , new SizeF( keyActualSize * key.Width , keyActualSize * key.Height)  );
						// Add rect to keyRectangles
						keyRectangles.Add(new ItemRect(key, actualKeyRect,worldKeyRect));
						keyLocation.X += (Single)key.Width;
					}
					actualRowRect.Height = row.Height * keyActualSize;
					actualRowRect.Width = (keyLocation.X - edgeResultantOffset) * keyActualSize;
					rowRectangles.Add(new ItemRect(row, actualRowRect,actualRowRect));
				}
				keyLocation.Y += (Single)row.Height;
				keyLocation.X = 0;
			}
		}

		private void calculateIconRectangles()
		{
			iconRectangles = new ArrayList();
			// TODO: Icon issue
			SizeF iconActualSize = new SizeF( 32 , 32 );
			float keyActualSize = clientRectangle.Width / keyboardSize.Width;
			PointF iconActualOffset = new PointF( ((keyActualSize / 100) * style.OffsetIcon.X) , ((keyActualSize / 100) * style.OffsetIcon.Y) );
			foreach(ItemRect keyRect in keyRectangles)
			{
				KeyboardLayoutKey key = (KeyboardLayoutKey)keyRect.Item;
				RectangleF actualKeyRect = keyRect.ActualRectangle;
				if(!key.IsPlaceHolder && key.Icon!=null)
				{
					PointF actualIconPoint = new PointF(0,0);
					actualIconPoint.X = actualKeyRect.Right - iconActualSize.Width - iconActualOffset.X;
					actualIconPoint.Y = actualKeyRect.Bottom - iconActualSize.Height - iconActualOffset.Y;
					RectangleF actualIconRect = new RectangleF( actualIconPoint , iconActualSize);
					iconRectangles.Add(new ItemRect(key, actualIconRect,actualIconRect));
				}
			}
		}

		private Rectangle actualRect;
		private KeyboardLayout layout = null;
		private KeyboardStyle style = null;
		private Hashtable language = null;
		private ShiftState shiftState = ShiftState.None;
		private Rectangle clientRectangle;

		public void RenderAll(Graphics g, IntPtr hWnd, KeyboardLayout layout, KeyboardStyle style,Hashtable language, ShiftState shiftState, Rectangle clientRectangle)
		{
			this.setColors(style);
			this.style = style;
			this.layout = layout;
			this.language = language;
			this.shiftState = shiftState;
			this.clientRectangle = clientRectangle;
			this.keyboardSize = this.calculateLogicalKeyboardSize(layout , style);
			this.usableClientRectangle = new Rectangle(clientRectangle.Location, clientRectangle.Size);
			this.actualRect = new Rectangle(clientRectangle.Location, clientRectangle.Size);
			if(keyboardSize.Width > keyboardSize.Height)
			{
				usableClientRectangle.Width = clientRectangle.Width;
				usableClientRectangle.Height =  clientRectangle.Width;
				actualRect.Width = clientRectangle.Width;
				actualRect.Height =  (int)((((double)clientRectangle.Width) / ((double)keyboardSize.Width)) * (double)keyboardSize.Height);
			}
			else
			{
				usableClientRectangle.Height = clientRectangle.Height;
				usableClientRectangle.Width = (int)(clientRectangle.Height * (keyboardSize.Height / keyboardSize.Width));
			}

			this.actualSize = new Size(actualRect.Width, actualRect.Height);
			
			edgeResultantOffset = ((float)style.EdgeExternalWidth / 100) + ((float)style.EdgeInternalWidth / 100);
			float totalEdgeWidth = edgeResultantOffset * 2;

			
			if(this.bufferBitmap==null)
			{
				this.renderKeyBoardBuffer();
			}
			if(g!=null)
			{
				this.renderKeyBoardBufferWithIcons(g, hWnd);
			}
		}
		
		Bitmap bufferBitmapWithIcons = null;
		Bitmap bufferBitmapWithIconsSpare = null;
		private void createBufferBitmapWithIcons(IntPtr hWnd, Rectangle rect)
		{
			// This actually takes a screen shot from the hWnd
			// Rational: we can only render icons properly on the final surface, this way we cache that final surface.
			Graphics onsScreenGraphics = Graphics.FromHwnd(hWnd);
			bufferBitmapWithIcons = new Bitmap(rect.Width, rect.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
			Graphics bufferWithIconsGraphics = Graphics.FromImage(bufferBitmapWithIcons);
			IntPtr onScreenDC = onsScreenGraphics.GetHdc();
			IntPtr bufferWithIconsDC = bufferWithIconsGraphics.GetHdc();
			Win32Interop.BitBlt(bufferWithIconsDC, 0, 0, rect.Width, rect.Height, onScreenDC, 0, 0, 13369376);
			onsScreenGraphics.ReleaseHdc(onScreenDC);
			bufferWithIconsGraphics.ReleaseHdc(bufferWithIconsDC);
			bufferBitmapWithIconsSpare = (Bitmap)bufferBitmapWithIcons.Clone();
		}

		public void UpdateCacheWithScreen(IntPtr hWnd, KeyboardLayoutKey key)
		{
			Graphics onsScreenGraphics = Graphics.FromHwnd(hWnd);
			Graphics bufferWithIconsGraphics = Graphics.FromImage(bufferBitmapWithIcons);
			IntPtr onScreenDC = onsScreenGraphics.GetHdc();
			IntPtr bufferWithIconsDC = bufferWithIconsGraphics.GetHdc();
			Rectangle rect = Rectangle.Truncate( this.getItemRectForKey(key).ActualRectangle );
			Win32Interop.BitBlt(bufferWithIconsDC, rect.Top , rect.Left, rect.Width, rect.Height, onScreenDC,rect.Top , rect.Left, 13369376);
			onsScreenGraphics.ReleaseHdc(onScreenDC);
			bufferWithIconsGraphics.ReleaseHdc(bufferWithIconsDC);
			bufferBitmapWithIconsSpare = (Bitmap)bufferBitmapWithIcons.Clone();
		}

		private void renderIcon(Graphics g, KeyboardLayoutKey key, RectangleF rectangle)
		{
			// Icons are rendered on the final surface: bugs in the framework mess up alpha data
			g.DrawIcon(key.Icon, new Rectangle(new Point((int)rectangle.Left,(int)rectangle.Top),new Size((int)rectangle.Width,(int)rectangle.Height)));
		}

		private void renderAllIcons(Graphics g)
		{
			foreach(ItemRect iconRect in this.iconRectangles)
			{
				this.renderIcon(g, (KeyboardLayoutKey)iconRect.Item, iconRect.ActualRectangle);
			}
		}

		private void renderKey(Graphics g, KeyboardLayoutKey key, PointF position, Color edgeColor)
		{
			// Renders a single key
			// Should/might be replaced by some add-in mechanisme so the drawing stuff can be replaced
			g.SmoothingMode = SmoothingMode.HighQuality;
			Pen newpen = new Pen(new SolidBrush(edgeColor), outerPen.Width);
			// Draw bottom rectangle
			RectangleF outerRect = new  RectangleF(position.X + (outerPen.Width / 2) ,position.Y + (outerPen.Width / 2),(100 * key.Width) - outerPen.Width,(100 * key.Height) - outerPen.Width);
			LinearGradientBrush outerBrush=new LinearGradientBrush(outerRect,darkColor,lightColor,90,false);
			this.drawRoundRect(g, newpen,outerBrush,outerRect.X,outerRect.Y,outerRect.Width,outerRect.Height,7.5F);	
			// Draw top rectangle
			RectangleF innerRect = new  RectangleF(position.X + 10, position.Y + 10,(100 * key.Width) - 20,(100 * key.Height) - (30 * (key.Height>=1 ? 1 : key.Height) ));
			LinearGradientBrush innerBrush =new LinearGradientBrush(innerRect,lightColor,darkColor,90,false);
			this.drawRoundRect(g, new Pen(new SolidBrush(darkColor),1),innerBrush,innerRect.X,innerRect.Y,innerRect.Width, innerRect.Height,7.5F);	
			// Draw key text
			if(key.Texts[0].ToString().Length>1 && key.Texts.Count==1 && key.ScanCode==0)
				g.DrawString(key.Texts[0].ToString(), new Font("Arial Narrow",20,FontStyle.Italic),new SolidBrush(fontColor),position.X + 10, position.Y + 10);
			else if (/*key.Texts[0].ToString().Length==1 &&*/ key.Texts.Count==1)
			{
				if(this.language!=null && key.ScanCode!=0)
				{
					if(this.languageHasShiftState())
					{
						string text = (string)((Hashtable)this.language[this.shiftState])[key.ScanCode];
						g.DrawString(text, new Font("Arial Narrow",30,FontStyle.Italic),new SolidBrush(fontColor),position.X + 10, position.Y + 10);
					}
				}
				else
					g.DrawString(key.Texts[0].ToString(), new Font("Arial Narrow",30,FontStyle.Italic),new SolidBrush(fontColor),position.X + 10, position.Y + 10);
			}
			else if(key.Texts.Count==2)
				g.DrawString(key.Texts[1].ToString(), new Font("Wingdings",30),new SolidBrush(fontColor),position.X + 10, position.Y + 10);
		}

        private void renderKeyToFile( KeyboardLayoutKey key, Color edgeColor, string filename)
        {

            Pen newpen = new Pen(new SolidBrush(edgeColor), outerPen.Width);
            // Draw bottom rectangle
            RectangleF outerRect = new RectangleF((outerPen.Width / 2), (outerPen.Width / 2), (100 * key.Width) - outerPen.Width, (100 * key.Height) - outerPen.Width);

            Bitmap bmp = new Bitmap((int)Math.Ceiling(outerRect.Width + outerPen.Width), (int)Math.Ceiling(outerRect.Height + outerPen.Width));
            using (Graphics g = Graphics.FromImage(bmp))
            {

            // Renders a single key
            // Should/might be replaced by some add-in mechanisme so the drawing stuff can be replaced
            g.SmoothingMode = SmoothingMode.HighQuality;
            LinearGradientBrush outerBrush = new LinearGradientBrush(outerRect, darkColor, lightColor, 90, false);
            this.drawRoundRect(g, newpen, outerBrush, outerRect.X, outerRect.Y, outerRect.Width, outerRect.Height, 7.5F);
            // Draw top rectangle
            RectangleF innerRect = new RectangleF(10, 10, (100 * key.Width) - 20, (100 * key.Height) - (30 * (key.Height >= 1 ? 1 : key.Height)));
            LinearGradientBrush innerBrush = new LinearGradientBrush(innerRect, lightColor, darkColor, 90, false);
            this.drawRoundRect(g, new Pen(new SolidBrush(darkColor), 1), innerBrush, innerRect.X, innerRect.Y, innerRect.Width, innerRect.Height, 7.5F);
            // Draw key text
            if (key.Texts[0].ToString().Length > 1 && key.Texts.Count == 1 && key.ScanCode == 0)
                g.DrawString(key.Texts[0].ToString(), new Font("Arial Narrow", 20, FontStyle.Italic), new SolidBrush(fontColor), 10, 10);
            else if (/*key.Texts[0].ToString().Length==1 &&*/ key.Texts.Count == 1)
            {
                if (this.language != null && key.ScanCode != 0)
                {
                    if (this.languageHasShiftState())
                    {
                        string text = (string)((Hashtable)this.language[this.shiftState])[key.ScanCode];
                        g.DrawString(text, new Font("Arial Narrow", 30, FontStyle.Italic), new SolidBrush(fontColor), 10, 10);
                    }
                }
                else
                    g.DrawString(key.Texts[0].ToString(), new Font("Arial Narrow", 30, FontStyle.Italic), new SolidBrush(fontColor), 10, 10);
            }
            else if (key.Texts.Count == 2)
                g.DrawString(key.Texts[1].ToString(), new Font("Wingdings", 30), new SolidBrush(fontColor), 10, 10);

            }
            bmp.MakeTransparent(Color.White);
            bmp.Save(filename,ImageFormat.Png);
        }

		private ItemRect getItemRectForKey(KeyboardLayoutKey key)
		{
			foreach(ItemRect itemRect in keyRectangles)
			{
				if((KeyboardLayoutKey)itemRect.Item==key)return itemRect;
			}
			return null;
		}

		private bool languageHasShiftState()
		{
			foreach(object shiftState in this.language.Keys)
				if(shiftState.GetType() == typeof(ShiftState)) // Strange we need to check type here (after doing a typed foreach!)
					if((ShiftState)shiftState == this.shiftState)
						return true;
			return false;
		}

		private void drawRoundRect(Graphics g, Pen p, Brush fillBrush, float X, float Y, float width, float height, float radius)
		{
			// Draws a rectangle with rounded corners
			GraphicsPath gp=new GraphicsPath();
			gp.AddLine(X + radius, Y, X + width - (radius*2), Y);
			gp.AddArc(X + width - (radius*2), Y, radius*2, radius*2, 270, 90);
			gp.AddLine(X + width, Y + radius, X + width, Y + height - (radius*2));
			gp.AddArc(X + width - (radius*2), Y + height - (radius*2), radius*2, radius*2,0,90);
			gp.AddLine(X + width - (radius*2), Y + height, X + radius, Y + height);
			gp.AddArc(X, Y + height - (radius*2), radius*2, radius*2, 90, 90);
			gp.AddLine(X, Y + height - (radius*2), X, Y + radius);
			gp.AddArc(X, Y, radius*2, radius*2, 180, 90);
			gp.CloseFigure();
			if(fillBrush!=null)
				g.FillPath(fillBrush,gp);
			if(p.Width!=0)
				g.DrawPath(p, gp);
			gp.Dispose();
		}

		private void initGraphics(Graphics g)
		{
			// Initialize render quality and coordinates
			g.CompositingQuality = CompositingQuality.HighQuality;
			g.InterpolationMode = InterpolationMode.High;
			g.SmoothingMode = SmoothingMode.HighQuality;
			initializeCoordinates(g, keyboardSize, usableClientRectangle);
		}

		private void initializeCoordinates(Graphics g, SizeF size, Rectangle clientRectangle)
		{
			// Intializes coordinate space in such a way that a 1x1 key is 100x100 world points large
			if (clientRectangle.Width == 0 || clientRectangle.Height == 0)
				return;

			float finches = Math.Min(clientRectangle.Width / g.DpiX, clientRectangle.Height / g.DpiY);
			
			float actualSize = 0;
			if(size.Width > size.Height)
				actualSize = size.Width;
			else
				actualSize = size.Height;

			g.ScaleTransform(finches * g.DpiX / (100 * actualSize) , finches * g.DpiY / (100 * actualSize));
		}

		private SizeF calculateLogicalKeyboardSize(KeyboardLayout layout, KeyboardStyle style)
		{
			// 'Logical' because a default key is 1-by-1... keys internally use a logical grid of 100-by-100
			SizeF keyboardSize = new SizeF();
			foreach(KeyboardLayoutRow row in layout.Rows)
			{
				keyboardSize.Height += row.Height;
				Single currentRowWidth = 0;
				foreach(KeyboardLayoutKey key in row.Keys)
				{
					currentRowWidth += key.Width;
				}
				if(currentRowWidth > keyboardSize.Width)
					keyboardSize.Width = currentRowWidth;
			}
			// Take into account external edge + internal edge of entire 
			float totalEdgeWidth = ((float)style.EdgeExternalWidth * 2 / 100) + ((float)style.EdgeInternalWidth * 2 / 100);
			keyboardSize.Width += totalEdgeWidth;
			keyboardSize.Height += totalEdgeWidth;
			return keyboardSize;
		}

		private void setColors(KeyboardStyle style)
		{
			this.outerPen = new Pen(new SolidBrush(style.OuterPenColor), style.OuterPenWidth);
			this.darkColor = style.DarkColor;
			this.lightColor = style.LightColor;
			this.fontColor = style.FontColor;
			this.backColor = style.BackColor;
		}

		private class ItemRect
		{
			// Class used to store item and onscreen location data (Real and World)
			public ItemRect()
			{
			}

			public ItemRect(object item, RectangleF rectangleF, RectangleF worldRectangleF)
			{
				this.Item = item;
				this.ActualRectangle = rectangleF;
				this.WorldRectangle = worldRectangleF;
			}

			public object Item = null;
			public RectangleF ActualRectangle = new RectangleF(0,0,0,0);
			public RectangleF WorldRectangle = new RectangleF(0,0,0,0);	
		}
	}
}
