using System;
using UnityEngine;

namespace GoogleMobileAds.Common
{
	internal class Utils
	{
		public static Texture2D GetTexture2DFromByteArray(byte[] img)
		{
			Texture2D texture2D = new Texture2D(1, 1);
			if (!texture2D.LoadImage(img))
			{
				throw new InvalidOperationException("Could not load custom native template\n                        image asset as texture");
			}
			return texture2D;
		}
	}
}
