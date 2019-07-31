using Leadtools;
using Leadtools.Codecs;
using Leadtools.Controls;
using Leadtools.Documents.Converters;
using Leadtools.Drawing;
using Leadtools.Forms;
using Leadtools.Forms.DocumentWriters;
using Leadtools.Forms.Ocr;
using Leadtools.ImageProcessing;
using Leadtools.ImageProcessing.Color;
using Leadtools.ImageProcessing.Core;
using Leadtools.ImageProcessing.Effects;
using Leadtools.Twain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HLjscom
{



    #region  初始字段
    public class Hljsimage
    {

        public delegate void ScanPage(int page, int counpage);

        public event ScanPage Spage;
        //扫描仪
        private TwainSession _twains;
        //扫描仪属性
        private TwainCapability _twnCap;
        //图像框架
        private ImageViewer _Imageview;
        //图像文件操作类
        private RasterCodecs _Codefile = new RasterCodecs();
        //调用imageview自带画矩形功能
        private ImageViewerAddRegionInteractiveMode regionInteractiveMode = new ImageViewerAddRegionInteractiveMode();
        //排序页码  
        public Dictionary<int, int> _PageNumber = new Dictionary<int, int>();
        //字母可用 
        public Dictionary<int, string> _PageAbc = new Dictionary<int, string>();

        public Dictionary<int, string> _PageFuhao = new Dictionary<int, string>();

        public List<string> Fuhao = new List<string>();

        Ltoolsapi.Ltoolsapi api = null;
        //注册页码
        public int RegPage;
        //排序页码循环点击次数
        private int dianjicount = 0;
        // PrintDocument 对象
        private PrintDocument _PrintDoc;
        //压缩质量
        private const int Factor = 80;
        //系统临时目录
        public string Filename = "c:\\temp\\scantmp.tif";
        //句柄
        private IntPtr istwan;
        //信息
        private const string strtwan = "未找到扫描仪或不支持！";
        //当前页
        private int CrrentPage = 0;
        //总页数
        private int CountPage = 0;
        //打印
        private int PrintPageC = 0;
        private int PrintPageT = 0;
        //控制是否扫描到本地
        public string scanid = string.Empty;
        // 魔法棒
        bool tf = false;
        //图像转换
        DocumentConverter documentConverter = new DocumentConverter();
        //创建对象
        IOcrEngine ocrEngine = null;
        IOcrPage ocrPage = null;
        //设置扫描模式，追加替换
        public int Scanms = 0;
        public int TagPage = 0;


        #endregion


        #region  初始化数据

        public void Dispose()
        {
            if (_twnCap != null) {
                _twnCap.Dispose();
            }
            if (_Codefile != null) {
                _Codefile.Dispose();
            }
            if (_twains != null) {
                _twains.Shutdown();
            }
            if (_PrintDoc != null) {
                _PrintDoc.Dispose();
            }
            GC.Collect();
        }

        public Hljsimage()
        {
            try {
                api = new Ltoolsapi.Ltoolsapi();
                api._Getpzsys();
                // api.Readdat();

            } catch (Exception ex) {
                throw ex;
            }
        }

        //初始化
        public void _Instimagtwain(ImageViewer image, IntPtr x, int id)
        {

            _Imageview = image;
            istwan = x;
            if (id == 1) {
                if (_Istwain()) {
                    _twnCap = new TwainCapability();
                    _twains = new TwainSession();
                    _twains.Startup(x, "Leadtools", "Leadtools ", "Version 1.0", "TWAIN Test Application",
                        TwainStartupFlags.None);
                    _twains.AcquirePage += new EventHandler<TwainAcquirePageEventArgs>(_twanscan_AcquirePage);

                }
            }
        }

        #endregion

        # region  图像处理
        //滤底
        public void _ImgLd()
        {
            try {
                if (_Imageview.Image != null) {
                    PosterizeCommand command = new PosterizeCommand();
                    command.Levels = 2;
                    command.Run(_Imageview.Image);
                }
            } catch { }
        }


        private void Print()
        {
            if (PrinterSettings.InstalledPrinters == null || PrinterSettings.InstalledPrinters.Count < 1) {
                MessageBox.Show("请检查是否正确安装打印机！");
                return;
            }
            else {
                this._PrintDoc = new PrintDocument();
                this._PrintDoc.PrintPage += new PrintPageEventHandler(PrintPg);
                this._PrintDoc.Print();
            }
        }

        private void PrintPg(object sender, PrintPageEventArgs e)
        {

            this._PrintDoc.PrinterSettings.MinimumPage = this.PrintPageC;
            this._PrintDoc.PrinterSettings.MaximumPage = this.PrintPageT;
            // 默认打印所有页面
            this._PrintDoc.PrinterSettings.FromPage = this._PrintDoc.PrinterSettings.MinimumPage;
            this._PrintDoc.PrinterSettings.ToPage = this._PrintDoc.PrinterSettings.MaximumPage;
            // 打印一页
            PrintDocument document = sender as PrintDocument;
            // 创建一个新的LEADTOOLS image printer类
            RasterImagePrinter printer = new RasterImagePrinter();
            // 设置 document 对象以便进行页面计算
            printer.PrintDocument = document;

            printer.SizeMode = RasterPaintSizeMode.FitAlways;
            printer.HorizontalAlignMode = RasterPaintAlignMode.Center;
            printer.VerticalAlignMode = RasterPaintAlignMode.Center;

            // 考虑具有不同水平和垂直分辨率的传真图像
            printer.UseDpi = true;
            // 打印整个图像
            printer.ImageRectangle = Rectangle.Empty;
            // 使用最大页面维度，这和使用Windows照片库打印等效
            printer.PageRectangle = RectangleF.Empty;
            // 无论我们是否要忽略页边距，都会通知打印机
            //  printer.UseMargins = usePageMarginsToolStripMenuItem.Checked;
            // 打印当前页
            //  rasterImageViewer1.Image = this._rasterCodecs.Load(dlg.FileName);
            LoadPage(PrintPageC);
            printer.Print(_Imageview.Image, 1, e);
            // 转到下一页
            this.PrintPageC++;

            // 无论我们是否要打印更多的页面，都通知打印机
            if (this.PrintPageC <= document.PrinterSettings.ToPage) {
                e.HasMorePages = true;
            }
            else {
                e.HasMorePages = false;
            }
        }

        public void _PrintImg(int page1, int page2)
        {
            try {
                this.PrintPageC = page1;
                this.PrintPageT = page2;
                Print();
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Rectcls()
        {
            if (_Imageview.Image != null)
                _Imageview.Image.MakeRegionEmpty();
        }

        //自动去边
        public void _AutoCrop()
        {
            try {
                AutoCropCommand cmdauto = new AutoCropCommand();
                cmdauto.Threshold = 128;
                cmdauto.Run(_Imageview.Image);
            } catch { }
        }



        //矩形      
        public void _Rectang(Boolean reg)
        {
            try {
                if (_Imageview.Image != null)
                    _Imageview.Image.MakeRegionEmpty();
                if (reg == true) {
                    regionInteractiveMode.AutoRegionToFloater = false;
                    regionInteractiveMode.Shape = ImageViewerRubberBandShape.Rectangle;
                    regionInteractiveMode.BorderPen = Pens.Red;
                    _Imageview.InteractiveModes.BeginUpdate();
                    _Imageview.InteractiveModes.Clear();
                    _Imageview.InteractiveModes.Add(regionInteractiveMode);
                    _Imageview.InteractiveModes.EndUpdate();
                }
                else if (_Imageview.Image != null) {
                    _Imageview.InteractiveModes.Clear();
                    _Imageview.Image.MakeRegionEmpty();
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }


        //微调
        public void _RoteimgWt(ImageViewer img, int x)
        {
            try {
                if (img.Image != null) {
                    RotateCommand _Rotate = null;
                    if (x == 0) {
                        _Rotate = new RotateCommand(-1 * 100, RotateCommandFlags.Resample, new RasterColor(255, 255, 255));
                    }
                    else {
                        _Rotate = new RotateCommand(+1 * 100, RotateCommandFlags.Resample, new RasterColor(255, 255, 255));
                    }
                    Application.DoEvents();
                    _Rotate.Run(img.Image);
                }
            } catch { }
        }

        //旋转
        public void _Roteimage(int x)
        {
            try {
                if (_Imageview.Image != null) {
                    RotateCommand _Rotate = null;
                    if (x == 0) {
                        _Rotate = new RotateCommand(-90 * 100, RotateCommandFlags.Resize, new RasterColor(0, 0, 0));
                    }
                    else {
                        _Rotate = new RotateCommand(+90 * 100, RotateCommandFlags.Resize, new RasterColor(0, 0, 0));
                    }
                    _Rotate.Run(_Imageview.Image);
                    _Imageview.Image.MakeRegionEmpty();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        //矫正
        private void Autodeskew()
        {
            try {
                DeskewCommand cmddesk = new DeskewCommand();
                cmddesk.Flags = DeskewCommandFlags.RotateResample;
                cmddesk.FillColor = new Leadtools.RasterColor(255, 255, 255);
                cmddesk.Run(_Imageview.Image);
                _Imageview.Image.MakeRegionEmpty();

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 两种矫正方法
        /// </summary>        
        /// <param name="x">0:自动矫正;2:剪切矫正</param>
        public void _Deskewimage(int x)
        {
            try {
                if (_Imageview.Image != null) {
                    switch (x) {
                        case 0:
                            Autodeskew();
                            break;
                        case 1:
                            PerspectiveDeskewCommand cmdper = new PerspectiveDeskewCommand();
                            cmdper.Run(_Imageview.Image);
                            break;
                    }
                    _Imageview.Image.MakeRegionEmpty();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }


        //填充
        public void _ImgFill(MouseEventArgs e)
        {
            try {
                if (tf == false) {
                    tf = true;
                    LeadPointD pt = _Imageview.ConvertPoint(null, ImageViewerCoordinateType.Control, ImageViewerCoordinateType.Image, new LeadPointD(e.X, e.Y));
                    Point onpt = new Point((int)pt.X, (int)pt.Y);
                    RasterColor color = _Imageview.Image.GetPixel((int)pt.X, (int)pt.Y);
                    //if (color.R < 200 && color.G < 200 && color.B < 200)
                    //{
                    RasterColor lowerColor = new RasterColor(255, 255, 255);
                    RasterColor upperColor = new RasterColor(0, 0, 0);
                    _Imageview.Image.AddMagicWandToRegion((int)pt.X, (int)pt.Y, lowerColor, upperColor, RasterRegionCombineMode.Set);
                    FillCommand fill = new FillCommand(RasterColor.FromKnownColor(RasterKnownColor.White));
                    fill.Run(_Imageview.Image);
                    //}
                    _Imageview.Image.MakeRegionEmpty();
                    tf = false;
                }
            } catch {
                tf = false;
            }
        }


        //放大缩小
        public void _Sizeimge(int x)
        {
            try {
                if (_Imageview.Image != null) {
                    switch (x) {
                        case 0:
                            _Imageview.Zoom(ControlSizeMode.None, _Imageview.ScaleFactor / 1.2, _Imageview.DefaultZoomOrigin);
                            break;
                        case 1:
                            _Imageview.Zoom(ControlSizeMode.None, _Imageview.ScaleFactor * 1.2, _Imageview.DefaultZoomOrigin);
                            break;
                        default:
                            _Imageview.Zoom(ControlSizeMode.FitAlways, 1, _Imageview.DefaultZoomOrigin);
                            break;
                    }
                    _Imageview.Image.MakeRegionEmpty();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        //自动去边
        private void Autocrop()
        {
            try {
                AutoCropCommand cmdauto = new AutoCropCommand();
                cmdauto.Threshold = 64;
                cmdauto.Run(_Imageview.Image);
                _Imageview.Image.MakeRegionEmpty();
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }


        // 私有-剪切去边
        private void Bcropcmd()
        {
            try {
                CropCommand cmdcrop = new CropCommand();
                cmdcrop.Rectangle = new LeadRect(0, 0, _Imageview.Image.Width - 8, _Imageview.Image.Height - 8);
                cmdcrop.Run(_Imageview.Image);
                cmdcrop.Rectangle = new LeadRect(5, 5, _Imageview.Image.Width - 8, _Imageview.Image.Height - 8);
                cmdcrop.Run(_Imageview.Image);
                _Imageview.Image.MakeRegionEmpty();
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        /// 填充去边
        public void _BfillImage(int id)
        {
            try {
                if (id < _Imageview.Image.Width && id < _Imageview.Image.Height) {
                    FillCommand cmdfill = new FillCommand();
                    LeadRect rc = new LeadRect(0, 0, _Imageview.Image.Width - id, _Imageview.Image.Height - id);
                    _Imageview.Image.AddRectangleToRegion(null, rc, RasterRegionCombineMode.SetNot);
                    cmdfill.Color = RasterColor.White;
                    cmdfill.Run(_Imageview.Image);

                    LeadRect rc1 = new LeadRect(id, id, _Imageview.Image.Width - id, _Imageview.Image.Height - id);
                    _Imageview.Image.AddRectangleToRegion(null, rc1, RasterRegionCombineMode.SetNot);
                    cmdfill.Color = RasterColor.White;
                    cmdfill.Run(_Imageview.Image);
                    _Imageview.Image.MakeRegionEmpty();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }


        //私有-剪切范围之外的图像
        private void Rectcrop()
        {
            try {
                CropCommand cmdcrop = new CropCommand();
                cmdcrop.Rectangle = _Imageview.Image.GetRegionBounds(null);
                cmdcrop.Run(_Imageview.Image);
                _Imageview.Image.MakeRegionEmpty();

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 图像去边处理 
        /// </summary>       
        /// <param name="x">模式区分0为自动,1为剪切;2:剪切范围外部</param>
        public void _Sidecrop(int x)
        {
            try {
                if (_Imageview.Image != null) {
                    switch (x) {
                        case 0:
                            Autocrop();
                            break;
                        case 1:
                            Bcropcmd();
                            break;
                        case 2:
                            Rectcrop();
                            break;
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 私有-图像色深浅
        /// </summary>
        /// <param name="x">0:图像深;1:图像浅</param>
        private void Imagecolor(int x)
        {
            try {
                if (x == 0) {
                    ChangeIntensityCommand cmdcolors = new ChangeIntensityCommand();
                    cmdcolors.Brightness -= 30;
                    cmdcolors.Run(_Imageview.Image);
                }
                else if (x == 1) {
                    ChangeIntensityCommand cmdcolorq = new ChangeIntensityCommand();
                    cmdcolorq.Brightness += 30;
                    cmdcolorq.Run(_Imageview.Image);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 私有-字体颜色深浅
        /// </summary>
        /// <param name="x">2：字体深;3：字体浅</param>
        private void Fontcolor(int x)
        {
            try {
                if (x == 2) {
                    MinimumCommand cmdfonts = new MinimumCommand();
                    cmdfonts.Dimension = 2;
                    cmdfonts.Run(_Imageview.Image);
                }
                else if (x == 3) {
                    MaximumCommand cmdfontq = new MaximumCommand();
                    cmdfontq.Dimension = 2;
                    cmdfontq.Run(_Imageview.Image);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 图像及字体颜色深浅
        /// </summary>        
        /// <param name="x">0:图像深;1：图像浅;2：字体深;3：字体浅</param>
        public void _Imagefontcolor(int x)
        {
            try {
                if (_Imageview.Image != null) {
                    if (x == 0 || x == 1) {
                        Imagecolor(x);
                    }
                    else if (x == 2 || x == 3) {
                        Fontcolor(x);
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 私有-镜像
        /// </summary>
        private void Flipimage()
        {
            try {
                FlipCommand cmdflip = new FlipCommand();
                cmdflip.Horizontal = true;
                cmdflip.Run(_Imageview.Image);
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 镜像反像
        /// </summary>        
        /// <param name="x">0：镜像;1:反像</param>
        public void _Fliprevimage(int x)
        {
            try {
                if (_Imageview.Image != null) {
                    switch (x) {
                        case 0:
                            Flipimage();
                            break;
                        case 1:
                            InvertCommand cmdin = new InvertCommand();
                            cmdin.Run(_Imageview.Image);
                            break;
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 翻页
        /// </summary>      
        /// <param name="x">0：前页;1:后页</param>
        public void _Pagenext(int x)
        {
            try {
                if (_Imageview.Image != null) {
                    switch (x) {
                        case 0:
                            if (CrrentPage > 1) {
                                CrrentPage--;
                                LoadPage(CrrentPage);
                            }
                            break;
                        case 1:
                            if (CrrentPage < CountPage) {
                                CrrentPage++;
                                LoadPage(CrrentPage);
                            }
                            break;
                    }
                    _Imageview.Image.MakeRegionEmpty();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }



        // 私有-图像转灰度
        private void Desaimage()
        {
            try {
                DesaturateCommand cmdcolorhd = new DesaturateCommand();
                cmdcolorhd.Run(_Imageview.Image);
                ColorResolutionCommand cmdbithd = new ColorResolutionCommand();
                cmdbithd.BitsPerPixel = 1;
                cmdbithd.Run(_Imageview.Image);
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }


        /// 私有-图像转黑白
        private void Dynaimage()
        {
            try {
                DynamicBinaryCommand cmdcolorhb = new DynamicBinaryCommand();
                cmdcolorhb.Run(_Imageview.Image);
                ColorResolutionCommand cmdbithb = new ColorResolutionCommand();
                cmdbithb.BitsPerPixel = 1;
                cmdbithb.Run(_Imageview.Image);
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }
        //二值化
        private void ErzhiImg()
        {
            try {
                if (_Imageview.Image != null) {
                    AutoBinarizeCommand command = new AutoBinarizeCommand();
                    command.Run(_Imageview.Image);
                }
            } catch { }
        }

        /// <summary>
        /// 图像颜色转换
        /// </summary>        
        /// <param name="x">0:转灰;1:转黑白;2:二值化，比1效果好</param>
        public void _Formatcolor(int x)
        {
            try {
                if (_Imageview.Image != null) {
                    switch (x) {
                        case 0:
                            Desaimage();
                            break;
                        case 1:
                            Dynaimage();
                            break;
                        case 2:
                            ErzhiImg();
                            break;
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }


        //内外区域填充
        private void Rectfill(int x)
        {
            FillCommand cmdfill = new FillCommand();
            LeadRect rcw = _Imageview.Image.GetRegionBounds(null);
            if (rcw.X > 1 || rcw.Y > 1) {
                if (x == 0) {
                    _Imageview.Image.AddRectangleToRegion(null, rcw, RasterRegionCombineMode.And);
                    cmdfill.Color = RasterColor.White;
                    cmdfill.Run(_Imageview.Image);
                }
                else if (x == 1) {
                    _Imageview.Image.AddRectangleToRegion(null, rcw, RasterRegionCombineMode.SetNot);
                    cmdfill.Color = RasterColor.White;
                    cmdfill.Run(_Imageview.Image);
                }
            }
            _Imageview.Image.MakeRegionEmpty();
        }

        //填充矩形范围
        private void Fillblack()
        {
            if (_Imageview.Image != null) {
                FillCommand filler = new FillCommand(RasterColor.Black);
                filler.Run(_Imageview.Image);
            }
        }

        /// <summary>
        /// 填充矩形范围
        /// </summary>
        /// <param name="x">0:填充内部；>1填充外部</param>
        public void _Fillrect(int x)
        {
            try {
                if (x == 0) {
                    Rectfill(0);
                }
                else if (x == 1) {
                    Rectfill(1);
                }
                else {
                    Fillblack();
                }
            } catch (Exception ex) {
                throw ex;
            }
        }


        //检测空白页
        public Boolean _Blankimg()
        {
            try {
                if (_Imageview.Image != null) {
                    BlankPageDetectorCommand cmdblan = new BlankPageDetectorCommand(); ;
                    cmdblan.Flags = BlankPageDetectorCommandFlags.DetectEmptyPage;
                    cmdblan.Run(_Imageview.Image);
                    if (cmdblan.IsBlank == true) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                return false;
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }


        // 居中
        public void _CenterImg()
        {
            try {
                if (_Imageview.Image != null) {
                    RasterImage rasterImage = _Imageview.Image.Clone();
                    int w = rasterImage.Width;
                    int h = rasterImage.Height;
                    CropCommand cmdcrop = new CropCommand();
                    cmdcrop.Rectangle = _Imageview.Image.GetRegionBounds(null);
                    cmdcrop.Run(_Imageview.Image);
                    AutoCropCommand autoCropCommand = new AutoCropCommand(64);
                    autoCropCommand.Run(rasterImage);
                    //if (_Imageview.Image.ImageHeight < _Imageview.Image.ImageWidth)
                    //{
                    //    //RotateCommand rotateCommand = new RotateCommand(9000, RotateCommandFlags.Resize, new RasterColor(0, 0, 0));
                    // //   rotateCommand.Run(_Imageview.Image);
                    //}
                    //if (rasterImage.ImageHeight < rasterImage.ImageWidth)
                    //{
                    //   // RotateCommand rotateCommand = new RotateCommand(9000, RotateCommandFlags.Resize, new RasterColor(0, 0, 0));
                    //  //  rotateCommand.Run(rasterImage);
                    //}
                    //int num = (int)((double)rasterImage.YResolution / 25.4 * 420.0);
                    //int width = (int)((double)rasterImage.XResolution / 25.4 * 297.0);
                    //int num2 = (int)((double)rasterImage.YResolution / 25.4 * 297.0);
                    //int width2 = (int)((double)rasterImage.XResolution / 25.4 * 210.0);
                    SizeCommand sizeCommand = new SizeCommand();
                    sizeCommand.Flags = RasterSizeFlags.None;
                    //if (rasterImage.ImageHeight > num)
                    //{
                    //    sizeCommand.Width = _Imageview.Image.ImageWidth;
                    //    sizeCommand.Height = _Imageview.Image.ImageHeight;
                    //}
                    //else if (rasterImage.ImageHeight > num2)
                    //{
                    //    sizeCommand.Height = num;
                    //    sizeCommand.Width = width;
                    //}
                    //else
                    //{
                    //    sizeCommand.Width = width2;
                    //    sizeCommand.Height = num2;
                    //}
                    sizeCommand.Width = w;
                    sizeCommand.Height = h;
                    sizeCommand.Run(_Imageview.Image);
                    int x = (_Imageview.Image.ImageWidth - rasterImage.ImageWidth) / 2;
                    int y = (_Imageview.Image.ImageHeight - rasterImage.ImageHeight) / 2;
                    new FillCommand
                    {
                        Color = RasterColor.FromRgb(16777215)
                    }.Run(_Imageview.Image);
                    CombineFastCommand Command = new CombineFastCommand();
                    Command.DestinationRectangle = new LeadRect(
                      x,
                      y,
                      rasterImage.Width,
                      rasterImage.Height);
                    Command.SourcePoint = LeadPoint.Empty;
                    Command.DestinationImage = _Imageview.Image;
                    Command.Flags = CombineFastCommandFlags.OperationAdd | CombineFastCommandFlags.Destination0;
                    Command.Run(rasterImage);
                    _Imageview.Image.MakeRegionEmpty();

                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public void _Filterbot()
        {
            try {
                if (_Imageview.Image != null) {
                    PosterizeCommand command = new PosterizeCommand();
                    command.Levels = 2;
                    command.Run(_Imageview.Image);
                }
            } catch { }
        }
        # endregion

        #region 文件处理

        public void LoadPage(int x)
        {

            try {
                CodecsImageInfo info = _Codefile.GetInformation(Filename, true);
                if (info.BitsPerPixel == 24) {
                    _Codefile.Options.Load.Format = RasterImageFormat.TifxJpeg;
                }
                else {
                    _Codefile.Options.Load.Format = info.Format;
                }

                _Codefile.Options.Load.NoDiskMemory = true;
                _Codefile.Options.Load.Compressed = true;
                _Imageview.Image = _Codefile.Load(Filename, 0, CodecsLoadByteOrder.BgrOrGray, x, x);
                _Imageview.Zoom(ControlSizeMode.FitAlways, 1, _Imageview.DefaultZoomOrigin);
                if (info.TotalPages == 0) {
                    MessageBox.Show("文件页码为0，请重新加载档案！");
                    return;
                }
                else {
                    _Imageview.BeginUpdate();
                    _Imageview.Image.Page = 1;
                    _Imageview.EndUpdate();
                }

                CrrentPage = x;
                CountPage = info.TotalPages;
                Setpage(CrrentPage, CountPage);
            } catch { }
        }

        //保存图像
        public void _SavePage(string _path)
        {
            try {
                if (_Imageview.Image != null) {
                    _Imageview.Image.MakeRegionEmpty();
                    int bit = _Imageview.Image.BitsPerPixel;
                    RasterImage _rimage = _Imageview.Image.Clone();
                    int _CurrectPage = CrrentPage;
                    if (bit != 1) {
                        if (bit != 8) {
                            _Codefile.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codefile.Save(_rimage, _path, RasterImageFormat.TifJpeg, bit, 1, 1, _CurrectPage, CodecsSavePageMode.Replace);
                        }
                        else {
                            _Codefile.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codefile.Save(_rimage, _path, RasterImageFormat.TifJpeg, 8, 1, 1, _CurrectPage, CodecsSavePageMode.Replace);
                        }
                    }
                    else {
                        _Codefile.Save(_rimage, _path, RasterImageFormat.CcittGroup4, 1, 1, 1, _CurrectPage, CodecsSavePageMode.Replace);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            }
        }
        //保存图像
        public void _SavePage()
        {
            try {
                if (_Imageview.Image != null) {
                    _Imageview.Image.MakeRegionEmpty();
                    int bit = _Imageview.Image.BitsPerPixel;
                    RasterImage _rimage = _Imageview.Image.Clone();
                    if (bit != 1) {
                        if (bit != 8) {
                            _Codefile.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codefile.Save(_rimage, Filename, RasterImageFormat.TifJpeg, bit, 1, 1, CrrentPage, CodecsSavePageMode.Replace);
                        }
                        else {
                            _Codefile.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codefile.Save(_rimage, Filename, RasterImageFormat.TifJpeg, 8, 1, 1, CrrentPage, CodecsSavePageMode.Replace);
                        }
                    }
                    else {
                        _Codefile.Save(_rimage, Filename, RasterImageFormat.CcittGroup4, 1, 1, 1, CrrentPage, CodecsSavePageMode.Replace);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public string _GetA3page()
        {
            RasterCodecs _Codef = new RasterCodecs();
            CodecsImageInfo info = _Codefile.GetInformation(Filename, true);
            try {
                int A3 = 0;
                int A0 = 0;
                string apages = string.Empty;
                for (int i = 1; i <= info.TotalPages; i++) {
                    RasterImage _imagepx = _Codef.Load(Filename, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, i, i);
                    if (_imagepx.ImageHeight > 3550 && _imagepx.ImageHeight < 9000 || _imagepx.ImageWidth > 3550 && _imagepx.ImageWidth < 9000) {
                        if (_imagepx.BitsPerPixel >= 200) {
                            A3 += 1;
                        }
                        else {
                            A0 += 1;
                        }
                    }
                    else if (_imagepx.ImageWidth > 9000 || _imagepx.ImageHeight > 9000) {
                        A0 += 1;
                    }

                }
                apages = A0.ToString() + "A" + A3.ToString();
                return apages;

            } catch {
                return "0";
            } finally {
                info.Dispose();
                _Codef.Dispose();
            }
        }

        //图像水印
        public RasterImage WaterImg(RasterImage water, RasterImage img)
        {
            RasterImage rimg = null;
            List<int> lsx = new List<int>();
            List<int> lsy = new List<int>();
            rimg = water;
            int x, y;
            if (ClsInfopar.waterwz > 0) {
                x = 5;
                y = 5;
                lsx.Add(x);
                lsy.Add(y);
                x = 5;
                y = (img.Height - rimg.Height) / 2; ;
                lsx.Add(x);
                lsy.Add(y);
                x = 5;
                y = img.Height - 5;
                lsx.Add(x);
                lsy.Add(y);
                x = img.Width - 5;
                y = 5;
                lsx.Add(x);
                lsy.Add(y);
                x = img.Width - 5;
                y = (img.Height - rimg.Height) / 2;
                lsx.Add(x);
                lsy.Add(y);
                x = img.Width - 5;
                y = img.Height - 5;
                lsx.Add(x);
                lsy.Add(y);
                x = (img.Width - rimg.Width) / 2;
                y = 5;
                lsx.Add(x);
                lsy.Add(y);
                x = (img.Width - rimg.Width) / 2;
                y = (img.Height - rimg.Height) / 2;
                lsx.Add(x);
                lsy.Add(y);
                x = (img.Width - rimg.Width) / 2;
                y = img.Height - 5;
                lsx.Add(x);
                lsy.Add(y);
            }
            if (ClsInfopar.waterwz < 9) {
                AlphaBlendCommand alphaBlend = new AlphaBlendCommand
                {
                    DestinationRectangle = LeadRect.Create(lsx[ClsInfopar.waterwz - 1], lsy[ClsInfopar.waterwz - 1], rimg.Width, rimg.Height),
                    SourceImage = rimg,
                    Opacity = ClsInfopar.watertmd,
                    SourcePoint = new LeadPoint(0, 0)

                };
                alphaBlend.Run(img);
            }
            else if (ClsInfopar.waterwz == 10) {
                for (int i = 0; i < lsx.Count; i++) {
                    AlphaBlendCommand alphaBlend = new AlphaBlendCommand
                    {
                        DestinationRectangle = LeadRect.Create(lsx[i], lsy[i], rimg.Width, rimg.Height),
                        SourceImage = rimg,
                        Opacity = ClsInfopar.watertmd,
                        SourcePoint = new LeadPoint(0, 0)

                    };
                    alphaBlend.Run(img);
                }
            }
            else if (ClsInfopar.waterwz == 11) {
                for (int i = 0; i < lsx.Count; i++) {
                    if (i == 0 || i == 2 || i == 3 || i == 5) {
                        AlphaBlendCommand alphaBlend = new AlphaBlendCommand
                        {
                            DestinationRectangle = LeadRect.Create(lsx[i], lsy[i], rimg.Width, rimg.Height),
                            SourceImage = rimg,
                            Opacity = ClsInfopar.watertmd,
                            SourcePoint = new LeadPoint(0, 0)

                        };
                        alphaBlend.Run(img);
                    }
                }
            }

            return img;
        }


        public Bitmap Getbmp(string file,int page)
        {
            Bitmap bitmap = null;
            try {
                using (RasterCodecs _Codef = new RasterCodecs()) {
                    RasterImage _imagepx = _Codef.Load(file, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, page, page).Clone();
                    Image imgtp = RasterImageConverter.ConvertToImage(_imagepx, ConvertToImageOptions.None);
                    bitmap = new Bitmap(imgtp);
                }
                return bitmap;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return bitmap;
            }

        }

        //文字水印
        public RasterImage WaterImgtxt(RasterImage water)
        {
            RasterImage rimg = null;
            Image imgtp = RasterImageConverter.ConvertToImage(water, ConvertToImageOptions.None);
            List<StringFormat> lsList = new List<StringFormat>();
            if (ClsInfopar.waterid == 0)
                return null;
            Bitmap bitmap = new Bitmap(imgtp);
            Graphics g = Graphics.FromImage(bitmap);
            Rectangle textRect = new Rectangle(Point.Empty, bitmap.Size);
            Font myFont = new Font("宋体", ClsInfopar.waterfontsize, FontStyle.Bold, GraphicsUnit.Point);
            string[] color = ClsInfopar.watercolor.Split(';');
            Brush bush = new SolidBrush(Color.FromArgb(ClsInfopar.watertmd, Convert.ToInt32(color[0]), Convert.ToInt32(color[1]), Convert.ToInt32(color[2])));
            StringFormat sf = new StringFormat();
            sf.Trimming = StringTrimming.EllipsisCharacter;
            if (ClsInfopar.waterwz > 0) {

                sf.LineAlignment = StringAlignment.Near;
                sf.Alignment = StringAlignment.Near;
                lsList.Add(sf);
                sf.LineAlignment = StringAlignment.Near;
                sf.Alignment = StringAlignment.Center;
                lsList.Add(sf);
                sf.LineAlignment = StringAlignment.Near;
                sf.Alignment = StringAlignment.Far;
                lsList.Add(sf);
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Near;
                lsList.Add(sf);
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                lsList.Add(sf);
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Far;
                lsList.Add(sf);
                sf.LineAlignment = StringAlignment.Far;
                sf.Alignment = StringAlignment.Near;
                lsList.Add(sf);
                sf.LineAlignment = StringAlignment.Far;
                sf.Alignment = StringAlignment.Center;
                lsList.Add(sf);
                sf.LineAlignment = StringAlignment.Far;
                sf.Alignment = StringAlignment.Far;
                lsList.Add(sf);
            }
            if (ClsInfopar.waterwz <= 9)
                g.DrawString(ClsInfopar.waterstr, myFont, bush, textRect, lsList[ClsInfopar.waterwz]);
            else if (ClsInfopar.waterwz == 10) {
                for (int i = 0; i < lsList.Count; i++) {
                    g.DrawString(ClsInfopar.waterstr, myFont, bush, textRect, lsList[i]);
                }
            }
            else if (ClsInfopar.waterid == 1) {
                for (int i = 0; i < lsList.Count; i++) {
                    if (i == 0 || i == 2 || i == 3 || i == 5) {
                        g.DrawString(ClsInfopar.waterstr, myFont, bush, textRect, lsList[i]);
                    }
                }
            }
            g.Save();
            rimg = RasterImageConverter.ConvertFromImage(bitmap, ConvertFromImageOptions.None);
            return rimg;
        }
        public void GetImgSize(out List<string> a0, out List<string> a1, out List<string> a2, out List<string> a3,
            out List<string> a4)
        {
            a0 = new List<string>();
            a1 = new List<string>();
            a2 = new List<string>();
            a3 = new List<string>();
            a4 = new List<string>();
            RasterImage _imagepx;
            RasterCodecs _Codef = new RasterCodecs();
            CodecsImageInfo info = _Codefile.GetInformation(Filename, true);
            for (int i = 1; i <= info.TotalPages; i++) {
                _imagepx = _Codef.Load(Filename, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, i, i);
                int num = _imagepx.ImageWidth;
                int num2 = _imagepx.ImageHeight;
                int num3 = num;
                if (num > num2) {
                    num = num2;
                    num2 = num3;
                }
                double w = (double)(num / 300) * 25.4;
                double h = (double)(num2 / 300) * 25.4;
                if (h >= 200 && h < 297) {
                    a4.Add(i.ToString());
                }
                else if (h >= 297 && h < 420) {
                    a3.Add(i.ToString());
                }
                else if (h >= 420 && h < 592) {
                    a2.Add(i.ToString());
                }
                else if (h >= 592 && h < 840) {
                    a1.Add(i.ToString());
                }
                else {
                    a0.Add(i.ToString());
                }
            }
            _Codef.Dispose();
        }

        public bool _SplitImg(string pathfile, int zlcf, out List<string> lsjpg)
        {
            try {
                lsjpg = new List<string>();
                RasterImage _imagepx;
                RasterCodecs _Codef = new RasterCodecs();
                CodecsImageInfo info = _Codefile.GetInformation(Filename, true);
                for (int i = 1; i <= info.TotalPages; i++) {
                    _imagepx = _Codef.Load(Filename, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, i, i);
                    int bit = _imagepx.BitsPerPixel;
                    string file = Path.Combine(pathfile, i.ToString().PadLeft(3, '0') + ".jpg");
                    lsjpg.Add(file);
                    if (File.Exists(file)) {
                        if (zlcf == 2) {
                            try {
                                File.Delete(file);
                            } catch { }
                        }
                        else
                            continue;
                    }
                    if (bit != 1) {
                        if (bit != 8) {
                            _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codef.Save(_imagepx, file, RasterImageFormat.TifJpeg, bit, 1, 1, i, CodecsSavePageMode.Append);
                        }
                        else {
                            _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codef.Save(_imagepx, file, RasterImageFormat.TifJpeg, 8, 1, 1, i, CodecsSavePageMode.Append);
                        }
                    }
                    else {
                        _Codef.Save(_imagepx, file, RasterImageFormat.CcittGroup4, 1, 1, 1, -1, CodecsSavePageMode.Append);
                    }
                }
                _Codef.Dispose();
                return true;
            } catch {
                lsjpg = null;
                return false;
            }
        }
        public bool _SplitImg(string pathfile, int zlcf, out List<string> lsjpg, out List<string> a0, out List<string> a1, out List<string> a2, out List<string> a3, out List<string> a4)
        {
            try {
                lsjpg = new List<string>();
                a0 = new List<string>();
                a1 = new List<string>();
                a2 = new List<string>();
                a3 = new List<string>();
                a4 = new List<string>();
                RasterImage _imagepx;
                RasterCodecs _Codef = new RasterCodecs();
                CodecsImageInfo info = _Codefile.GetInformation(Filename, true);
                for (int i = 1; i <= info.TotalPages; i++) {
                    _imagepx = _Codef.Load(Filename, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, i, i);
                    int bit = _imagepx.BitsPerPixel;
                    string file = Path.Combine(pathfile, i.ToString().PadLeft(3, '0') + ".jpg");
                    lsjpg.Add(file);
                    int num = _imagepx.ImageWidth;
                    int num2 = _imagepx.ImageHeight;
                    int num3 = num;
                    if (num > num2) {
                        num = num2;
                        num2 = num3;
                    }
                    double w = (double)(num / 300) * 25.4;
                    double h = (double)(num2 / 300) * 25.4;
                    if (h >= 200 && h < 297) {
                        a4.Add(i.ToString());
                    }
                    else if (h >= 297 && h < 420) {
                        a3.Add(i.ToString());
                    }
                    else if (h >= 420 && h < 592) {
                        a2.Add(i.ToString());
                    }
                    else if (h >= 592 && h < 840) {
                        a1.Add(i.ToString());
                    }
                    else {
                        a0.Add(i.ToString());
                    }
                    if (File.Exists(file)) {
                        if (zlcf == 2) {
                            try {
                                File.Delete(file);
                            } catch { }
                        }
                        else
                            continue;
                    }
                    if (bit != 1) {
                        if (bit != 8) {
                            _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codef.Save(_imagepx, file, RasterImageFormat.TifJpeg, bit, 1, 1, i, CodecsSavePageMode.Append);
                        }
                        else {
                            _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codef.Save(_imagepx, file, RasterImageFormat.TifJpeg, 8, 1, 1, i, CodecsSavePageMode.Append);
                        }
                    }
                    else {
                        _Codef.Save(_imagepx, file, RasterImageFormat.CcittGroup4, 1, 1, 1, -1, CodecsSavePageMode.Append);
                    }
                }
                _Codef.Dispose();
                return true;
            } catch {
                lsjpg = null;
                a0 = null;
                a1 = null;
                a2 = null;
                a3 = null;
                a4 = null;
                return false;
            }
        }
        /// <param name="yfile">源图像</param>
        /// <param name="mfile">新图像</param>
        /// <param name="p1">页码1</param>
        /// <param name="p2">页码2</param>
        /// <param name="fileformat">图像格式</param>
        public static bool _SplitImg(string yfile, string mfile, int p1, int p2, string fileformat)
        {
            try {
                RasterImageFormat format = new RasterImageFormat();
                if (fileformat.IndexOf("pdf") >= 0)
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
                            _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codef.Save(_imagepx, mfile, format, bit, 1, 1, i, CodecsSavePageMode.Append);

                        }
                        else {
                            _Codef.Options.Jpeg.Save.QualityFactor = Factor;
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
        //拆分图像
        public List<string> _SplitImgls(string yfile, string mfile, int p1, int p2, string fileformat)
        {
            List<string> str = new List<string>();

            try {
                RasterImageFormat format = new RasterImageFormat();
                if (fileformat.IndexOf("pdf") >= 0)
                    format = RasterImageFormat.RasPdfJpeg;
                else if (fileformat.IndexOf("tif") >= 0)
                    format = RasterImageFormat.TifJpeg;
                RasterImage _imagepx;
                RasterImage rimg = null;
                using (RasterCodecs codecs = new RasterCodecs()) {
                    if (ClsInfopar.waterid == 2)
                        rimg = codecs.Load(ClsInfopar.waterstr, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, 1, 1);
                }
                using (RasterCodecs _codecs = new RasterCodecs()) {
                    for (int i = p1; i <= p2; i++) {
                        _imagepx = _codecs.Load(yfile, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, i, i);
                        if (ClsInfopar.waterid == 2)
                            _imagepx = WaterImg(rimg, _imagepx).Clone();
                        else if (ClsInfopar.waterid == 1)
                            _imagepx = WaterImgtxt(_imagepx).Clone();
                        int bit = _imagepx.BitsPerPixel;
                        if (bit != 1) {
                            if (bit != 8) {
                                _codecs.Options.Jpeg.Save.QualityFactor = Factor;
                                _codecs.Save(_imagepx, mfile, format, 24, 1, 1, i, CodecsSavePageMode.Append);

                            }
                            else {
                                _codecs.Options.Jpeg.Save.QualityFactor = Factor;
                                _codecs.Save(_imagepx, mfile, format, 8, 1, 1, i, CodecsSavePageMode.Append);
                            }
                        }
                        else {
                            _codecs.Save(_imagepx, mfile, format, 1, 1, 1, -1, CodecsSavePageMode.Append);
                        }
                        str.Add(mfile);
                    }
                }

                return str;
            } catch (Exception ex) {
                str.Add("错误：" + ex);
                return str;
            }
        }
        //拆分图像
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
                        _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                        _Codef.Save(_imagepx, mfile, format, bit, 1, 1, -1, CodecsSavePageMode.Append);

                    }
                    else {
                        _Codef.Options.Jpeg.Save.QualityFactor = Factor;
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
        //拆分图像
        public string _SplitImg(string yfile, string mfile, string fileformat)
        {
            try {
                RasterImageFormat format = new RasterImageFormat();
                if (fileformat.IndexOf("pdf") >= 0)
                    format = RasterImageFormat.RasPdfJpeg;
                else if (fileformat.IndexOf("tif") >= 0)
                    format = RasterImageFormat.TifJpeg;
                else
                    return "生成图像格式不正确";
                RasterImage _imagepx;
                RasterImage rimg = null;
                if (ClsInfopar.waterid == 2) {
                    using (RasterCodecs codecs = new RasterCodecs()) {
                        rimg = codecs.Load(ClsInfopar.waterstr, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, 1, 1);
                    }
                }
                using (RasterCodecs _codecs = new RasterCodecs()) {
                    CodecsImageInfo info = _codecs.GetInformation(yfile, true);
                    for (int i = 1; i < info.TotalPages; i++) {
                        _imagepx = _codecs.Load(yfile, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, i, i);
                        if (ClsInfopar.waterid == 2)
                            _imagepx = WaterImg(rimg, _imagepx).Clone();
                        else if (ClsInfopar.waterid == 1)
                            _imagepx = WaterImgtxt(_imagepx).Clone();
                        int bit = _imagepx.BitsPerPixel;
                        if (bit != 1) {
                            if (bit != 8) {
                                _codecs.Options.Jpeg.Save.QualityFactor = Factor;
                                _codecs.Save(_imagepx, mfile, format, 24, 1, 1, i, CodecsSavePageMode.Append);
                            }
                            else {
                                _codecs.Options.Jpeg.Save.QualityFactor = Factor;
                                _codecs.Save(_imagepx, mfile, format, 8, 1, 1, i, CodecsSavePageMode.Append);
                            }
                        }
                        else {
                            _codecs.Save(_imagepx, mfile, format, 1, 1, 1, -1, CodecsSavePageMode.Append);
                        }
                    }
                }
                return "ok";
            } catch (Exception ex) {
                return "错误：" + ex;
            }
        }

        //拆分图像
        public string _SplitImg(string yimg, string mimg, int p1, int p2, string qzd, string hzd, int cd, int id, string fileformat, int qs)
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
                int num = 0;
                using (RasterCodecs _codecs = new RasterCodecs()) {
                    CodecsImageInfo info = _codecs.GetInformation(yimg, true);
                    if (p1 == 0 && p2 == 0) {
                        p1 = 1;
                        p2 = info.TotalPages;
                    }
                    for (int i = p1; i <= p2; i++) {
                        if (qs > 0)
                            num += 1;
                        else
                            num = i;
                        int zd = 0;
                        string Newfile = "";
                        if (cd > 0) {
                            zd = cd - qzd.Trim().Length - hzd.Trim().Length;
                            Newfile = Path.Combine(mimg, qzd.Trim() + num.ToString().PadLeft(zd, '0') + hzd.Trim() + "." + fileformat);
                        }
                        else {
                            Newfile = Path.Combine(mimg, qzd.Trim() + num.ToString() + hzd.Trim() + "." + fileformat);
                        }

                        if (id > 1) {
                            try {
                                if (File.Exists(Newfile)) {
                                    File.Delete(Newfile);
                                }
                            } catch { }
                        }
                        else if (id == 1) {
                            if (File.Exists(Newfile))
                                continue;
                        }
                        _imagepx = _codecs.Load(yimg, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, i, i);
                        int bit = _imagepx.BitsPerPixel;
                        if (bit != 1) {
                            if (bit != 8) {
                                _codecs.Options.Jpeg.Save.QualityFactor = Factor;
                                _codecs.Save(_imagepx, Newfile, format, bit, 1, 1, i, CodecsSavePageMode.Append);

                            }
                            else {
                                _codecs.Options.Jpeg.Save.QualityFactor = Factor;
                                _codecs.Save(_imagepx, Newfile, format, 8, 1, 1, i, CodecsSavePageMode.Append);
                            }
                        }
                        else {
                            _codecs.Save(_imagepx, Newfile, format, 1, 1, 1, -1, CodecsSavePageMode.Append);
                        }
                    }
                }
                return "ok";
            } catch (Exception ex) {
                return "错误：" + ex;
            }
        }
        //拆分图像
        public List<string> _SplitImgls(string yimg, string mimg, int p1, int p2, string qzd, string hzd, int cd, int id, string fileformat, int qs)
        {
            List<string> str = new List<string>();
            RasterImageFormat format = new RasterImageFormat();
            if (fileformat.IndexOf("jpg") >= 0)
                format = RasterImageFormat.Jpeg;
            else if (fileformat.IndexOf("pdf") >= 0)
                format = RasterImageFormat.RasPdfJpeg;
            else if (fileformat.IndexOf("tif") >= 0)
                format = RasterImageFormat.TifJpeg;
            RasterImage _imagepx;
            RasterImage rimg = null;
            int num = 0;
            try {
                if (ClsInfopar.waterid == 2) {
                    using (RasterCodecs codecs = new RasterCodecs()) {
                        rimg = codecs.Load(ClsInfopar.waterstr, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, 1, 1);
                    }
                }
                using (RasterCodecs _codecs = new RasterCodecs()) {
                    CodecsImageInfo info = _codecs.GetInformation(yimg, true);
                    if (p1 == 0 && p2 == 0) {
                        p1 = 1;
                        p2 = info.TotalPages;
                    }
                    for (int i = p1; i <= p2; i++) {
                        if (qs > 0)
                            num += 1;
                        else
                            num = i;
                        int zd = 0;
                        string Newfile = "";
                        if (cd > 0) {
                            zd = cd - qzd.Trim().Length - hzd.Trim().Length;
                            Newfile = Path.Combine(mimg, qzd.Trim() + num.ToString().PadLeft(zd, '0') + hzd.Trim() + "." + fileformat);
                        }
                        else {
                            Newfile = Path.Combine(mimg, qzd.Trim() + num.ToString() + hzd.Trim() + "." + fileformat);
                        }
                        if (id > 1) {
                            try {
                                if (File.Exists(Newfile)) {
                                    File.Delete(Newfile);
                                }
                            } catch { }
                        }
                        else if (id == 1) {
                            if (File.Exists(Newfile))
                                continue;
                        }
                        _imagepx = _codecs.Load(yimg, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, i, i);
                        if (ClsInfopar.waterid == 2)
                            _imagepx = WaterImg(rimg, _imagepx).Clone();
                        else if (ClsInfopar.waterid == 1)
                            _imagepx = WaterImgtxt(_imagepx).Clone();

                        int bit = _imagepx.BitsPerPixel;
                        if (bit != 1) {
                            if (bit != 8) {
                                _codecs.Options.Jpeg.Save.QualityFactor = Factor;
                                _codecs.Save(_imagepx, Newfile, format, 24, 1, 1, i, CodecsSavePageMode.Append);

                            }
                            else {
                                _codecs.Options.Jpeg.Save.QualityFactor = Factor;
                                _codecs.Save(_imagepx, Newfile, format, 8, 1, 1, i, CodecsSavePageMode.Append);
                            }
                        }
                        else {
                            _codecs.Save(_imagepx, Newfile, format, 1, 1, 1, -1, CodecsSavePageMode.Append);
                        }
                        str.Add(Newfile);
                    }

                }
                return str;
            } catch (Exception ex) {
                str.Add("错误：" + ex.ToString());
                return str;
            }
        }

        private void MySetCapability(TwainCapabilityType capType, TwainItemType itemType, object data)
        {
            using (TwainCapability twainCapability = new TwainCapability()) {
                twainCapability.Information.Type = capType;
                twainCapability.Information.ContainerType = TwainContainerType.OneValue;
                twainCapability.OneValueCapability.ItemType = itemType;
                twainCapability.OneValueCapability.Value = data;
                _twains.SetCapability(twainCapability, TwainSetCapabilityMode.Set);
            }
        }
        //图像方向
        public void _SetimgFx(int ORD, int PaperType)
        {
            try {
                TwainFrame twainFrame = default(TwainFrame);
                if (PaperType == 1) {
                    twainFrame.LeftMargin = (float)Convert.ToDouble(0);
                    twainFrame.TopMargin = (float)Convert.ToDouble(0);
                    twainFrame.RightMargin = (float)Convert.ToDouble(11.69);
                    twainFrame.BottomMargin = (float)Convert.ToDouble(16.53);
                    this.MySetCapability(TwainCapabilityType.ImageFrames, TwainItemType.Frame, twainFrame);
                }
                else {
                    twainFrame.LeftMargin = (float)Convert.ToDouble(0);
                    twainFrame.TopMargin = (float)Convert.ToDouble(0);
                    switch (ORD) {
                        case 0: //横
                            twainFrame.RightMargin = (float)Convert.ToDouble(11.69);
                            twainFrame.BottomMargin = (float)Convert.ToDouble(8.26);
                            break;
                        case 1:     //纵                     
                            twainFrame.RightMargin = (float)Convert.ToDouble(8.26);
                            twainFrame.BottomMargin = (float)Convert.ToDouble(11.69);
                            break;

                        case 2:
                            twainFrame.RightMargin = (float)Convert.ToDouble(7.27);
                            twainFrame.BottomMargin = (float)Convert.ToDouble(10.42);
                            break;
                        case 3:
                            twainFrame.RightMargin = (float)Convert.ToDouble(10.42);
                            twainFrame.BottomMargin = (float)Convert.ToDouble(7.27);
                            break;
                    }
                    this.MySetCapability(TwainCapabilityType.ImageFrames, TwainItemType.Frame, twainFrame);
                }
            } catch { }
        }


        //拼接
        public void _ImgPj(int page, string Sourimg, string _path)
        {
            try {
                RasterImage _imagepx;
                RasterCodecs _Codef = new RasterCodecs();
                _imagepx = _Codef.Load(Sourimg, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, page, page);
                // AutoCropCommand cmdauto = new AutoCropCommand();
                //   cmdauto.Threshold = 64;
                // cmdauto.Run(_imagepx);
                int bit = _imagepx.BitsPerPixel;
                if (bit != 1) {
                    if (bit != 8) {
                        _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                        _Codef.Save(_imagepx, _path, RasterImageFormat.TifJpeg, bit, 1, 1, 1, CodecsSavePageMode.Append);
                    }
                    else {
                        _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                        _Codef.Save(_imagepx, _path, RasterImageFormat.TifJpeg, 8, 1, 1, 1, CodecsSavePageMode.Append);
                    }
                }
                else {
                    _Codef.Save(_imagepx, _path, RasterImageFormat.CcittGroup4, 1, 1, 1, 1, CodecsSavePageMode.Append);
                }
                _imagepx.Dispose();
                _Codef.Dispose();
            } catch (Exception ex) {
                throw ex;
            }
        }


        //拼接替换
        public void _ImgPjRep(int page, string Sourimg, string _path)
        {
            try {
                RasterImage _imagepx;
                RasterCodecs _Codef = new RasterCodecs();
                _imagepx = _Codef.Load(Sourimg, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, page, page);
                int bit = _imagepx.BitsPerPixel;
                if (bit != 1) {
                    if (bit != 8) {
                        _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                        _Codef.Save(_imagepx, _path, RasterImageFormat.TifJpeg, bit, 1, 1, page, CodecsSavePageMode.Replace);
                    }
                    else {
                        _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                        _Codef.Save(_imagepx, _path, RasterImageFormat.TifJpeg, 8, 1, 1, page, CodecsSavePageMode.Replace);
                    }
                }
                else {
                    _Codef.Save(_imagepx, _path, RasterImageFormat.CcittGroup4, 1, 1, 1, page, CodecsSavePageMode.Replace);
                }
                _imagepx.Dispose();
                _Codef.Dispose();

            } catch (Exception ex) {
                throw ex;
            }
        }



        //排序
        public void _OrderSave(string _path)
        {
            int pagecoun = 0;
            try {
                if (_PageAbc.Count >= 1) {
                    int zimu_a = (int)Convert.ToByte('a');
                    int oldpage = 97;
                    for (int abc = 1; abc <= _PageAbc.Count; abc++) {
                        string zimu = (Convert.ToChar(oldpage)).ToString();
                        var abckeys = _PageAbc.Where(q => q.Value == zimu).Select(q => q.Key);
                        foreach (var a in abckeys) {
                            pagecoun += 1;
                            OrderSave(a, pagecoun, _path);
                        }
                        oldpage = zimu_a + abc;
                    }
                }
                if (_PageNumber.Count >= 1) {
                    int fuhao = _PageFuhao.Count;
                    for (int i = 1; i <= RegPage - _PageAbc.Count - fuhao; i++) {
                        int k = _PageNumber.First(q => q.Value == i).Key;
                        if (_PageNumber[k].Equals(i)) {
                            pagecoun += 1;
                            OrderSave(k, pagecoun, _path);
                            if (_PageFuhao.Count > 0) {
                                for (int t = 0; t < fuhao; t++) {
                                    string str = i + "-" + (t + 1);
                                    int oldpage = 0;
                                    try {
                                        oldpage = _PageFuhao.First(q => q.Value == str).Key;
                                    } catch { }
                                    if (oldpage > 0) {
                                        pagecoun += 1;
                                        OrderSave(oldpage, pagecoun, _path);
                                        _PageFuhao.Remove(oldpage);
                                    }
                                }
                            }
                        }
                    }
                }

            } catch {

            }
        }
        //排序
        public bool _OrderSave(string oldfile, string _path)
        {
            try {

                if (_PageAbc.Count >= 1) {
                    int zimu_a = (int)Convert.ToByte('a');
                    int oldpage = 97;
                    for (int abc = 1; abc <= _PageAbc.Count; abc++) {
                        string zimu = (Convert.ToChar(oldpage)).ToString();
                        var abckeys = _PageAbc.Where(q => q.Value == zimu).Select(q => q.Key);

                        foreach (var a in abckeys) {
                            OrderSave(a, abc, oldfile, _path);
                        }
                        oldpage = zimu_a + abc;
                    }
                }
                if (_PageNumber.Count >= 1) {
                    for (int i = 1; i <= RegPage - _PageAbc.Count; i++) {
                        int k = _PageNumber.First(q => q.Value == i).Key;
                        if (_PageNumber[k].Equals(i)) {

                            OrderSave(k, _PageAbc.Count + i, oldfile, _path);

                        }
                    }
                }
                return true;

            } catch {
                return false;
            }
        }
        //排序
        public bool _OrderSave(int tagpage, int regpage, string oldfile, string _path, Dictionary<int, string> Pabc, Dictionary<int, int> Pnumber, Dictionary<int, string> fuhao)
        {
            int pagecoun = 0;
            try {
                if (Pabc.Count >= 1) {
                    int zimu_a = (int)Convert.ToByte('a');
                    int oldpage = 97;
                    for (int abc = 1; abc <= Pabc.Count; abc++) {
                        string zimu = (Convert.ToChar(oldpage)).ToString();
                        var abckeys = Pabc.Where(q => q.Value == zimu).Select(q => q.Key);
                        foreach (var a in abckeys) {
                            pagecoun += 1;
                            OrderSave(a, pagecoun, oldfile, _path);
                        }
                        oldpage = zimu_a + abc;
                    }
                }
                if (tagpage == 0) {
                    if (Pnumber.Count >= 1) {
                        int fh = fuhao.Count;
                        for (int i = 1; i <= regpage - Pabc.Count - fh; i++) {
                            int k = Pnumber.First(q => q.Value == i).Key;
                            if (Pnumber[k].Equals(i)) {
                                pagecoun += 1;
                                OrderSave(k, pagecoun, oldfile, _path);
                                if (fuhao.Count > 0) {
                                    for (int t = 0; t < fh; t++) {
                                        string str = i + "-" + (t + 1);
                                        int oldpage = 0;
                                        try {
                                            oldpage = fuhao.First(q => q.Value == str).Key;
                                        } catch { }
                                        if (oldpage > 0) {
                                            pagecoun += 1;
                                            OrderSave(oldpage, pagecoun, oldfile, _path);
                                            fuhao.Remove(oldpage);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return true;
                }
                else {
                    if (Pnumber.Count >= 1) {
                        int fh = fuhao.Count;
                        for (int i = tagpage; i <= regpage - Pabc.Count - fh + tagpage - 1; i++) {
                            int k = Pnumber.First(q => q.Value == i).Key;
                            if (Pnumber[k].Equals(i)) {
                                pagecoun += 1;
                                OrderSave(k, pagecoun, oldfile, _path);
                                if (fuhao.Count > 0) {
                                    for (int t = 0; t < fh; t++) {
                                        string str = i + "-" + (t + 1);
                                        int oldpage = 0;
                                        try {
                                            oldpage = fuhao.First(q => q.Value == str).Key;
                                        } catch { }
                                        if (oldpage > 0) {
                                            pagecoun += 1;
                                            OrderSave(oldpage, pagecoun, oldfile, _path);
                                            fuhao.Remove(oldpage);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return true;
                }


            } catch {
                return false;
            }
        }


        //排序生成文件
        private void OrderSave(int k, int i, string _path)
        {
            try {
                RasterImage _imagepx;
                RasterCodecs _Codef = new RasterCodecs();
                _imagepx = _Codef.Load(Filename, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, k, k);
                int bit = _imagepx.BitsPerPixel;
                if (bit != 1) {
                    if (bit != 8) {
                        _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                        _Codef.Save(_imagepx, _path, RasterImageFormat.TifJpeg, bit, 1, 1, i, CodecsSavePageMode.Append);
                    }
                    else {
                        _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                        _Codef.Save(_imagepx, _path, RasterImageFormat.TifJpeg, 8, 1, 1, i, CodecsSavePageMode.Append);
                    }
                }
                else {
                    _Codef.Save(_imagepx, _path, RasterImageFormat.CcittGroup4, 1, 1, 1, -1, CodecsSavePageMode.Append);
                }
            } catch { }
        }
        //排序生成文件
        private void OrderSave(int k, int i, string oldfile, string _path)
        {
            try {
                RasterImage _imagepx;
                RasterCodecs _Codef = new RasterCodecs();
                _imagepx = _Codef.Load(oldfile, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, k, k);
                int bit = _imagepx.BitsPerPixel;
                if (bit != 1) {
                    if (bit != 8) {
                        _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                        _Codef.Save(_imagepx, _path, RasterImageFormat.TifJpeg, 24, 1, 1, i, CodecsSavePageMode.Append);
                    }
                    else {
                        _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                        _Codef.Save(_imagepx, _path, RasterImageFormat.TifJpeg, 8, 1, 1, i, CodecsSavePageMode.Append);
                    }
                }
                else {
                    _Codef.Save(_imagepx, _path, RasterImageFormat.CcittGroup4, 1, 1, 1, -1, CodecsSavePageMode.Append);
                }
            } catch { }
        }

        public void jpgTotif(List<string> jpg, string tif)
        {
            try {
                RasterImage _imagepx;
                RasterCodecs _Codef = new RasterCodecs();
                for (int i = 0; i < jpg.Count; i++) {
                    string oldfile = jpg[i];
                    _imagepx = _Codef.Load(oldfile, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, 1, 1);
                    int bit = _imagepx.BitsPerPixel;
                    if (bit != 1) {
                        if (bit != 8) {
                            _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codef.Save(_imagepx, tif, RasterImageFormat.TifJpeg, bit, 1, 1, i, CodecsSavePageMode.Append);
                        }
                        else {
                            _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codef.Save(_imagepx, tif, RasterImageFormat.TifJpeg, 8, 1, 1, i, CodecsSavePageMode.Append);
                        }
                    }
                    else {
                        _Codef.Save(_imagepx, tif, RasterImageFormat.CcittGroup4, 1, 1, 1, -1, CodecsSavePageMode.Append);
                    }
                }

            } catch { }
        }


        //总页码
        public int _CountPage()
        {
            int result;
            if (Filename.Trim().Length <= 0) {
                result = 0;
            }
            else if (!File.Exists(Filename)) {
                result = 0;
            }
            else {
                CodecsImageInfo information = _Codefile.GetInformation(Filename, true);
                int totalPages = information.TotalPages;
                result = totalPages;
                Setpage(CrrentPage, totalPages);
            }
            return result;
        }


        //扫描保存
        private void Scanepage(RasterImage scanimg)
        {
            try {
                int bit = scanimg.BitsPerPixel;
                int _CurrectPage = scanimg.Page;
                if (bit != 1) {
                    if (bit != 8) {
                        _Codefile.Options.Jpeg.Save.QualityFactor = Factor;
                        if (Scanms == 0)
                            _Codefile.Save(scanimg, Filename, RasterImageFormat.TifJpeg, 24, 1, 1, _CurrectPage, CodecsSavePageMode.Append);
                        if (Scanms == 1)
                            _Codefile.Save(scanimg, Filename, RasterImageFormat.TifJpeg, 24, 1, 1, _CurrectPage, CodecsSavePageMode.Insert);
                        if (Scanms == 2)
                            _Codefile.Save(scanimg, Filename, RasterImageFormat.TifJpeg, 24, 1, 1, _CurrectPage, CodecsSavePageMode.Replace);

                    }
                    else {
                        _Codefile.Options.Jpeg.Save.QualityFactor = Factor;
                        if (Scanms == 0)
                            _Codefile.Save(scanimg, Filename, RasterImageFormat.TifJpeg, 8, 1, 1, _CurrectPage, CodecsSavePageMode.Append);
                        if (Scanms == 1)
                            _Codefile.Save(scanimg, Filename, RasterImageFormat.TifJpeg, 8, 1, 1, _CurrectPage, CodecsSavePageMode.Insert);
                        if (Scanms == 2)
                            _Codefile.Save(scanimg, Filename, RasterImageFormat.TifJpeg, 8, 1, 1, _CurrectPage, CodecsSavePageMode.Replace);
                    }
                }
                else {
                    if (Scanms == 0)
                        _Codefile.Save(scanimg, Filename, RasterImageFormat.CcittGroup4, 1, 1, 1, _CurrectPage, CodecsSavePageMode.Append);
                    if (Scanms == 1)
                        _Codefile.Save(scanimg, Filename, RasterImageFormat.CcittGroup4, 8, 1, 1, _CurrectPage, CodecsSavePageMode.Append);
                    if (Scanms == 2)
                        _Codefile.Save(scanimg, Filename, RasterImageFormat.CcittGroup4, 8, 1, 1, _CurrectPage, CodecsSavePageMode.Append);

                }

            } catch (Exception ex) {
                throw ex;
            }
        }

        //获取文件总页码
        public int GetFilePage(string str)
        {
            int pags = 0;
            try {
                if (!File.Exists(str))
                    return pags;
                using (RasterCodecs _code = new RasterCodecs()) {
                    CodecsImageInfo info = _code.GetInformation(str, true);
                    pags = info.TotalPages;
                    return pags;
                }
            } catch {
                return pags;
            }

        }

        //删除页码
        public void _Delepage()
        {
            try {
                if (_Imageview.Image == null || Filename.Trim().Length <= 0)
                    return;
                if (CrrentPage >= 2) {
                    _Codefile.DeletePage(Filename, CrrentPage);
                    LoadPage(CrrentPage - 1);
                    return;
                }
                else {
                    int x = _CountPage();
                    if (x >= 2) {
                        _Codefile.DeletePage(Filename, CrrentPage);
                        LoadPage(x - 1);
                        return;
                    }
                }
                _Imageview.Image = null;
                File.Delete(Filename);
                CrrentPage = 0;
                CountPage = 0;
                Setpage(CrrentPage, CountPage);

            } catch (Exception ex) {
                MessageBox.Show("错误:" + ex.ToString());
            }
        }

        //插入空白
        public void InterWhiteImg()
        {
            try {
                if (Filename != null) {
                    RasterImage img = WhiteImg();
                    int bit = img.BitsPerPixel;
                    int page = CountPage + 1;
                    if (bit != 1) {
                        if (bit != 8) {
                            _Codefile.Options.Jpeg2000.Save.CompressionRatio = (float)Factor;
                            _Codefile.Options.Ecw.Save.QualityFactor = Factor;
                            _Codefile.Save(img, Filename, RasterImageFormat.TifJpeg, bit, 1, 1, page, CodecsSavePageMode.Insert);
                        }
                        else {
                            _Codefile.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codefile.Save(img, Filename, RasterImageFormat.TifJpeg, bit, 1, 1, page, CodecsSavePageMode.Insert);
                        }
                    }
                    else {
                        _Codefile.Save(img, Filename, RasterImageFormat.CcittGroup4, bit, 1, 1, page, CodecsSavePageMode.Insert);
                    }
                }
                //Setpage(CrrentPage, _CountPage());
            } catch (Exception ex) {
                throw ex;
            }
        }

        //插入图像
        public void _Insterpage(string instimg)
        {
            try {
                if (_Imageview.Image != null) {
                    RasterImage Instimg = _Codefile.Load(instimg, 0, CodecsLoadByteOrder.BgrOrGray, 1, 1);
                    int bit = Instimg.BitsPerPixel;
                    int page = CrrentPage;
                    if (bit != 1) {
                        if (bit != 8) {
                            _Codefile.Options.Jpeg2000.Save.CompressionRatio = (float)Factor;
                            _Codefile.Options.Ecw.Save.QualityFactor = Factor;
                            _Codefile.Save(Instimg, Filename, RasterImageFormat.TifJpeg, bit, 1, 1, page, CodecsSavePageMode.Insert);
                        }
                        else {
                            _Codefile.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codefile.Save(Instimg, Filename, RasterImageFormat.TifJpeg, bit, 1, 1, page, CodecsSavePageMode.Insert);
                        }
                    }
                    else {
                        _Codefile.Save(Instimg, Filename, RasterImageFormat.CcittGroup4, bit, 1, 1, page, CodecsSavePageMode.Insert);
                    }
                    _Gotopage(page);
                    // CountPage += 1; 
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        public void _Insterpage(Image bmp, int page)
        {
            try {
                if (_Imageview.Image != null) {
                    Bitmap bitmap = new Bitmap(bmp);
                    RasterImage rimg = RasterImageConverter.ConvertFromImage(bitmap, ConvertFromImageOptions.None);
                    int bit = rimg.BitsPerPixel;
                    if (bit != 1) {
                        if (bit != 8) {
                            _Codefile.Options.Jpeg2000.Save.CompressionRatio = (float)Factor;
                            _Codefile.Options.Ecw.Save.QualityFactor = Factor;
                            _Codefile.Save(rimg, Filename, RasterImageFormat.TifJpeg, 24, 1, 1, page, CodecsSavePageMode.Insert);
                        }
                        else {
                            _Codefile.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codefile.Save(rimg, Filename, RasterImageFormat.TifJpeg, 8, 1, 1, page, CodecsSavePageMode.Insert);
                        }
                    }
                    else {
                        _Codefile.Save(rimg, Filename, RasterImageFormat.CcittGroup4, 1, 1, 1, page, CodecsSavePageMode.Insert);
                    }
                }

            } catch { }
        }

        public bool _CopyImg()
        {
            if (_Imageview.Image == null)
                return false;
            try {
                RasterImage img = _Imageview.Image.Clone();
                int bit = img.BitsPerPixel;
                int page = CrrentPage;
                if (bit != 1) {
                    if (bit != 8) {
                        _Codefile.Options.Jpeg2000.Save.CompressionRatio = (float)Factor;
                        _Codefile.Options.Ecw.Save.QualityFactor = Factor;
                        _Codefile.Save(img, Filename, RasterImageFormat.TifJpeg, bit, 1, 1, page + 1, CodecsSavePageMode.Insert);
                    }
                    else {
                        _Codefile.Options.Jpeg.Save.QualityFactor = Factor;
                        _Codefile.Save(img, Filename, RasterImageFormat.TifJpeg, bit, 1, 1, page + 1, CodecsSavePageMode.Insert);
                    }
                }
                else {
                    _Codefile.Save(img, Filename, RasterImageFormat.CcittGroup4, bit, 1, 1, page + 1, CodecsSavePageMode.Insert);
                }
                return true;
            } catch {
                return false;
            }

        }

        // 转跳指定页码
        public void _Gotopage(int x)
        {
            try {
                if (_Imageview.Image == null && Filename.Trim().Length <= 0)
                    return;
                LoadPage(x);
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }


        //创建空白图像
        public RasterImage WhiteImg()
        {
            RasterImage result;

            try {
                int w = 1000;
                int h = 1000;
                int bitsPerPixel = 1;
                FillCommand fillCmd = new FillCommand(RasterColor.FromKnownColor(RasterKnownColor.White));
                RasterByteOrder order = (bitsPerPixel == 1) ? RasterByteOrder.Rgb : RasterByteOrder.Bgr;
                RasterImage pageImage = new RasterImage(
                   RasterMemoryFlags.Conventional,
                   w,
                   h,
                   bitsPerPixel,
                   order,
                   RasterViewPerspective.TopLeft,
                   null,
                   IntPtr.Zero,
                   0);
                result = pageImage;
                return pageImage;
            } catch {
                result = null;
            }
            return result;
        }

        //转换为pdf
        public string Autopdf(string yfile, string mfile)
        {
            string str = "ok";
            try {
                RasterImage rimg;
                using (RasterCodecs codefile = new RasterCodecs()) {
                    rimg = codefile.Load(yfile, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, 1, 1);
                    codefile.Save(rimg, mfile, RasterImageFormat.RasPdfJpeg, rimg.BitsPerPixel, 1, 1, -1, CodecsSavePageMode.Append);
                }
                return str;
            } catch (Exception e) {
                str = "错误 :" + e.ToString();
                return str;
            }
        }

        //转换pdf
        public bool _Autopdf(string openfile, string pathfile, List<string> strinser)
        {
            try {
                int bit = 0;
                RasterImage _imagepx;
                RasterCodecs _Codef = new RasterCodecs();
                CodecsImageInfo info = _Codefile.GetInformation(openfile, true);
                int _bit = info.BitsPerPixel;
                int _allpage = info.TotalPages;
                for (int i = 1; i <= _allpage; i++) {
                    _imagepx = _Codef.Load(openfile, 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, i, i);
                    bit = _imagepx.BitsPerPixel;
                    if (bit != 1) {
                        if (bit != 8) {
                            _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codef.Save(_imagepx, pathfile, RasterImageFormat.RasPdfJpeg, bit, 1, 1, -1, CodecsSavePageMode.Append);

                        }
                        else {
                            _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                            _Codef.Save(_imagepx, pathfile, RasterImageFormat.RasPdfJpeg, 8, 1, 1, -1, CodecsSavePageMode.Append);
                        }
                    }
                    else {
                        _Codef.Save(_imagepx, pathfile, RasterImageFormat.RasPdfJpeg, 1, 1, 1, -1, CodecsSavePageMode.Append);
                    }
                }
                for (int t = 0; t < strinser.Count; t++) {
                    info = _Codefile.GetInformation(strinser[t], true);
                    _allpage = info.TotalPages;
                    for (int i = _allpage; i >= 1; i--) {
                        _imagepx = _Codef.Load(strinser[t], 0, CodecsLoadByteOrder.BgrOrGrayOrRomm, i, i);
                        bit = _imagepx.BitsPerPixel;
                        int wz = 1;
                        if (t == 2) {
                            wz = -1;
                        }
                        _Codef.Options.Jpeg.Save.QualityFactor = Factor;
                        _Codef.Save(_imagepx, pathfile, RasterImageFormat.RasPdfJpeg, bit, 1, 1, wz, CodecsSavePageMode.Insert);
                    }
                    try {
                        if (File.Exists(strinser[t]) == true) {
                            File.Delete(strinser[t]);
                        }
                    } catch { }
                }
                _Codef.Dispose();
                return true;
            } catch {
                return false;
            }
        }

        //转换双层pdf
        private bool _AutoPdfAD(string openfile, string pathfile)
        {
            try {
                if (File.Exists(pathfile) == true) {
                    File.Delete(pathfile);
                }
                var format = DocumentFormat.Pdf;
                PdfDocumentOptions pdfOptions = ocrEngine.DocumentWriterInstance.GetOptions(DocumentFormat.Pdf) as PdfDocumentOptions;
                pdfOptions.ImageOverText = true;
                ocrEngine.DocumentWriterInstance.SetOptions(DocumentFormat.Pdf, pdfOptions);

                documentConverter.SetOcrEngineInstance(ocrEngine, true);
                //错误时继续
                documentConverter.Options.JobErrorMode = DocumentConverterJobErrorMode.Continue;
                //配置基本数据
                var jobData = new DocumentConverterJobData();
                jobData.InputDocumentFileName = openfile;
                jobData.JobName = "myjob";
                jobData.InputDocumentFirstPageNumber = 1;
                jobData.InputDocumentLastPageNumber = -1;
                jobData.DocumentFormat = format;
                jobData.RasterImageBitsPerPixel = 24;
                jobData.OutputDocumentFileName = pathfile;
                jobData.UserData = null;
                var job = documentConverter.Jobs.CreateJob(jobData);
                documentConverter.Jobs.RunJob(job);
                if (File.Exists(pathfile) == true) {
                    return true;
                }
                else {
                    return false;
                }
            } catch {
                return false;
            } finally {
                _CloseOcr();
                _StartOcr(0);
            }
        }
        //启动ocr
        public bool _StartOcr(int x)
        {
            try {
                if (ocrEngine == null || ocrEngine.IsStarted == false) {
                    string pathtmp = "c:\\temp\\otmp";
                    if (!Directory.Exists(pathtmp)) {
                        Directory.CreateDirectory(pathtmp);
                    }
                    else {
                        Directory.Delete(pathtmp, true);
                        Directory.CreateDirectory(pathtmp);
                    }
                    string Ocrpath = "";
                    if (x == 0) {
                        Ocrpath = Path.Combine(@"c:\\temp", "OcrPro");
                        ocrEngine = OcrEngineManager.CreateEngine(OcrEngineType.Professional, false);
                        ocrEngine.Startup(null, null, pathtmp, Ocrpath);
                    }
                    else if (x == 1) {
                        Ocrpath = Path.Combine(@"c:\\temp", "OcrAdv");
                        ocrEngine = OcrEngineManager.CreateEngine(OcrEngineType.Advantage, false);
                        ocrEngine.Startup(null, null, pathtmp, Ocrpath);
                    }
                    ocrEngine.LanguageManager.EnableLanguages(new string[] { "zh-Hans" });
                }

                return true;
            } catch {
                return false;
            }
        }

        //判断ocr
        public void _CloseOcr()
        {
            try {
                ocrEngine.Shutdown();
                ocrEngine.Dispose();
                documentConverter.Dispose();
            } catch { }
        }

        //转双层pdf
        [HandleProcessCorruptedStateExceptions]
        public bool _AutoPdfocr(string openfile, string pathfile)
        {
            try {
                var format = DocumentFormat.Pdf;
                PdfDocumentOptions pdfOptions = ocrEngine.DocumentWriterInstance.GetOptions(DocumentFormat.Pdf) as PdfDocumentOptions;
                pdfOptions.ImageOverText = true;
                ocrEngine.DocumentWriterInstance.SetOptions(DocumentFormat.Pdf, pdfOptions);

                documentConverter.SetOcrEngineInstance(ocrEngine, true);
                //错误时继续
                documentConverter.Options.JobErrorMode = DocumentConverterJobErrorMode.Continue;
                //配置基本数据
                var jobData = new DocumentConverterJobData();
                jobData.InputDocumentFileName = openfile;
                jobData.JobName = "myjob";
                jobData.InputDocumentFirstPageNumber = 1;
                jobData.InputDocumentLastPageNumber = -1;
                jobData.DocumentFormat = format;
                jobData.RasterImageBitsPerPixel = 24;
                jobData.OutputDocumentFileName = pathfile;
                jobData.UserData = null;
                var job = documentConverter.Jobs.CreateJob(jobData);
                documentConverter.Jobs.RunJob(job);
                if (File.Exists(pathfile) == true) {
                    return true;
                }
                else {
                    _CloseOcr();
                    _StartOcr(1);
                    return _AutoPdfAD(openfile, pathfile);
                }

            } catch {
                _CloseOcr();
                _StartOcr(1);
                return _AutoPdfAD(openfile, pathfile);
            }
        }
        //转双层pdf
        public string _AutoPdfOcr2(string Yimg, string Mimg, string path, int id)
        {
            var format = DocumentFormat.Pdf;
            IOcrEngine ocrEng = null;
            string pathtmp = "c:\\temp\\otmp";
            string Ocrpath = "";
            try {
                if (!Directory.Exists(pathtmp)) {
                    Directory.CreateDirectory(pathtmp);
                }
                if (id == 1) {
                    Ocrpath = Path.Combine(path, "OcrPro");
                    ocrEng = OcrEngineManager.CreateEngine(OcrEngineType.Professional, false);
                    ocrEng.Startup(null, null, pathtmp, Ocrpath);
                }
                else if (id == 2) {
                    Ocrpath = Path.Combine(path, "OcrAdv");
                    ocrEng = OcrEngineManager.CreateEngine(OcrEngineType.Advantage, false);
                    ocrEng.Startup(null, null, pathtmp, Ocrpath);
                }
                ocrEng.LanguageManager.EnableLanguages(new string[] { "zh-Hans" });

                PdfDocumentOptions pdfOptions = ocrEng.DocumentWriterInstance.GetOptions(DocumentFormat.Pdf) as PdfDocumentOptions;
                pdfOptions.ImageOverText = true;
                ocrEng.DocumentWriterInstance.SetOptions(DocumentFormat.Pdf, pdfOptions);
                using (DocumentConverter documentConve = new DocumentConverter()) {

                    documentConve.SetOcrEngineInstance(ocrEng, true);
                    //错误时继续
                    documentConve.Options.JobErrorMode = DocumentConverterJobErrorMode.Continue;
                    //配置基本数据
                    var jobData = new DocumentConverterJobData();
                    jobData.InputDocumentFileName = Yimg;
                    jobData.JobName = "myjob";
                    jobData.InputDocumentFirstPageNumber = 1;
                    jobData.InputDocumentLastPageNumber = -1;
                    jobData.DocumentFormat = format;
                    jobData.RasterImageBitsPerPixel = 24;
                    jobData.OutputDocumentFileName = Mimg;
                    jobData.UserData = null;
                    var job = documentConve.Jobs.CreateJob(jobData);
                    documentConve.Jobs.RunJob(job);
                }
                if (File.Exists(Mimg)) {
                    return "ok";
                }
                ocrEng.Shutdown();
                ocrEng.Dispose();
                return "ok";

            } catch (Exception ex) {
                try {
                    ocrEng.Shutdown();
                    ocrEng.Dispose();
                } catch { }
                return "错误：" + ex.ToString();
            }
        }


        //ocr识别
        public string _OcrRecttxt()
        {
            try {
                string txt = "";
                ocrPage = ocrEngine.CreatePage(_Imageview.Image, OcrImageSharingMode.None);
                if (ocrPage == null) {
                    return "";
                }
                ocrPage.Zones.Clear();
                LeadRect rcw = _Imageview.Image.GetRegionBounds(null);
                OcrZone zone = new OcrZone();
                zone.Bounds = LogicalRectangle.FromRectangle(rcw);
                zone.ZoneType = OcrZoneType.Text;
                zone.CharacterFilters = OcrZoneCharacterFilters.None;
                ocrPage.Zones.Add(zone);
                ocrPage.Recognize(null);
                txt = ocrPage.GetText(0);
                _Imageview.Image.MakeRegionEmpty();
                return txt;
            } catch {
                return "";
            }
        }
        public void _OcrRecttxt(string file, out List<string> str, out List<string> pages, int id)
        {
            str = new List<string>();
            pages = new List<string>();
            try {
                //IOcrEngine ocrEng = null;
                //string pathtmp = "c:\\temp\\otmp";
                //  string Ocrpath = "";

                //if (!Directory.Exists(pathtmp)) {
                //    Directory.CreateDirectory(pathtmp);
                //}
                ////if (id == 1) {
                //    Ocrpath = Path.Combine(@"c:\temp", "OcrPro");
                //    ocrEng = OcrEngineManager.CreateEngine(OcrEngineType.Professional, false);
                //    ocrEng.Startup(null, null, pathtmp, Ocrpath);
                ////}
                ////else if (id == 2) {
                ////    Ocrpath = Path.Combine(path, "OcrAdv");
                ////    ocrEng = OcrEngineManager.CreateEngine(OcrEngineType.Advantage, false);
                ////    ocrEng.Startup(null, null, pathtmp, Ocrpath);
                ////}
                //ocrEng.LanguageManager.EnableLanguages(new string[] { "zh-Hans" });
                _StartOcr(id);
                RasterImage _imagepx;
                RasterCodecs _Codef = new RasterCodecs();
                CodecsImageInfo info = _Codefile.GetInformation(file, true);
                for (int i = 1; i < info.TotalPages; i++) {
                    try {
                        _imagepx = _Codef.Load(file, info.BitsPerPixel, CodecsLoadByteOrder.BgrOrGrayOrRomm, i, i);
                        ocrPage = ocrEngine.CreatePage(_imagepx, OcrImageSharingMode.None);
                        if (ocrPage == null) {
                            continue;
                        }

                        ocrPage.Zones.Clear();
                        LeadRect rcw = new LeadRect(0, 0, _imagepx.Width - 8, 800);
                        OcrZone zone = new OcrZone();
                        zone.Bounds = LogicalRectangle.FromRectangle(rcw);
                        zone.ZoneType = OcrZoneType.Text;
                        zone.CharacterFilters = OcrZoneCharacterFilters.None;
                        ocrPage.Zones.Add(zone);
                        ocrPage.Recognize(null);
                        string txt = ocrPage.GetText(0);
                        str.Add(txt);
                        pages.Add(i.ToString());
                    } catch {
                        continue;
                    }

                }
            } catch {
            } finally {
                _CloseOcr();
            }
        }



        #endregion

        #region 扫描处理

        /// 传递扫描页数
        public void Setpage(int page, int counpage)
        {
            Spage?.Invoke(page, counpage);
        }
        //清空页码
        public void SetpageZero()
        {
            CrrentPage = 0;
            CountPage = 0;
        }

        /// <summary>
        /// 私有-设置扫描纸张大小
        /// </summary>
        /// <param name="_size">1:a4;2:a3</param>
        public void _Setpagesize(int x)
        {
            try {
                _twnCap.Information.Type = TwainCapabilityType.ImageSupportedSizes;
                _twnCap.Information.ContainerType = TwainContainerType.OneValue;
                _twnCap.OneValueCapability.ItemType = TwainItemType.Uint16;
                switch (x) {
                    case 0:
                        _twnCap.OneValueCapability.Value = (UInt16)TwainCapabilityValue.SupportedSizesJisB5;
                        _twains.SetCapability(_twnCap, TwainSetCapabilityMode.Set);
                        break;
                    case 1:
                        _twnCap.OneValueCapability.Value = (UInt16)TwainCapabilityValue.SupportedSizesA4;
                        _twains.SetCapability(_twnCap, TwainSetCapabilityMode.Set);
                        break;
                    case 2:
                        _twnCap.OneValueCapability.Value = (UInt16)TwainCapabilityValue.SupportedSizesA3;
                        _twains.SetCapability(_twnCap, TwainSetCapabilityMode.Set);
                        break;
                }
            } catch {

            }
        }

        // 私有-图像转格式
        private void Imgtobit(int x)
        {
            try {
                _twnCap.Information.Type = TwainCapabilityType.ImageBitDepth;
                _twnCap.Information.ContainerType = TwainContainerType.OneValue;
                _twnCap.OneValueCapability.ItemType = TwainItemType.Uint16;
                switch (x) {
                    case 1:
                        _twnCap.OneValueCapability.Value = 1;
                        _twains.SetCapability(_twnCap, TwainSetCapabilityMode.Set);
                        break;
                    case 8:
                        _twnCap.OneValueCapability.Value = 8;
                        _twains.SetCapability(_twnCap, TwainSetCapabilityMode.Set);
                        break;
                    case 24:
                        _twnCap.OneValueCapability.Value = 24;
                        _twains.SetCapability(_twnCap, TwainSetCapabilityMode.Set);
                        break;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 设置纸张颜色
        /// </summary>       
        /// <param name="x">0:黑白;1:灰度;2:彩色</param>
        public void _Setcolor(int x)
        {
            try {
                _twains.ImageBitsPerPixel = x;
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// 私有-ADF和平板
        /// </summary>       
        /// <param name="x">0:Adf;1:平板</param>
        private void Setadf(int x)
        {
            try {
                _twnCap.Information.Type = TwainCapabilityType.FeederEnabled;
                _twnCap.Information.ContainerType = TwainContainerType.OneValue;
                _twnCap.OneValueCapability.ItemType = TwainItemType.Bool;
                if (x == 0) {
                    // _twnCap.Information.Type = TwainCapabilityType.FeederEnabled;
                }

                if (x == 1) {
                    _twnCap.OneValueCapability.Value = true;
                    _twains.SetCapability(_twnCap, TwainSetCapabilityMode.Set);
                    return;
                }
                else if (x == 2) {

                    _twnCap.OneValueCapability.Value = false;
                    _twains.SetCapability(_twnCap, TwainSetCapabilityMode.Set);
                    return;

                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
                return;
            }
        }


        /// <summary>
        /// ADF和平板
        /// </summary>       
        /// <param name="x">0:Adf;1:平板</param>
        public void _Setadf(int x)
        {
            try {
                if (GetTwain()) {
                    Setadf(x);
                }
            } catch { }
        }


        //分辨率
        public void _Setdpi(int x)
        {
            try {
                //x分辨率
                _twnCap.Information.Type = TwainCapabilityType.ImageXResolution;
                _twnCap.Information.ContainerType = TwainContainerType.OneValue;
                _twnCap.OneValueCapability.ItemType = TwainItemType.Fix32;
                _twnCap.OneValueCapability.Value = x;
                _twains.SetCapability(_twnCap, TwainSetCapabilityMode.Set);
                //y 分辨率
                _twnCap.Information.Type = TwainCapabilityType.ImageYResolution;
                _twnCap.OneValueCapability.ItemType = TwainItemType.Fix32;
                _twnCap.OneValueCapability.Value = x;
                _twains.SetCapability(_twnCap, TwainSetCapabilityMode.Set);
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }



        /// <summary>
        /// 扫描 显示参数
        /// </summary>
        /// <param name="x">0:选择源目录；1：直接扫描； 2：显示扫描参数</param>
        public void _Twainscan(int x)
        {
            try {

                if (x == 0) {
                    if (_twains.SelectSource(String.Empty) == DialogResult.Cancel) { }
                }
                else if (x == 1) {
                    _twains.Acquire(TwainUserInterfaceFlags.None | TwainUserInterfaceFlags.Modal);
                }
                else if (x == 2) {
                    _twains.Acquire(TwainUserInterfaceFlags.Show | TwainUserInterfaceFlags.Modal);
                }
            } catch {
                MessageBox.Show("请查看扫描驱动或者是否连接扫描仪");
            }
        }

        // 判断是否安装扫描源
        public Boolean _Istwain()
        {
            bool twainAvailable = TwainSession.IsAvailable(istwan);
            if (twainAvailable) {
                return true;
            }
            else {
                return false;
            }
        }

        /// 是否连接扫描仪
        private Boolean GetTwain()
        {
            try {
                _twnCap = _twains.GetCapability(TwainCapabilityType.DeviceOnline, TwainGetCapabilityMode.GetCurrent);
                _twnCap.Information.ContainerType = TwainContainerType.OneValue;
                if (Convert.ToInt32(_twnCap.OneValueCapability.Value) == 1) {
                    return true;
                }
                else {
                    return false;
                }
            } catch {
                return false;
            }
        }

        //扫描处发
        private void _twanscan_AcquirePage(object sender, TwainAcquirePageEventArgs e)
        {
            try {
                if (e.Image != null) {
                    Action<RasterImage> Act = Scanepage;
                    _Imageview.Zoom(ControlSizeMode.FitAlways, 1, _Imageview.DefaultZoomOrigin);
                    Act(e.Image);
                    _Imageview.Image = e.Image;
                    CountPage++;
                    Setpage(CountPage, CountPage);
                    CrrentPage = CountPage;
                    Application.DoEvents();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }
        //单双面
        public void _Duplexpage(bool x)
        {
            try {
                _twains.EnableDuplexScanning = x;
            } catch {

            }
        }

        #endregion

        #region pageorder

        //找重复页码
        private Dictionary<int, string> addcfpage()
        {
            Dictionary<int, string> tmp = new Dictionary<int, string>();
            if (TagPage == 0) {

                for (int i = 1; i <= _PageNumber.Count; i++) {
                    _PageNumber.GroupBy(item => item.Value)
                        .Where(item => item.Count() > 1 && item.Key == i && item.Key != -1)
                        .SelectMany(item => item)
                  .ToList()
                   .ForEach(item => tmp.Add(item.Key, item.Value.ToString()));
                    if (tmp.Count >= 2) {
                        return tmp;
                    }
                }
                if (_PageAbc.Count > 0) {
                    _PageAbc.GroupBy(item => item.Value)
                        .Where(item => item.Count() > 1)
                        .SelectMany(item => item)
                        .ToList()
                        .ForEach(item => tmp.Add(item.Key, item.Value.ToString()));
                    if (tmp.Count >= 2) {
                        return tmp;
                    }
                }
                if (_PageFuhao.Count > 0) {
                    _PageFuhao.GroupBy(item => item.Value)
                        .Where(item => item.Count() > 1)
                        .SelectMany(item => item)
                        .ToList()
                        .ForEach(item => tmp.Add(item.Key, item.Value));
                    if (tmp.Count >= 2) {
                        return tmp;
                    }
                }

            }
            else {
                for (int i = TagPage; i <= _PageNumber.Count + TagPage - 1; i++) {
                    _PageNumber.GroupBy(item => item.Value)
                        .Where(item => item.Count() > 1 && item.Key == i && item.Key != -1)
                        .SelectMany(item => item)
                        .ToList()
                        .ForEach(item => tmp.Add(item.Key, item.Value.ToString()));
                    if (tmp.Count >= 2) {
                        return tmp;
                    }
                }

                if (_PageAbc.Count > 0) {
                    _PageAbc.GroupBy(item => item.Value)
                        .Where(item => item.Count() > 1)
                        .SelectMany(item => item)
                        .ToList()
                        .ForEach(item => tmp.Add(item.Key, item.Value.ToString()));
                    if (tmp.Count >= 2) {
                        return tmp;
                    }
                }
                if (_PageFuhao.Count > 0) {
                    _PageFuhao.GroupBy(item => item.Value)
                        .Where(item => item.Count() > 1)
                        .SelectMany(item => item)
                        .ToList()
                        .ForEach(item => tmp.Add(item.Key, item.Value));
                    if (tmp.Count >= 2) {
                        return tmp;
                    }
                }
            }
            return tmp;
        }

        //查找缺少页码
        private List<string> addpageqs()
        {
            List<string> tmp = new List<string>();
            if (TagPage == 0) {
                for (int t = 0; t < Fuhao.Count; t++) {
                    string s = Fuhao[t];
                    if (s.Trim().Length <= 0)
                        continue;
                    if (!_PageFuhao.ContainsValue(s))
                        tmp.Add(s);
                }
                for (int i = 1; i <= RegPage - _PageAbc.Count - _PageFuhao.Count; i++) {
                    if (!_PageNumber.ContainsValue(i)) {
                        tmp.Add(i.ToString());
                    }
                }

            }
            else {
                for (int t = 0; t < Fuhao.Count; t++) {
                    string s = Fuhao[t];
                    if (s.Trim().Length <= 0)
                        continue;
                    if (!_PageFuhao.ContainsValue(s))
                        tmp.Add(s);
                }
                for (int i = TagPage; i <= RegPage - _PageAbc.Count - _PageFuhao.Count + TagPage - 1; i++) {
                    if (!_PageNumber.ContainsValue(i)) {
                        tmp.Add(i.ToString());
                    }
                }
            }
            if (dianjicount >= tmp.Count) {
                dianjicount = 0;
            }

            return tmp;
        }

        //查找超出登记页码
        private List<string> addpagedy()
        {
            List<string> tmp = new List<string>();
            if (TagPage == 0) {
                foreach (var item in _PageFuhao.Values)
                {
                    if (Fuhao.IndexOf(item)<0)
                        tmp.Add(item.ToString());
                }
                foreach (var item in _PageNumber.Values) {
                    if (item > RegPage - _PageAbc.Count - _PageFuhao.Count) {
                        tmp.Add(item.ToString());
                    }
                }
            }
            else {
                foreach (var item in _PageFuhao.Values) {
                    if (Fuhao.IndexOf(item) < 0)
                        tmp.Add(item.ToString());
                }
                foreach (var item in _PageNumber.Values) {
                    if (item > RegPage - _PageAbc.Count - _PageFuhao.Count + TagPage) {
                        tmp.Add(item.ToString());
                    }
                }
            }
            if (dianjicount >= tmp.Count) {
                dianjicount = 0;
            }
            return tmp;
        }

        //校对页码
        public Boolean _Checkpage()
        {
            if (_Imageview.Image == null || Filename.Trim().Length <= 0)
                return false;
            if (_PageNumber.Count > 0) {
                if (addcfpage() != null) {
                    Dictionary<int, string> tmp = addcfpage();
                    if (dianjicount >= tmp.Count) {
                        dianjicount = 0;
                    }
                    if (tmp.Count - 1 >= dianjicount) {
                        MessageBox.Show(string.Format("第{0}页重复", tmp.Values.First()));
                        int page = Convert.ToInt32(tmp.ElementAt(dianjicount).Key);
                        _Gotopage(page);
                        dianjicount++;
                        return false;
                    }
                }
                List<string> lstmp = addpageqs().ToList();
                if (lstmp.Count > 0) {
                    MessageBox.Show("缺少页码：" + string.Join(",", lstmp.ToArray()));
                    lstmp = addpagedy().ToList();
                    {
                        if (lstmp.Count > 0) {
                            MessageBox.Show("超出登记页码:" + string.Join(",", lstmp.ToArray()));
                            return false;
                        }
                    }
                    return false;
                }
                lstmp = addpagedy().ToList();
                {
                    if (lstmp.Count > 0) {
                        MessageBox.Show("超出登记页码：" + string.Join(",", lstmp.ToArray()));
                        return false;
                    }
                }
                return true;
            }
            else {
                MessageBox.Show("未发现页码或未排序!");
                return false;
            }
        }

        //保存英文页码
        private void OderAbc(string npage)
        {
            int oldpage = CrrentPage;
            if (_PageAbc.ContainsKey(oldpage))
                //修改页码
                _PageAbc[oldpage] = npage;
            //	数据不存在 添加
            else if (_PageAbc.Count <= 0 || !_PageAbc.ContainsKey(oldpage)) {
                _PageAbc.Add(oldpage, npage);
            }
            //_SavePage();
        }

        private bool isExists(string str)
        {
            return Regex.Matches(str, "[a-zA-Z]").Count > 0;
        }

        private void OderFh(string page)
        {
            if (_PageFuhao.ContainsKey(CrrentPage))
                _PageFuhao[CrrentPage] = page;
            else if (_PageFuhao.Count <= 0 || !_PageFuhao.ContainsKey(CrrentPage))
                _PageFuhao.Add(CrrentPage, page);
        }

        public void _DelePageTag(int p)
        {
            if (_PageFuhao.ContainsKey(p))
                _PageFuhao.Remove(p);
            else if (_PageAbc.ContainsKey(p))
                _PageAbc.Remove(p);
            else {
                if (_PageNumber.ContainsKey(p))
                    _PageNumber[p] = -9999;
                else if (_PageNumber.Count <= 0 || !_PageNumber.ContainsKey(p)) {
                    _PageNumber.Add(p, -9999);
                }
            }
        }

        //判断数字或英文页码
        public void _Oderpage(string page)
        {
            try {
                if (page == "已删除")
                    page = "-9999";
                if (page == "-9999") {
                    if (_PageAbc.ContainsKey(CrrentPage))
                        _PageAbc.Remove(CrrentPage);
                    if (_PageFuhao.ContainsKey(CrrentPage))
                        _PageFuhao.Remove(CrrentPage);
                    Oderpage(int.Parse(page));
                }
                else if (!isExists(page) && page.IndexOf("-") < 0) {
                    if (_PageAbc.ContainsKey(CrrentPage))
                        _PageAbc.Remove(CrrentPage);
                    if (_PageFuhao.ContainsKey(CrrentPage))
                        _PageFuhao.Remove(CrrentPage);
                    Oderpage(int.Parse(page));
                }
                else if (page.IndexOf("-") <= 0) {
                    if (_PageNumber.ContainsKey(CrrentPage))
                        _PageNumber.Remove(CrrentPage);
                    if (_PageFuhao.ContainsKey(CrrentPage))
                        _PageFuhao.Remove(CrrentPage);
                    OderAbc(page);
                }
                else {
                    if (_PageNumber.ContainsKey(CrrentPage))
                        _PageNumber.Remove(CrrentPage);
                    if (_PageAbc.ContainsKey(CrrentPage))
                        _PageAbc.Remove(CrrentPage);
                    OderFh(page);
                }
                _SavePage();
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }
        //保存数字页码
        private void Oderpage(int npage)
        {
            int oldpage = CrrentPage;
            if (_PageNumber.ContainsKey(oldpage))
                //修改页码
                _PageNumber[oldpage] = npage;
            //	数据不存在 添加
            else if (_PageNumber.Count <= 0 || !_PageNumber.ContainsKey(oldpage)) {
                _PageNumber.Add(oldpage, npage);
            }
        }

        //读取已保存页码
        public string _Readpage()
        {
            try {
                if (_Imageview.Image == null)
                    return "";
                if (!_PageNumber.ContainsKey(CrrentPage)) {
                    if (!_PageAbc.ContainsKey(CrrentPage)) {
                        if (!_PageFuhao.ContainsKey(CrrentPage))
                            return "";
                        else
                            return _PageFuhao[CrrentPage].ToString();
                    }
                    else
                        return _PageAbc[CrrentPage].ToString();
                }
                else
                    return _PageNumber[CrrentPage].ToString();

            } catch {
                return "";
            }
        }


        #endregion
    }
}
