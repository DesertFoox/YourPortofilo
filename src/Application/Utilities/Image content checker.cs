//using Application.Mapping;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Utilities
//{
//    public static class HttpPostedFileBaseExtensions
//    {
//        public const int ImageMinimumBytes = 512;

//        public static bool IsImage(this UploadImageUserDTO postedFile)
//        {
//            //-------------------------------------------
//            //  Check the image mime types
//            //-------------------------------------------
//            if (!string.Equals(postedFile.Image.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
//                !string.Equals(postedFile.Image.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
//                !string.Equals(postedFile.Image.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
//                !string.Equals(postedFile.Image.ContentType, "image/gif", StringComparison.OrdinalIgnoreCase) &&
//                !string.Equals(postedFile.Image.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
//                !string.Equals(postedFile.Image.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
//            {
//                return false;
//            }

//            //-------------------------------------------
//            //  Check the image extension
//            //-------------------------------------------
//            var postedFileExtension = Path.GetExtension(postedFile.Image.FileName);
//            if (!string.Equals(postedFileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
//                && !string.Equals(postedFileExtension, ".png", StringComparison.OrdinalIgnoreCase)
//                && !string.Equals(postedFileExtension, ".gif", StringComparison.OrdinalIgnoreCase)
//                && !string.Equals(postedFileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
//            {
//                return false;
//            }

//            //-------------------------------------------
//            //  Attempt to read the file and check the first bytes
//            //-------------------------------------------
//            try
//            {
//                if (!postedFile.Image.InputStream.CanRead)
//                {
//                    return false;
//                }
//                //------------------------------------------
//                //   Check whether the image size exceeding the limit or not
//                //------------------------------------------ 
//                if (postedFile.Image.ContentLength < ImageMinimumBytes)
//                {
//                    return false;
//                }

//                byte[] buffer = new byte[ImageMinimumBytes];
//                postedFile.InputStream.Read(buffer, 0, ImageMinimumBytes);
//                string content = System.Text.Encoding.UTF8.GetString(buffer);
//                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
//                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
//                {
//                    return false;
//                }
//            }
//            catch (Exception)
//            {
//                return false;
//            }

//            //-------------------------------------------
//            //  Try to instantiate new Bitmap, if .NET will throw exception
//            //  we can assume that it's not a valid image
//            //-------------------------------------------

//            try
//            {
//                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
//                {
//                }
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//            finally
//            {
//                postedFile.InputStream.Position = 0;
//            }

//            return true;
//        }
//    }
//}
