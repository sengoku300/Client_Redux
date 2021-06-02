using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Redux_Client.CustomControls
{
	public class MegaClass
	{
		public delegate void DeletePhotography(PhotoCard card);
		public delegate void SelectionPhotography(PhotoCard card);
		public delegate void LikeProfile(LikedUser sender);
		public delegate void DislikeProfile(LikedUser sender);
		public delegate void BanProfile(LikedUser sender);
		public delegate void UnbanAndLikeProfile(BlackListItem sender);
		public delegate void OpenProfile(BlackListItem sender);
	}
}
