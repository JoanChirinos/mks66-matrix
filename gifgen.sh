for i in $(seq -f "%04g" 0 10 359); do
mono MatrixTester.exe ${i}
convert ${i}Square.ppm ${i}.png
rm ${i}Square.ppm
done
