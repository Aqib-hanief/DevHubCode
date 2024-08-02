using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Web;
using System.IO;

namespace BLL
{
    public static class CommonBLL
    {
        public static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[20];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string CreatePasswordHash(string password, string salt)
        {
            string pwdSalt = string.Concat(password, salt);
            SHA256Managed sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(pwdSalt));
            return Convert.ToBase64String(hash);
        }

        public static string UploadProfileImage(HttpPostedFileBase img, string dir)
        {

            string extension = Path.GetExtension(img.FileName).ToLower();

            if (extension == ".jpg" || extension == ".jpeg")
            {
                if (img.ContentLength >= 2097152)
                {
                    return "Image size must be less than 2 mb.";
                }

                string filename = Guid.NewGuid() + extension;
                string fullPath = dir + filename;

                img.SaveAs(HttpContext.Current.Server.MapPath(fullPath));
                return fullPath;

            }
            return "Only jpg and jpeg formats are allowed.";
        }
    }
}
