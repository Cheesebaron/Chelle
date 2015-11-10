using System;
using Android.Content;

namespace dk.ostebaronen.chelle
{
    public static class Chelle
    {
        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If Context is null.</exception>
        public static SocialShare Share(Context context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context), "Context cannot be null");

            return new SocialShare(context);
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If Context is null.</exception>
        public static Email Email(Context context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context), "Context cannot be null");
            return new Email(context);
        }
    }
}
