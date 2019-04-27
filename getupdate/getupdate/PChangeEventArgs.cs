using System;

namespace getupdate
{
	public class PChangeEventArgs : EventArgs
	{
		public long CountSize;

		public long TmpSize;

		public PChangeEventArgs(long Countsize, long Tmpsize)
		{
			this.CountSize = Countsize;
			this.TmpSize = Tmpsize;
		}
	}
}
