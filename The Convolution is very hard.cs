using System;

namespace _24
{
    class Program
    {
        
            static void Main(string[] args)
            {
                string imgdata;
                Console.WriteLine("Please insert the destination of the pic flie");
                imgdata = Console.ReadLine();
                double[,] imgdataarray = ReadImageDataFromFile(imgdata);

                string convodata;
                Console.WriteLine("Please insert the destination of Convolution Kernel");
                convodata = Console.ReadLine();
                double[,] convoarray = ReadImageDataFromFile(convodata);

                string outputdata;
                Console.WriteLine("Please insert the destination of the out put flie");
                outputdata = Console.ReadLine();

                double[,] outputimgdata;
                double[,] newarray = Expanded(imgdataarray);




                outputimgdata = Convolution(newarray, imgdataarray.GetLength(0), imgdataarray.GetLength(1), convoarray);
                WriteImageDataToFile(outputdata, outputimgdata);


            }

            static double[,] Expanded(double[,] Readdata)
            {
                int expandedrow, expandedcollumn;

                expandedrow = 7;
                expandedcollumn = 7;
                double[,] n = new double[expandedrow, expandedcollumn];

                for (int i = 0; i < expandedrow; i++)
                {
                    for (int j = 0; j < expandedcollumn; j++)
                    {
                        n[i, j] += Readdata[(i + 4) % 5, (j + 4) % 5];
                    }
                }
                return n;

            }

            static double[,] Convolution(double[,] expendeddataarry, int arrycollumn, int arryrow, double[,] convo)
            {
                double[,] output = new double[arrycollumn, arryrow];

                for (int i = 0; i < arrycollumn; i++)
                {
                    for (int j = 0; j < arryrow; j++)
                    {
                        for (int k = 0; k < convo.GetLength(0); k++)
                        {
                            for (int p = 0; p < convo.GetLength(1); p++)
                            {
                                output[i, j] += expendeddataarry[(k + i), (p + j)] * convo[k, p];
                            }
                        }
                    }
                }
                return output;

            }

            static double[,] ReadImageDataFromFile(string imageDataFilePath)
            {
                string[] lines = System.IO.File.ReadAllLines(imageDataFilePath);
                int imageHeight = lines.Length;
                int imageWidth = lines[0].Split(',').Length;
                double[,] imageDataArray = new double[imageHeight, imageWidth];

                for (int i = 0; i < imageHeight; i++)
                {
                    string[] items = lines[i].Split(',');
                    for (int j = 0; j < imageWidth; j++)
                    {
                        imageDataArray[i, j] = double.Parse(items[j]);
                    }
                }
                return imageDataArray;
            }

            static void WriteImageDataToFile(string imageDataFilePath,
                                             double[,] imageDataArray)
            {
                string imageDataString = "";
                for (int i = 0; i < imageDataArray.GetLength(0); i++)
                {
                    for (int j = 0; j < imageDataArray.GetLength(1) - 1; j++)
                    {
                        imageDataString += imageDataArray[i, j] + ", ";
                    }
                    imageDataString += imageDataArray[i,
                                                    imageDataArray.GetLength(1) - 1];
                    imageDataString += "\n";
                }

                System.IO.File.WriteAllText(imageDataFilePath, imageDataString);
            }


        }
}
