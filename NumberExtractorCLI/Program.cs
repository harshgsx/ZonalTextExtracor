using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Tesseract;
using System.IO;
using System.Diagnostics;

namespace NumberExtractorCLI
{
    class Program
    {
        static void TiffFileCropper()
        {
            string TiffFiles = @"E:\Projects\TiffOutput_GrayScale\";
            //string singleTiffFile = @"E:\Projects\RGB_TiffOutput\1 - Copy.tif";
            //string singleTiffFile = @"E:\Projects\MonoChrome_TiffOutput\1.tif";
            string singleTiffFile = @"E:\Projects\Gray_TiffOutput\1.tif";
            string singleTiffFileOverLayed = @"E:\Projects\RGB_TiffOutput\_2.tif";

            if (!Directory.Exists(TiffFiles))
            {
                Directory.CreateDirectory(TiffFiles);
            }


            var tiffFile = Bitmap.FromFile(singleTiffFile);

            int width = 1550;
            int height = 550;
            int fileCounter = 1;
            //using (Graphics g = Graphics.FromImage(tiffFile))
            {
                for (int y = 390; y < 5500;)
                {
                    int StartY = y;
                    int newColor = 50;
                    for (int x = 210; x < 3900;)
                    {
                        int startX = x;

                        Rectangle cropRect = new Rectangle(startX, StartY, width, 90);
                        Color funnyColor = Color.FromArgb(255, 123, 45, newColor);
                        Bitmap croppedImage = new Bitmap(width, 90);
                        using (Graphics tagretImage = Graphics.FromImage(croppedImage))
                        {
                            tagretImage.DrawImage(tiffFile, new Rectangle(0, 0, width, 90), cropRect, GraphicsUnit.Pixel);
                        }
                        string croppedImageFileName = TiffFiles + fileCounter++ + ".tiff";
                        croppedImage.Save(croppedImageFileName, System.Drawing.Imaging.ImageFormat.Tiff);
                        //using (SolidBrush br = new SolidBrush(funnyColor))
                        //{
                        //    Pen p = new Pen(br, 10);
                        //    g.DrawRectangle(p, startX, StartY, width, height);
                        Console.WriteLine("X:{0}, Y:{1}, Width:{2}, Height:{3} ", startX, StartY, width, 90);
                        //}
                        x += width;
                    }
                    y += height;
                }



            }
            tiffFile.Save(singleTiffFileOverLayed, System.Drawing.Imaging.ImageFormat.Tiff);

        }

        static void tes()
        {
            string TiffFiles = @"E:\Projects\TiffOutput_GrayScale";
            try
            {
                foreach (string file in Directory.GetFiles(TiffFiles))
                {
                    Console.WriteLine("----------------");
                    Console.WriteLine(file);

                    using (var engine = new TesseractEngine(@"E:\Projects\tessdata_best-master\tessdata_best-master", "eng", EngineMode.Default))
                    {
                        using (var img = Pix.LoadFromFile(file))
                        {
                            using (var page = engine.Process(img))
                            {
                                var text = page.GetText();
                                Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                                Console.WriteLine("Text (GetText): \r\n{0}", text);
                                //Console.WriteLine("Text (iterator):");
                                //using (var iter = page.GetIterator())
                                //{
                                //    iter.Begin();

                                //    do
                                //    {
                                //        do
                                //        {
                                //            do
                                //            {
                                //                do
                                //                {
                                //                    //if (iter.IsAtBeginningOf(PageIteratorLevel.Block))
                                //                    //{
                                //                    //  // Console.WriteLine("<BLOCK>");
                                //                    //}

                                //                    //Console.Write(iter.GetText(PageIteratorLevel.Word));
                                //                    //Console.Write(" ");

                                //                    //if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                //                    //{
                                //                    //    Console.WriteLine();
                                //                    //}
                                //                } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                                //                if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                                //                {
                                //                    Console.WriteLine();
                                //                }
                                //            } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                                //        } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                                //    } while (iter.Next(PageIteratorLevel.Block));
                                //}
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                Console.WriteLine("Unexpected Error: " + e.Message);
                Console.WriteLine("Details: ");
                Console.WriteLine(e.ToString());
            }
            //Console.Write("Press any key to continue . . . ");
            //Console.ReadKey(true);
        }

        static void Main(string[] args)
        {
            //TiffFileCropper();
            tes();
        }
    }


    }

