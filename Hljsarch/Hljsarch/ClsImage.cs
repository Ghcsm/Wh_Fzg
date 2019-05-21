using Leadtools;
using Leadtools.Codecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hljsapi
{
    public static  class ClsImage
    {

        /// <param name="yfile">源图像</param>
        /// <param name="mfile">新图像</param>
        /// <param name="p1">页码1</param>
        /// <param name="p2">页码2</param>
        /// <param name="fileformat">图像格式</param>
        public static bool _SplitImg(string yfile, string mfile, int p1, int p2, string fileformat)
        {
            try {
                RasterImageFormat format = new RasterImageFormat();
                if (fileformat.IndexOf("jpg") >= 0)
                    format = RasterImageFormat.Jpeg;
                else if (fileformat.IndexOf("pdf") >= 0)
                    format = RasterImageFormat.RasPdfJpeg;
                else if (fileformat.IndexOf("tif") >= 0)
                    format = RasterImageFormat.TifJpeg;

                RasterImage _imagepx;
                RasterCodecs _Codef = new RasterCodecs();
                for (int i = p1; i <= p2; i++) {
                    _imagepx = _Codef.Load(yfile, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, i, i);
                    int bit = _imagepx.BitsPerPixel;
                    if (bit != 1) {
                        if (bit != 8) {
                           // _Codef.Options.Jpeg.Save.QualityFactor = 80;
                            _Codef.Save(_imagepx, mfile, format, bit, 1, 1, i, CodecsSavePageMode.Append);

                        }
                        else {
                           // _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codef.Save(_imagepx, mfile, format, 8, 1, 1, i, CodecsSavePageMode.Append);
                        }
                    }
                    else {
                        _Codef.Save(_imagepx, mfile, format, 1, 1, 1, -1, CodecsSavePageMode.Append);
                    }
                }
                _Codef.Dispose();
                return true;
            } catch {
                return false;
            }
        }

        public static bool _SplitImg(string yfile, string mfile, int p1, string fileformat)
        {
            try {
                RasterImageFormat format = new RasterImageFormat();
                if (fileformat.IndexOf("jpg") >= 0)
                    format = RasterImageFormat.Jpeg;
                else if (fileformat.IndexOf("pdf") >= 0)
                    format = RasterImageFormat.RasPdfJpeg;
                else if (fileformat.IndexOf("tif") >= 0)
                    format = RasterImageFormat.TifJpeg;

                RasterImage _imagepx;
                RasterCodecs _Codef = new RasterCodecs();
                _imagepx = _Codef.Load(yfile, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, p1, p1);
                int bit = _imagepx.BitsPerPixel;
                if (bit != 1) {
                    if (bit != 8) {
                       // _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                        _Codef.Save(_imagepx, mfile, format, bit, 1, 1, -1, CodecsSavePageMode.Append);

                    }
                    else {
                      //  _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                        _Codef.Save(_imagepx, mfile, format, 8, 1, 1, -1, CodecsSavePageMode.Append);
                    }
                }
                else {
                    _Codef.Save(_imagepx, mfile, format, 1, 1, 1, -1, CodecsSavePageMode.Append);
                }
                _Codef.Dispose();
                return true;
            } catch {
                return false;
            }
        }


    }
}
