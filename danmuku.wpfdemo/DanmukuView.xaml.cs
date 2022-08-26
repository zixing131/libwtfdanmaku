using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace danmuku.wpfdemo
{
    /// <summary>
    /// DanmukuView.xaml 的交互逻辑
    /// </summary>
    public partial class DanmukuView : UserControl
    {
        public DanmukuView()
        {
            InitializeComponent();
        } 
        private void panel_Resize(object sender, EventArgs e)
        {
            if(wtf!=IntPtr.Zero)
            { 
                LibLoader.WTF_Resize(wtf, (uint)panel.Width, (uint)panel.Height);
            }
        }

        public const int WM_CLOSE = 0x0010;
        public const int WM_DESTROY = 0x0002;
        public const int WM_SIZE = 0x0005;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_DPICHANGED = 0x02E0;
        public const int COLOR_WINDOW = 5;
        public const int IDC_ARROW = 32512;
        public const int IDI_APPLICATION = 32512;
        public const int WTF_DANMAKU_STYLE_OUTLINE = 1;
        public const int WTF_DANMAKU_TYPE_SCROLLING_VISIBLE = 1;
        public const int WTF_DANMAKU_TYPE_BOTTOM_VISIBLE = 2;
        public const int WTF_DANMAKU_TYPE_TOP_VISIBLE = 4;
        public const int WTF_DANMAKU_TYPE_RESERVE_VISIBLE = 8;
        public const int WTF_DANMAKU_TYPE_POSITION_VISIBLE = 16;
        public const int WTF_DANMAKU_TYPE_ADVANCED_VISIBLE = 32;
        public const long WS_EX_NOREDIRECTIONBITMAP = 0x00200000L;
        public const long SIZE_RESTORED = 0;
        public const long SIZE_MAXIMIZED = 2;

        public string targeDanmu { get; set; }

        private static bool wtfInited = false;
        private IntPtr wtf = IntPtr.Zero;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
          
        } 
        public void Load()
        {
            if (!wtfInited)
            {
                wtf = LibLoader.WTF_CreateInstance();
                LibLoader.WTF_InitializeWithHwnd(wtf, this.panel.Handle);
                LibLoader.WTF_SetFontName(wtf, "SimHei");
                LibLoader.WTF_SetFontWeight(wtf, 700);
                LibLoader.WTF_SetFontScaleFactor(wtf, 1.0f);
                LibLoader.WTF_SetDanmakuStyle(wtf, WTF_DANMAKU_STYLE_OUTLINE);
                LibLoader.WTF_SetCompositionOpacity(wtf, 0.9f);
                //LibLoader.WTF_SetDanmakuTypeVisibility(wtf, WTF_DANMAKU_TYPE_SCROLLING_VISIBLE | WTF_DANMAKU_TYPE_TOP_VISIBLE | WTF_DANMAKU_TYPE_BOTTOM_VISIBLE);
                LibLoader.WTF_LoadBilibiliFile(wtf, Encoding.ASCII.GetBytes(targeDanmu));
            }
            wtfInited = true;
        }

        public void Start()
        {
            LibLoader.WTF_Start(wtf);
        }
        public void Pause()
        {
            if (wtf != IntPtr.Zero)
            {
                LibLoader.WTF_Pause(wtf);
            }
        }
        public void Destory()
        { 
            if (LibLoader.WTF_IsRunning(wtf) != 0)
                LibLoader.WTF_Stop(wtf);
            LibLoader.WTF_ReleaseInstance(wtf);
            wtf = IntPtr.Zero;
            wtfInited = false;
        }
    }
}
