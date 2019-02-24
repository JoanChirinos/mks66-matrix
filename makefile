all:
	csc MatrixTester.cs Canvas.cs GraphicsMatrix.cs
	./gifgen.sh
	convert *.png animation.gif
	rm *.png
	echo "\n\nOpen animation.gif\n"

clean:
	-rm -f *.exe *.ppm *.png *.gif
