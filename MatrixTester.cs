using System;
using System.IO;

using CanvasApp;
using GraphicsMatrixApp;

namespace MatrixTesterApp {
  class MatrixTester {

    // method for testing matrix
    public static void Test() {
      Canvas c = new Canvas();
      GraphicsMatrix a = new GraphicsMatrix(
        new double[,] {{1, 2, 3, 4}, {5, 6, 7, 8}, {9, 10, 11, 12}, {13, 14, 15, 16}}
      );
      GraphicsMatrix b = new GraphicsMatrix(
        new double[,] {{11, 12, 13, 14}, {15, 16, 17, 18}, {19, 20, 21, 22}, {23, 24, 25, 26}}
      );

      Console.WriteLine("MATRIX A:");
      a.Print();
      Console.WriteLine("\n");

      Console.WriteLine("MATRIX B:");
      b.Print();
      Console.WriteLine("\n");

      Console.WriteLine("====================\na.mult(b)\n====================");

      a.Multiply(b);

      Console.WriteLine("MATRIX A:");
      a.Print();
      Console.WriteLine("\n");

      Console.WriteLine("MATRIX B:");
      b.Print();
      Console.WriteLine("\n");

      Console.WriteLine("====================\nb.mult(a)\n====================");

      b.Multiply(a);

      Console.WriteLine("MATRIX A:");
      a.Print();
      Console.WriteLine("\n");

      Console.WriteLine("MATRIX B:");
      b.Print();
      Console.WriteLine("\n");

      Console.WriteLine("====================\n(IDENTITY(4)).mult(a)\n====================");

      GraphicsMatrix aIdentity = GraphicsMatrix.GetIdentity(4);
      aIdentity.Multiply(a);

      Console.WriteLine("IDENTITY");
      aIdentity.Print();
      Console.WriteLine("\n");

      Console.WriteLine("MATRIX A:");
      a.Print();
      Console.WriteLine("\n");

      Console.WriteLine("These are the first values you gave us. I promise it's all correct. I checked! I wouldn't want to jeopardize my own code by \"faking\" matrix multiplication xD\n");

      Console.WriteLine("Now I'm going to draw a pretty image!\n");
    }

    // method for drawing pretty stuff using martrix
    public static void Art() {
      Canvas c = new Canvas();
      GraphicsMatrix gm = new GraphicsMatrix();

      // draw small square in the middle
      gm.AddEdge(225, 225, 0, 275, 225, 0);
      gm.AddEdge(275, 225, 0, 275, 275, 0);
      gm.AddEdge(275, 275, 0, 225, 275, 0);
      gm.AddEdge(225, 275, 0, 225, 225, 0);

      // premade rotation matrix for speeeeed
      GraphicsMatrix rotationMatrix = GraphicsMatrix.GetRotationMatrix(0, 0, 1);

      // loop and make 360 images
      int counter;
      for (counter = 0; counter < 36; counter += 4) {
        c = new Canvas();
        c.DrawLines(gm);
        c.WriteFile(String.Format("{0}.ppm", counter.ToString("D4")));
        gm.Rotate(250, 250, 0, rotationMatrix);
        if (counter % 30 == 0) {
          Console.WriteLine("Don't exit the program. Trust meee");
        }
      }
    }

    public static void Art(int frame) {
      Canvas c = new Canvas();
      GraphicsMatrix gm = new GraphicsMatrix();

      // draw small square in the middle
      gm.AddEdge(225, 225, 0, 275, 225, 0);
      gm.AddEdge(275, 225, 0, 275, 275, 0);
      gm.AddEdge(275, 275, 0, 225, 275, 0);
      gm.AddEdge(225, 275, 0, 225, 225, 0);

      // premade rotation matrix for speeeeed
      GraphicsMatrix rotationMatrix = GraphicsMatrix.GetRotationMatrix(0, 0, frame);

      c = new Canvas();
      gm.Rotate(250, 250, 0, rotationMatrix);
      c.DrawLines(gm);
      c.WriteFile(String.Format("{0}Square.ppm", frame.ToString("D4")));
    }

    public static void Main(string[] args) {
      // for testing
      // Test();

      // for drawing pretty matrix art
      if (args.Length >= 1) {
        Console.WriteLine(String.Format("Frame {0}", args[0]));
        int frame = Convert.ToInt32(args[0]);
        Art(frame);
      }
      else {
        Art();
      }
    } // end Main method

  } // end MatrixTester class
} // end MatrixTesterApp namespace
