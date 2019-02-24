using System;
using System.IO;
using GraphicsMatrixApp;

namespace CanvasApp {

  /*
  Canvas for plotting and writing PPM (P3) files.

  Origin is at bottom left corner, which is dependant solely on the WriteFile method
  */

  class Canvas {

    /* public instance vars */
    public int[,,] Grid { get; }
    public int Width { get; }
    public int Height { get; }

    /* constructors */
    public Canvas() {
      // default width and height (500x500)
      this.Width = 500;
      this.Height = 500;
      this.Grid = new int[500,500,3];
      // default background color is white
      int x, y, z;
      for (y = 0; y < 500; y++) {
        for (x = 0; x < 500; x++) {
          for (z = 0; z < 3; z++) {
            this.Grid[x,y,z] = 255;
          }
        }
      }
    }
    public Canvas(int w, int h) {
      Width = w;
      Height = h;
      Grid = new int[w,h,3];
      // default background color is white
      int x, y, z;
      for (y = 0; y < h; y++) {
        for (x = 0; x < w; x++) {
          for (z = 0; z < 3; z++) {
            this.Grid[x,y,z] = 255;
          }
        }
      }
    }
    public Canvas(int w, int h, int[] color) {
      Width = w;
      Height = h;
      Grid = new int[w,h,3];
      // default background color is white
      int x, y, z;
      for (y = 0; y < h; y++) {
        for (x = 0; x < w; x++) {
          for (z = 0; z < 3; z++) {
            this.Grid[x,y,z] = color[z];
          }
        }
      }
    }

    /* public methods */
    public void WriteFile(string filename) {
      using (StreamWriter sw = new StreamWriter(filename)) {
        // file setup
        sw.WriteLine(String.Format("P3 {0} {1} 255\n", Width, Height));

        int y, x;
        for (y = Height - 1; y >= 0; y--) {
          for (x = 0; x < Width; x++) {
            sw.WriteLine(String.Format("{0} {1} {2}\n", Grid[y,x,0], Grid[y,x,1], Grid[y,x,2]));
          }
        }
      }
    } // end WriteFile method

    public void Plot(int x, int y, int[] color) {
      Grid[y,x,0] = color[0];
      Grid[y,x,1] = color[1];
      Grid[y,x,2] = color[2];
    } // end Plot method

    // draw line given 2 points: [x0, y0], [x1, y1]
    public void DrawLine(double[] p0, double[] p1, int[] color) {
      this.DrawLine((int)Math.Round(p0[0], 0), (int)Math.Round(p0[1], 0), (int)Math.Round(p1[0], 0), (int)Math.Round(p1[1], 0), color);
    }

    // draw line given 2 points: [x0, y0], [x1, y1]
    public void DrawLine(int[] p0, int[] p1, int[] color) {
      this.DrawLine(p0[0], p0[1], p1[0], p1[1], color);
    }

    public void DrawLine(int x0, int y0, int x1, int y1, int[] color) {
      if (x0 > x1) {
        int tempX, tempY;

        tempX = x0;
        x0 = x1;
        x1 = tempX;

        tempY = y0;
        y0 = y1;
        y1 = tempY;
      }

      int x, y, A, B, d;

      x = x0;
      y = y0;
      A = 2 * (y1 - y0);
      B = -2 * (x1 - x0);

      // octants 1/8
      if (Math.Abs(x1 - x0) >= Math.Abs(y1 - y0)) {
        // octant 1
        if (A > 0) {
          d = A + B / 2;
          while (x <= x1) {
            this.Plot(x, y, color);
            if (d > 0) {
              y += 1;
              d += B;
            }
            x += 1;
            d += A;
          }
        }
        // octant 8
        else {
          d = A - B / 2;
          while (x <= x1) {
            this.Plot(x, y, color);
            if (d < 0) {
              y -= 1;
              d -= B;
            }
            x += 1;
            d += A;
          }
        }
      }

      // octant 2/7
      else {
        // octant 2
        if (A > 0) {
          d = A / 2 + B;
          while (y < y1) {
            this.Plot(x, y, color);
            if (d < 0) {
              x += 1;
              d += A;
            }
            y += 1;
            d += B;
          }
        }
        // octant 7
        else {
          d = A / 2 - B;
          while (y > y1) {
            this.Plot(x, y, color);
            if (d > 0) {
              x += 1;
              d += A;
            }
            y -= 1;
            d -= B;
          }
        }
      }
    } // end DrawLine method

    // draw lines given GraphicsMatrix object
    public void DrawLines(GraphicsMatrix matrix) {
      int i;
      for (i = 0; i < matrix.Cols - 1; i += 2) {
        this.DrawLine(matrix.GetPoint(i), matrix.GetPoint(i + 1), new int[3] {0, 0, 0});
      }
    }


  } // end Canvas class
} // end CanvasApp namespace
