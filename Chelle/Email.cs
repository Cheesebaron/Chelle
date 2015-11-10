using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.OS;
using Uri = Android.Net.Uri;

namespace dk.ostebaronen.chelle
{
    public class Email
    {
        private readonly Context _context;

        private readonly List<string> _toList = new List<string>();
        private readonly List<string> _ccList = new List<string>();
        private readonly List<string> _bccList = new List<string>();
        private string _subject;
        private string _body;

        /// <summary>
        /// Represents an email to a person or group of people.
        /// </summary>
        /// <param name="context"><see cref="Context"/> used to resolve the email sending <see cref="Intent"/></param>
        public Email(Context context) {
            _context = context;
        }

        /// <summary>
        /// Add email addresses, which email should be sent to. This appends to the existing To email addresses.
        /// </summary>
        /// <param name="to"><see cref="IEnumerable{T}"/> of <see cref="string"/> containing email addresses.</param>
        /// <exception cref="ArgumentNullException">Thrown if <param name="to"/> is null.</exception>
        public Email To(IEnumerable<string> to)
        {
            if (to == null) throw new ArgumentNullException(nameof(to));

            _toList.AddRange(to);
            return this;
        }

        /// <summary>
        /// Add a single email address, which the email should be sent to. This appends to the existing To email addresses.
        /// </summary>
        /// <param name="to"><see cref="string"/> with the email</param>
        /// <exception cref="ArgumentNullException">Thrown if <param name="to"/> is null or empty.</exception>
        public Email To(string to)
        {
            if (string.IsNullOrEmpty(to)) throw new ArgumentNullException(nameof(to));

            _toList.Add(to);
            return this;
        }

        /// <summary>
        /// Add email addresses, which should receive a Carbon Copy. This appends to the existing CC email addresses.
        /// </summary>
        /// <param name="cc"><see cref="IEnumerable{T}"/> of <see cref="string"/> containing email addresses.</param>
        /// <exception cref="ArgumentNullException">Thrown if <param name="cc"/> is null.</exception>
        public Email Cc(IEnumerable<string> cc)
        {
            if (cc == null) throw new ArgumentNullException(nameof(cc));

            _ccList.AddRange(cc);
            return this;
        }

        /// <summary>
        /// Add a single email addresses, which should receive a Carbon Copy. This appends to the existing CC email addresses.
        /// </summary>
        /// <param name="cc"><see cref="IEnumerable{T}"/> of <see cref="string"/> containing email addresses.</param>
        /// <exception cref="ArgumentNullException">Thrown if <param name="cc"/> is null or empty.</exception>
        public Email Cc(string cc)
        {
            if (string.IsNullOrEmpty(cc)) throw new ArgumentNullException(nameof(cc));

            _ccList.Add(cc);
            return this;
        }

        /// <summary>
        /// Add email addresses, which should receive a Blind Carbon Copy. This appends to the existing BCC email addresses.
        /// </summary>
        /// <param name="bcc"><see cref="IEnumerable{T}"/> of <see cref="string"/> containing email addresses.</param>
        /// <exception cref="ArgumentNullException">Thrown if <param name="bcc"/> is null.</exception>
        public Email Bcc(IEnumerable<string> bcc)
        {
            if (bcc == null) throw new ArgumentNullException(nameof(bcc));

            _bccList.AddRange(bcc);
            return this;
        }

        /// <summary>
        /// Add a single email addresses, which should receive a Blind Carbon Copy. This appends to the existing BCC email addresses.
        /// </summary>
        /// <param name="bcc"><see cref="IEnumerable{T}"/> of <see cref="string"/> containing email addresses.</param>
        /// <exception cref="ArgumentNullException">Thrown if <param name="bcc"/> is null or empty.</exception>
        public Email Bcc(string bcc)
        {
            if (string.IsNullOrEmpty(bcc)) throw new ArgumentNullException(nameof(bcc));

            _bccList.Add(bcc);
            return this;
        }

        /// <summary>
        /// Adds the subject to the email
        /// </summary>
        /// <param name="subject"><see cref="string"/> with the subject.</param>
        /// <exception cref="ArgumentNullException">Thrown if <param name="subject"/> is null or empty.</exception>
        public Email Subject(string subject)
        {
            if (string.IsNullOrEmpty(subject)) throw new ArgumentNullException(nameof(subject));

            _subject = subject;
            return this;
        }

        /// <summary>
        /// Adds the body to the email
        /// </summary>
        /// <param name="body"><see cref="string"/> with the body.</param>
        /// <exception cref="ArgumentNullException">Thrown if <param name="body"/> is null or empty.</exception>
        public Email Body(string body)
        {
            if (string.IsNullOrEmpty(body)) throw new ArgumentNullException(nameof(body));

            _body = body;
            return this;
        }

        /// <summary>
        /// Creates the email <see cref="Intent"/> and launches it.
        /// </summary>
        /// <returns>Returns <see cref="bool"/> with <value>true</value> if sucessful or <value>false</value> if not.</returns>
        /// <exception cref="!:NoType:ActivityNotFoundException">May throw if any email activities not found.</exception>
        public bool Send()
        {
            var emailIntent = new Intent(Intent.ActionSend);
            emailIntent.SetType(Mime.Email);
            emailIntent.SetData(Uri.Parse("mailto:"));

            if (_toList.Any())
                emailIntent.PutExtra(Intent.ExtraEmail, _toList.ToArray());

            if (_ccList.Any())
                emailIntent.PutExtra(Intent.ExtraCc, _ccList.ToArray());

            if (_bccList.Any())
                emailIntent.PutExtra(Intent.ExtraBcc, _bccList.ToArray());

            emailIntent.PutExtra(Intent.ExtraSubject, _subject);
            emailIntent.PutExtra(Intent.ExtraText, _body);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                emailIntent.AddFlags(ActivityFlags.NewDocument);
            else
                emailIntent.AddFlags(ActivityFlags.ClearWhenTaskReset);

            if (emailIntent.ResolveActivity(_context.PackageManager) == null) return false;

            _context.StartActivity(emailIntent);
            return true;
        }
    }
}