using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.OS;

namespace dk.ostebaronen.chelle
{
    public class SocialShare
    {
        private readonly Context _context;
        private string _text;
        private Android.Net.Uri _uri;
        private string _mimeType = Mime.PlainText;
        private readonly List<string> _urlList = new List<string>();

        /// <summary>
        /// Basic sharing of text, image or video.
        /// </summary>
        /// <param name="context"></param>
        public SocialShare(Context context) {
            _context = context;
        }

        /// <summary>
        /// Sets the text to share. Note: overwrites any existing text set
        /// </summary>
        /// <param name="text"><see cref="string"/> with text to share.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Throws if <param name="text"/> is null or empty.</exception>
        public SocialShare Text(string text)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));

            _text = text;
            return this;
        }

        /// <summary>
        /// Add urls to share. These will be appended to the end of the share text.
        /// <example><code>
        /// Chelle.Share(context)
        ///   .Text("Text to share")
        ///   .Url(new string[] { "http://ostebaronen.dk", "http://mvvmcross.com" })
        ///   .Send();
        /// </code>
        /// This will share the text: "Text to share http://ostebaronen.dk http://mvvmcross.com".
        /// </example>
        /// You can share as many urls as you want. They will just be appended.
        /// </summary>
        /// <param name="urls"><see cref="IEnumerable{T}"/> of <see cref="string"/> containing urls to share</param>
        /// <exception cref="ArgumentNullException">Throws if <param name="urls"/> is null.</exception>
        public SocialShare Url(IEnumerable<string> urls)
        {
            if (urls == null) throw new ArgumentNullException(nameof(urls));

            _urlList.AddRange(urls);
            return this;
        }

        /// <summary>
        /// Add url to share. This will be appended to the end of the share text.
        /// <example><code>
        /// Chelle.Share(context)
        ///   .Text("Text to share")
        ///   .Url(http://ostebaronen.dk)
        ///   .Send();
        /// </code>
        /// This will share the text: "Text to share http://ostebaronen.dk".
        /// </example>
        /// You can share as many urls as you want. They will just be appended.
        /// </summary>
        /// <param name="url"><see cref="string"/> containing url to share</param>
        /// <exception cref="ArgumentNullException">Throws if <param name="url"/> is null or empty.</exception>
        public SocialShare Url(string url)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            _urlList.Add(url);
            return this;
        }

        /// <summary>
        /// Sets the image to share. This can only be called once, you can only share one video or image. Not both.
        /// </summary>
        /// <param name="uri"><see cref="Android.Net.Uri"/> with the Uri to the image</param>
        /// <exception cref="ArgumentNullException">Throws if <param name="uri"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Throws if an Image or Video Uri has already been set. Only one Image or Video Uri allowed</exception>
        public SocialShare Image(Android.Net.Uri uri)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            if (_uri != null) throw new InvalidOperationException("Only one Image or Video Uri allowed");

            _uri = uri;
            _mimeType = Mime.AnyImage;
            return this;
        }

        /// <summary>
        /// Sets the video to share. This can only be called once, you can only share one video or image. Not both.
        /// </summary>
        /// <param name="uri"><see cref="Android.Net.Uri"/> with the Uri to the video</param>
        /// <exception cref="ArgumentNullException">Throws if <param name="uri"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Throws if an Image or Video Uri has already been set. Only one Image or Video Uri allowed</exception>
        public SocialShare Video(Android.Net.Uri uri)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            if (_uri != null) throw new InvalidOperationException("Only one Image or Video Uri allowed");

            _uri = uri;
            _mimeType = Mime.AnyVideo;
            return this;
        }

        /// <summary>
        /// Creates the share <see cref="Intent"/> and launches it.
        /// </summary>
        /// <returns>Returns <see cref="bool"/> with <value>true</value> if sucessful or <value>false</value> if not.</returns>
        /// <exception cref="!:NoType:ActivityNotFoundException">May throw if any email activities not found.</exception>
        public bool Send()
        {
            var shareIntent = new Intent(Intent.ActionSend);
            shareIntent.SetType(_mimeType);

            shareIntent.PutExtra(Intent.ExtraText, BuildText());

            if (_uri != null)
                shareIntent.PutExtra(Intent.ExtraStream, _uri);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                shareIntent.AddFlags(ActivityFlags.NewDocument);
            else
                shareIntent.AddFlags(ActivityFlags.ClearWhenTaskReset);

            if (shareIntent.ResolveActivity(_context.PackageManager) == null) return false;

            _context.StartActivity(shareIntent);
            return true;
        }

        private string BuildText()
        {
            if (!_urlList.Any()) return _text;
            
            foreach (var url in _urlList)
                _text += $" {url}";

            return _text;
        }
    }
}