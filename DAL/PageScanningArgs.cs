using System;

namespace DAL
{
    public class DownupArgs : EventArgs
    {
        private int _Page;

        private int _Pagetype;

        public int Page
        {
            get
            {
                return this._Page;
            }
        }

        public int PageType
        {
            get
            {
                return this._Pagetype;
            }
        }

        public DownupArgs(int page, int PagerType)
        {
            this._Page = page;
            this._Pagetype = PagerType;
        }
    }
}
