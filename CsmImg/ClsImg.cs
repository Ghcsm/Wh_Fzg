using System;
using Emgu.CV;
using Emgu.CV.Stitching;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Collections.Generic;
using System.Drawing;
using DAL;

namespace CsmImg
{
    public static class ClsImg
    {
        public static Bitmap ImgPj(Bitmap b1, Bitmap b2)
        {
            Bitmap bmp = null;
            Image<Bgr, byte> a = new Image<Bgr, byte>(b1);
            Image<Bgr, byte> b = new Image<Bgr, byte>(b2);
            Stitcher stitcher = new Stitcher(false);
            Mat outimg = new Mat();
            try {
                if (T_ConFigure.SfName.Trim().Length>0)
                stitcher.Stitch(new VectorOfMat(new Mat[] { a.Mat, b.Mat }), outimg);
            } catch {
                return bmp;
            }
            return outimg.Bitmap;
        }

        public static Bitmap ImgPj(string str, string str2)
        {
            Bitmap bmp = null;
            Image<Bgr, byte> a = new Image<Bgr, byte>(str);
            Image<Bgr, byte> b = new Image<Bgr, byte>(str2);
            Stitcher stitcher = new Stitcher(false);
            Mat outimg = new Mat();
            try {
                if (T_ConFigure.SfName.Trim().Length > 0)
                    stitcher.Stitch(new VectorOfMat(new Mat[] { a.Mat, b.Mat }), outimg);
            } catch {
                return bmp;
            }
            return outimg.Bitmap;
        }

        public static Bitmap ImgPj(List<string> strfile)
        {
            Bitmap bmp = null;
            Mat outimg;
            try {
                List<Mat> listmat = new List<Mat>();
                for (int i = 0; i < strfile.Count; i++) {
                    string str = strfile[i].ToString();
                    Image<Bgr, byte> a = new Image<Bgr, byte>(str);
                    listmat.Add(a.Mat);
                }
                Stitcher stitcher = new Stitcher(false);
                outimg = new Mat();
                if (T_ConFigure.SfName.Trim().Length > 0)
                    stitcher.Stitch(new VectorOfMat(listmat.ToArray()), outimg);
            } catch {
                return bmp;
            }
            return outimg.Bitmap;
        }

        public static Bitmap ImgPj(List<Bitmap> strfile)
        {
            Bitmap bmp = null;
            Mat outimg;
            try {
                List<Mat> listmat = new List<Mat>();
                for (int i = 0; i < strfile.Count; i++) {
                    Bitmap bmp1 = strfile[i];
                    Image<Bgr, byte> a = new Image<Bgr, byte>(bmp1);
                    listmat.Add(a.Mat);
                }
                Stitcher stitcher = new Stitcher(false);
                outimg = new Mat();
                if (T_ConFigure.SfName.Trim().Length > 0)
                    stitcher.Stitch(new VectorOfMat(listmat.ToArray()), outimg);
            } catch {
                return bmp;
            }
            return outimg.Bitmap;
        }


        public static Bitmap ImgPj(Bitmap b1, Bitmap b2, Bitmap b3, Bitmap b4)
        {
            Bitmap bmp = null;
            Image<Bgr, byte> a = new Image<Bgr, byte>(b1);
            Image<Bgr, byte> b = new Image<Bgr, byte>(b2);
            Image<Bgr, byte> c = new Image<Bgr, byte>(b3);
            Image<Bgr, byte> d = new Image<Bgr, byte>(b4);
            Stitcher stitcher = new Stitcher(false);
            Mat outimg = new Mat();
            try {
                if (T_ConFigure.SfName.Trim().Length > 0)
                    stitcher.Stitch(new VectorOfMat(new Mat[] { a.Mat, b.Mat, c.Mat, d.Mat }), outimg);
            } catch {
                return bmp;
            }
            return outimg.Bitmap;
        }

    }
}
